using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDaMao : MonoBehaviour
{
    public static ControleDaMao Referencia;
    private void Awake()
    {
        Referencia = this;
    }

    public List<Carta> SegurarCartas = new List<Carta>();

    public Transform MinPos, MaxPos;
    public List<Vector3> PosicoesCartas = new List<Vector3>();
    //Minimas e Maximas posi��es que as cartas podem estar na m�o

    // Start is called before the first frame update
    void Start()
    {
        SetarPosicaoDasCartasNaMao();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetarPosicaoDasCartasNaMao() // Fun��o que informa as posi��es das cartas
    {
        PosicoesCartas.Clear();

        Vector3 DistanciaEntreOsPontos = Vector3.zero;
        if (SegurarCartas.Count > 1 )
        {
            DistanciaEntreOsPontos = (MaxPos.position - MinPos.position) / (SegurarCartas.Count - 1);
        }
        for (int i = 0; i < SegurarCartas.Count; i++)
        {
            PosicoesCartas.Add(MinPos.position + (DistanciaEntreOsPontos * i));

            //Informa para onde a carta vai
            SegurarCartas[i].MoverParaOPonto(PosicoesCartas[i], MinPos.rotation);

            SegurarCartas[i].NaMao= true;
            SegurarCartas[i].PosicaoNaMao = i;

        }

    }

    public void RemoverCartaDaMao(Carta CartaParaRemover) // ao voce jogar a carta no campo ela � removida do armazenador de cartas na mao
    {
        if (SegurarCartas[CartaParaRemover.PosicaoNaMao] == CartaParaRemover)
        {
            SegurarCartas.RemoveAt(CartaParaRemover.PosicaoNaMao);
        }
        else
        {
            Debug.LogError("Carta na posicao " + CartaParaRemover.PosicaoNaMao + " nao deve ser removida da m�o");
        }

        SetarPosicaoDasCartasNaMao();
    }

    public void AdicionarCartaNaMao(Carta CartaParaAdicionar) // Ao adicionar uma carta na mao ela reformula a posicao e adiciona na lista de cartas seguradas
    {
        SegurarCartas.Add(CartaParaAdicionar);
        SetarPosicaoDasCartasNaMao();
    }

    
  public void MaoVazia()
  {
    foreach (Carta SegurarCarta in SegurarCartas)
    {
        SegurarCarta.NaMao = false;
        SegurarCarta.MoverParaOPonto(ControleDeBatalha.Referencia.Cemiterio.position, SegurarCarta.transform.rotation);
    }
    SegurarCartas.Clear();
  }
}
