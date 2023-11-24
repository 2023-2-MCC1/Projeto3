using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR;
using JetBrains.Annotations;

public class Carta : MonoBehaviour
{
    public CartaScriptableObject CartaSO;

    public bool EJogador;

    //Setando a vida, dano e custo das cartas

    public int VidaAtual;
    public int DanoDeAtaque, CustoDeMana;

    public TMP_Text ValorDeVida, ValorDeAtaque, ValorDeCustoMana, NomeDaCartaTexto, DescricaoDaCartaTexto, HistoriaDaCartaTexto;

    public Image ArteDoPersonagem, ArteDoFundo;

    private Vector3 PontoAlvo;
    private Quaternion RotacaoAlvo;
    public float VelocidadeDeMovimento = 5f, VelocidadeDeRotacao = 540f;

    public bool NaMao;
    public int PosicaoNaMao;

    private ControleDaMao CM;

    private bool Escolhido;
    private Collider Colisor;

    public LayerMask OQueETabuleiro,OQueELocalizacao;

    private bool FoiPressionado;

    public CartasPontos LugarMarcado;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
       if(PontoAlvo == Vector3.zero)
        {
            PontoAlvo = transform.position;
            RotacaoAlvo = transform.rotation;
        }
        
        SetupCarta();

        CM = FindObjectOfType<ControleDaMao>();
        Colisor = GetComponent<Collider>();

        
    }

    public void SetupCarta() //Fun��o que faz com que o valor emitido no Scriptable Object apare�a na carta
    {
        VidaAtual = CartaSO.VidaAtual;
        DanoDeAtaque = CartaSO.DanoDeAtaque;
        CustoDeMana = CartaSO.CustoDeMana;

        /*ValorDeVida.text = VidaAtual.ToString();
        ValorDeAtaque.text = DanoDeAtaque.ToString();
        ValorDeCustoMana.text = CustoDeMana.ToString();*/

        AtualizarCartaTexto();

        NomeDaCartaTexto.text = CartaSO.NomeDaCarta;
        DescricaoDaCartaTexto.text = CartaSO.DescricaoDaAcao;
        HistoriaDaCartaTexto.text = CartaSO.HistoriaDaCarta;

        ArteDoPersonagem.sprite = CartaSO.SpritePersonagem;
        ArteDoFundo.sprite = CartaSO.SpriteFundo;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, PontoAlvo, VelocidadeDeMovimento * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, RotacaoAlvo, VelocidadeDeRotacao * Time.deltaTime);

        if (Escolhido) //Sequencia de ifs que permitem a movimentacao das cartas e coloca-las em campo
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, OQueETabuleiro)) 
            {
                MoverParaOPonto(hit.point + new Vector3(0f, 2f, 0f), Quaternion.identity);
            }

            if (Input.GetMouseButtonDown(1) && ControleDeBatalha.Referencia.BatalhaFinalizada==false)
            {
                VoltarParaAMao();
            }

            if (Input.GetMouseButtonDown(0) && FoiPressionado == false && ControleDeBatalha.Referencia.BatalhaFinalizada==false)
            {
                if (Physics.Raycast(ray, out hit, 100f, OQueELocalizacao) && ControleDeBatalha.Referencia.TurnoAtual == ControleDeBatalha.OrdemDosTurnos.JogadorAtivo)
                {
                    CartasPontos PontoEscolhido = hit.collider.GetComponent<CartasPontos>();

                    if (PontoEscolhido.CartaAtiva == null && PontoEscolhido.EPontoJogavel)
                    {
                        if (ControleDeBatalha.Referencia.ManaJogador >= CustoDeMana)
                        {

                            PontoEscolhido.CartaAtiva = this;
                            LugarMarcado = PontoEscolhido;

                            MoverParaOPonto(PontoEscolhido.transform.position, Quaternion.identity);

                            NaMao = false;

                            Escolhido = false;

                            CM.RemoverCartaDaMao(this);

                            ControleDeBatalha.Referencia.GastarMana(CustoDeMana);
                        }
                        else
                        {
                            VoltarParaAMao();
                            ControleUI.Referencia.MostrarAvisoSemMana();
                        }
                    }
                    else
                    {
                        VoltarParaAMao();
                    }
                }
                else
                {
                    VoltarParaAMao();
                }
            }
         }
        FoiPressionado = false;
}


    public void MoverParaOPonto(Vector3 PontoParaMover,Quaternion Rotacao)
    {
        PontoAlvo = PontoParaMover;
        RotacaoAlvo = Rotacao;
    }

    private void OnMouseOver() //Fun��o que, ao passar o mouse por cima da carta na m�o, ela sobe.
    {
        if (NaMao && EJogador && ControleDeBatalha.Referencia.BatalhaFinalizada==false)
        {
            MoverParaOPonto(CM.PosicoesCartas[PosicaoNaMao] + new Vector3(0f, 1f, .5f), Quaternion.identity);
        }
    }

    private void OnMouseExit()//Fun��o que, ao tirar o mouse da carta, ela volta para seu ponto anterior
    {
        if (NaMao && EJogador && ControleDeBatalha.Referencia.BatalhaFinalizada==false)
        {
            MoverParaOPonto(CM.PosicoesCartas[PosicaoNaMao], CM.MinPos.rotation);
        }
    }

    private void OnMouseDown() //Fun��o que ao clicar na carta voce pode controlar ela
    {
        if (NaMao && ControleDeBatalha.Referencia.TurnoAtual == ControleDeBatalha.OrdemDosTurnos.JogadorAtivo && EJogador && ControleDeBatalha.Referencia.BatalhaFinalizada==false)
        {
            Escolhido = true;
            Colisor.enabled = false;

            FoiPressionado = true;
        }
    }

    public void VoltarParaAMao() //funcao com o proposito de fazer com que a sua carta volte para a mao
    {
        Escolhido = false;
        CM.enabled = true;
        Colisor.enabled = true;

        MoverParaOPonto(CM.PosicoesCartas[PosicaoNaMao], CM.MinPos.rotation);
    }

    public void DanoCarta(int QuantidadeDeDano)
    {
        VidaAtual -= QuantidadeDeDano;

        AtualizarCartaTexto();

        if (VidaAtual <= 0)
        {
            VidaAtual = 0;

            MoverParaOPonto(ControleDeBatalha.Referencia.Cemiterio.position, ControleDeBatalha.Referencia.Cemiterio.rotation);

            anim.SetTrigger("Pulo");

            Destroy(gameObject, 5f);

            LugarMarcado.CartaAtiva = null;

        }

        anim.SetTrigger("SofrerDano");

       
    }

    public void AtualizarCartaTexto()
    {
        ValorDeVida.text = VidaAtual.ToString();
        ValorDeAtaque.text = DanoDeAtaque.ToString();
        ValorDeCustoMana.text = CustoDeMana.ToString();
    }
}