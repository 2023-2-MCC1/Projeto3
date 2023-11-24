using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDeck : MonoBehaviour
{
    public static ControleDeck Referencia;
    private void Awake()
    {
        Referencia = this;
    }

    public List<CartaScriptableObject> DeckParaUso = new List<CartaScriptableObject>();

    private List<CartaScriptableObject> CartasAtivas = new List<CartaScriptableObject>();

    public Carta CartaParaSpawnar;

    public int CustoComprarCarta = 2;

    public float EsperaEntreAsCompras = 0.25f;

    

    // Start is called before the first frame update
    void Start()
    {
        SetupDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    public void SetupDeck() // Configura o deck de cartas disponíveis para uso, distribuindo as cartas presentes na lista 'DeckParaUso', em uma lista temporária e, em seguida, transferindo-as para a lista 'CartasAtivas' de forma aleatória.
    {
        CartasAtivas.Clear();

        List<CartaScriptableObject> DeckTemporario = new List<CartaScriptableObject>();
        DeckTemporario.AddRange(DeckParaUso);

        int Iteracao = 0; //Para garantir seguranca caso o while de errado
        while (DeckTemporario.Count > 0 && Iteracao < 500)
        {
            int selected = Random.Range(0, DeckTemporario.Count);
            CartasAtivas.Add(DeckTemporario[selected]);
            DeckTemporario.RemoveAt(selected);

            Iteracao++;
        }
    }

    public void ComprarCarta()//compra uma carta, enviando ela para a mao
    {
        if (CartasAtivas.Count == 0)
        {
            SetupDeck();
        }

        Carta NovaCarta = Instantiate(CartaParaSpawnar, transform.position, transform.rotation);
        NovaCarta.CartaSO = CartasAtivas[0];
        NovaCarta.SetupCarta();

        CartasAtivas.RemoveAt(0);

        ControleDaMao.Referencia.AdicionarCartaNaMao(NovaCarta);

    }

    public void ComprarCartaPorMana() //compra cartas gastando mana
    {
        if (ControleDeBatalha.Referencia.ManaJogador >= CustoComprarCarta)
        {
            ComprarCarta();
            ControleDeBatalha.Referencia.GastarMana(CustoComprarCarta);
        }
        else
        {
            ControleUI.Referencia.MostrarAvisoSemMana();
            ControleUI.Referencia.BotaoComprarCarta.SetActive(false);
        }
    }

    public void ComprarVariasCartas(int QuantidadeParaComprar) // comprara varias cartas, com um leve intervalo de tempo
    {
        StartCoroutine(ComprarCartasCO(QuantidadeParaComprar));
    }

    IEnumerator ComprarCartasCO(int QuantidadeParaComprar)
    {
        for (int i = 0; i < QuantidadeParaComprar; i++)
        {
            ComprarCarta();

            yield return new WaitForSeconds(EsperaEntreAsCompras);
        }
    }
}
