using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaPontosControle : MonoBehaviour
{

    public static CartaPontosControle Referencia;
    private void Awake()
    {
        Referencia = this;
    }

    public CartasPontos[] CartaPontosJogador, CartaPontosInimigos;

    public float TempoEntreAtaques = .25f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AtaqueJogador()
    {
        StartCoroutine(AtaqueJogadorCO());
    }

    IEnumerator AtaqueJogadorCO()
    {
        yield return new WaitForSeconds(TempoEntreAtaques);

        for (int i = 0; i < CartaPontosJogador.Length; i++)
        {
            if (CartaPontosJogador[i].CartaAtiva != null)
            {

                if (CartaPontosInimigos[i].CartaAtiva != null)
                {
                    //Ataca a carta aliada
                    CartaPontosInimigos[i].CartaAtiva.DanoCarta(CartaPontosJogador[i].CartaAtiva.DanoDeAtaque);




                }
                else
                {
                    //Ataca o heroi

                    ControleDeBatalha.Referencia.DanoInimigo(CartaPontosJogador[i].CartaAtiva.DanoDeAtaque);

                }

                //CartaPontosJogador[i].CartaAtiva.anim.SetTrigger("CausarDano");
                yield return new WaitForSeconds(TempoEntreAtaques);

            }
            if (ControleDeBatalha.Referencia.BatalhaFinalizada==true)
            {
                i = CartaPontosJogador.Length;
            }
        }
        ConferirCartasMarcadas();
        ControleDeBatalha.Referencia.AvancarTurno();
    }


    public void AtaqueInimigo()
    {
        StartCoroutine(AtaqueInimigoCO());
    }

    IEnumerator AtaqueInimigoCO()
    {
        yield return new WaitForSeconds(TempoEntreAtaques);

        for (int i = 0; i < CartaPontosInimigos.Length; i++)
        {
            if (CartaPontosInimigos[i].CartaAtiva != null)
            {

                if (CartaPontosJogador[i].CartaAtiva != null)
                {
                    //Ataca a carta aliada
                    CartaPontosJogador[i].CartaAtiva.DanoCarta(CartaPontosInimigos[i].CartaAtiva.DanoDeAtaque);




                }
                else
                {
                    //Ataca o heroi

                    ControleDeBatalha.Referencia.DanoJogador(CartaPontosInimigos[i].CartaAtiva.DanoDeAtaque);

                }

                //CartaPontosInimigos[i].CartaAtiva.anim.SetTrigger("CausarDano");
                yield return new WaitForSeconds(TempoEntreAtaques);

            }
             if (ControleDeBatalha.Referencia.BatalhaFinalizada==true)
            {
                i = CartaPontosInimigos.Length;
            }
        }
        ConferirCartasMarcadas();
        ControleDeBatalha.Referencia.AvancarTurno();
    }
        public void ConferirCartasMarcadas()
    {
        foreach (CartasPontos Ponto in CartaPontosInimigos)
        {
            if (Ponto.CartaAtiva!= null)
            {
                if (Ponto.CartaAtiva.VidaAtual <= 0)
                {
                    Ponto.CartaAtiva = null;
                }
            }
        }

        foreach (CartasPontos Ponto in CartaPontosJogador)
        {
            if (Ponto.CartaAtiva != null)
            {
                if (Ponto.CartaAtiva.VidaAtual <= 0)
                {
                    Ponto.CartaAtiva = null;
                }
            }
        }
    }
}