using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDeBatalha : MonoBehaviour
{
    public static ControleDeBatalha Referencia;

    private void Awake()
    {
        Referencia = this;
    }

    public int ManaInicial = 4, ManaMaxima = 12;
    public int ManaJogador, ManaInimigo;
    public int QuantidadeDeCartasNoComeco = 5;
    public int CartasParaComprarPorTurno = 1;
    private int ManaMaximaAtualDoJogador, ManaMaximaAtualDoInimigo;
    public int VidaJogador, VidaInimigo;

    public Transform Cemiterio;

    public enum OrdemDosTurnos { JogadorAtivo, JogadorCartaAtaque, InimigoAtivo, InimigoCartaAtaque }
    public OrdemDosTurnos TurnoAtual;
    public bool BatalhaFinalizada;
    public float ResultadoDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        ManaMaximaAtualDoJogador = ManaInicial;
        RestaurarMana();

        ControleDeck.Referencia.ComprarVariasCartas(QuantidadeDeCartasNoComeco);

        ManaMaximaAtualDoInimigo = ManaInicial;

        RestaurarManaInimigo();

        

    }

    // Update is called once per frame
    void Update()
    {
        ControleUI.Referencia.SetarVidaTextoJogador(VidaJogador);
        ControleUI.Referencia.SetarVidaTextoInimigo(VidaInimigo);

        if (Input.GetKeyDown(KeyCode.T))
        {
            AvancarTurno();
        }
    }

    public void GastarMana(int QuantidadeParaGastar) //Funcao com o proposito de gastar a mana indicada ao voce jogar a carta ou comprar uma
    {
        ManaJogador = ManaJogador - QuantidadeParaGastar;

        if (ManaJogador < 0)
        {
            ManaJogador = 0;
        }

        ControleUI.Referencia.SetarManaTexto(ManaJogador);

    }

    public void RestaurarMana()
    {
        ManaJogador = ManaMaximaAtualDoJogador;
        ControleUI.Referencia.SetarManaTexto(ManaJogador);
    }


    public void GastarManaInimigo(int QuantidadeParaGastar) //Funcao com o proposito de gastar a mana do inimigo
    {
        ManaInimigo = ManaInimigo - QuantidadeParaGastar;

        if (ManaInimigo < 0)
        {
            ManaInimigo = 0;
        }

        ControleUI.Referencia.SetarManaTextoInimigo(ManaInimigo);

    }

    public void RestaurarManaInimigo()
    {
        ManaInimigo = ManaMaximaAtualDoInimigo;
        ControleUI.Referencia.SetarManaTextoInimigo(ManaInimigo);
        
    }
    public void AvancarTurno()
    {
        if(BatalhaFinalizada==false)
        {
        TurnoAtual++;

        if ((int)TurnoAtual >= System.Enum.GetValues(typeof(OrdemDosTurnos)).Length)
        {
            TurnoAtual = 0;
        }


        switch (TurnoAtual)
        {
            case OrdemDosTurnos.JogadorAtivo:

                Debug.Log("Passando o Turno 1");
                ControleUI.Referencia.BotaoTerminarTurno.SetActive(true);
                ControleUI.Referencia.BotaoComprarCarta.SetActive(true);

                if (ManaMaximaAtualDoJogador < ManaMaxima)
                {
                    ManaMaximaAtualDoJogador++;
                }

                RestaurarMana();

                ControleDeck.Referencia.ComprarVariasCartas(CartasParaComprarPorTurno);

                break;

            case OrdemDosTurnos.JogadorCartaAtaque:
                //Debug.Log("Passando o Turno 2");

                CartaPontosControle.Referencia.AtaqueJogador();
                //AvancarTurno();

                break;


            case OrdemDosTurnos.InimigoAtivo:
                if (ManaMaximaAtualDoInimigo < ManaMaxima)
                {
                    ManaMaximaAtualDoInimigo++;
                }
                RestaurarManaInimigo();
                ControleInimigo.Referencia.ComecarAcao();
                break;

            case OrdemDosTurnos.InimigoCartaAtaque:

                CartaPontosControle.Referencia.AtaqueInimigo();
                // Debug.Log("Passando o Turno 4");
                // AvancarTurno();

                break;
        }
        }
    }

    public void TerminarTurno()
    {
        ControleUI.Referencia.BotaoTerminarTurno.SetActive(false);
        ControleUI.Referencia.BotaoComprarCarta.SetActive(false);

        AvancarTurno();
    }

    public void DanoJogador(int QuantidadeDeDano)
    {
        if (VidaJogador > 0 || !BatalhaFinalizada)
        {
            VidaJogador -= QuantidadeDeDano;
        }

        if (VidaJogador <= 0)
        {
            VidaJogador = 0;

            //finalizabatalha
            FimBatalha();
        }
        ControleUI.Referencia.SetarVidaTextoJogador(VidaJogador);

       
    }

    public void DanoInimigo(int QuantidadeDeDano)
    {
        if (VidaInimigo > 0 || BatalhaFinalizada ==false)
        {
            VidaInimigo -= QuantidadeDeDano;
        }

        if (VidaInimigo <= 0)
        {
            VidaInimigo = 0;

            //finalizabatalha
            FimBatalha();
        }
        ControleUI.Referencia.SetarVidaTextoInimigo(VidaInimigo);

       
    }
    void FimBatalha()
    {
        BatalhaFinalizada = true;
        ControleDaMao.Referencia.MaoVazia();

        if(VidaInimigo<=0)
        {
            ControleUI.Referencia.BatalhaResultadoTexto.text = "Você venceu!";
            StartCoroutine(MostrarVitoria());
            foreach (CartasPontos point in CartaPontosControle.Referencia.CartaPontosInimigos)
             {
                if (point.CartaAtiva != null)
                {
                    point.CartaAtiva.MoverParaOPonto(Cemiterio.position, point.CartaAtiva.transform.rotation);
                }
                }
                }else
                {

                ControleUI.Referencia.BatalhaResultadoTexto.text = "Você perdeu!";
                StartCoroutine(MostrarDerrota());

            foreach (CartasPontos point in CartaPontosControle.Referencia.CartaPontosJogador)
             {
                if (point.CartaAtiva != null)
                {
                    point.CartaAtiva.MoverParaOPonto(Cemiterio.position, point.CartaAtiva.transform.rotation);
                }
        }
                }
        
    }
    IEnumerator MostrarDerrota()
    {
   yield return new WaitForSeconds (ResultadoDelay);
   ControleUI.Referencia.BatalhaDerrota.SetActive(true);
    }

    IEnumerator MostrarVitoria()
    {
        yield return new WaitForSeconds(ResultadoDelay);
        ControleUI.Referencia.BatalhaVitoria.SetActive(true);
    }

}

