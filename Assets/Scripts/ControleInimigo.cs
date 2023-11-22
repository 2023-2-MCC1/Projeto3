using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleInimigo : MonoBehaviour
{

    public static ControleInimigo Referencia;
    private void Awake()
    {
        Referencia = this;
    }

    public List<CartaScriptableObject> DeckParaUso = new List<CartaScriptableObject>();
    private List<CartaScriptableObject> CartasAtivas = new List<CartaScriptableObject>();

    public Carta CartaParaSpawnar;
    public Transform CartaPontoDeSpawn;

    public enum AIType { ModoMedio, ModoAleatorio, ModoAgressivo, ModoDefensivo }
    public AIType IAModo;

    private List<CartaScriptableObject> CartasNaMao = new List<CartaScriptableObject>();
    public int TamanhoDaMaoInicial;


    // Start is called before the first frame update
    void Start()
    {
        SetupDeck();
        if (IAModo != AIType.ModoMedio)
        {
            SetupMao();
        }

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

    public void ComecarAcao()
    {
        {
            StartCoroutine(AcaoInimigoCO());
        }

        IEnumerator AcaoInimigoCO()
        {
            if (CartasAtivas.Count == 0)
            {
                SetupDeck();
            }

            yield return new WaitForSeconds(.5f);

            if (IAModo != AIType.ModoMedio)
            {
                for (int i = 0; i < ControleDeBatalha.Referencia.CartasParaComprarPorTurno; i++)
                {
                    CartasNaMao.Add(CartasAtivas[0]);
                    CartasAtivas.RemoveAt(0);

                    if (CartasAtivas.Count == 0)
                    {
                        SetupDeck();
                    }
                }
            }

            List<CartasPontos> cartasPontos = new List<CartasPontos>();
            cartasPontos.AddRange(CartaPontosControle.Referencia.CartaPontosInimigos);

            int PontoAleatorio = Random.Range(0, cartasPontos.Count);

            CartasPontos PontoEscolhido = cartasPontos[PontoAleatorio];

            if (IAModo == AIType.ModoMedio || IAModo == AIType.ModoAleatorio)
            {
                cartasPontos.Remove(PontoEscolhido);

                while (PontoEscolhido.CartaAtiva != null && cartasPontos.Count > 0)
                {
                    PontoAleatorio = Random.Range(0, cartasPontos.Count);
                    PontoEscolhido = cartasPontos[PontoAleatorio];
                    cartasPontos.RemoveAt(PontoAleatorio);
                }
            }

            CartaScriptableObject CartaEscolhida = null;
            int iteracao = 0;
            List<CartasPontos> PontosPreferidos= new List<CartasPontos>(); //locais em que o modo ira preferir jogar
            List<CartasPontos> PontosSecundarios = new List<CartasPontos>(); //locais em que, caso o primario nao possa ser jogado, jogara neste


            switch (IAModo)
            {
                case AIType.ModoMedio:

                    if (PontoEscolhido.CartaAtiva == null)
                    {
                        Carta NovaCarta = Instantiate(CartaParaSpawnar, CartaPontoDeSpawn.position, CartaPontoDeSpawn.rotation);

                        NovaCarta.CartaSO = CartasAtivas[0];
                        CartasAtivas.RemoveAt(0);
                        NovaCarta.SetupCarta();
                        NovaCarta.MoverParaOPonto(PontoEscolhido.transform.position,
                            PontoEscolhido.transform.rotation);
                        PontoEscolhido.CartaAtiva = NovaCarta;
                        NovaCarta.LugarMarcado = PontoEscolhido;
                    }

                    break;

                case AIType.ModoAleatorio:

                    CartaEscolhida = CartaEscolhidaParaJogar();

                    iteracao = 50;
                    while (CartaEscolhida != null && iteracao > 0 && PontoEscolhido.CartaAtiva == null)
                    {

                        JogarCarta(CartaEscolhida, PontoEscolhido);

                        //confere se é possivel jogar outra carta
                        CartaEscolhida = CartaEscolhidaParaJogar();

                        iteracao--;

                        yield return new WaitForSeconds(CartaPontosControle.Referencia.TempoEntreAtaques);

                        while (PontoEscolhido.CartaAtiva != null && cartasPontos.Count > 0)
                        {
                            PontoAleatorio = Random.Range(0, cartasPontos.Count);
                            PontoEscolhido = cartasPontos[PontoAleatorio];
                            cartasPontos.RemoveAt(PontoAleatorio);
                        }
                    }
                    break;

                case AIType.ModoAgressivo: //Vai priorizar jogar as cartas em lugares em que voce ainda não posicionou suas cartas

                    CartaEscolhida = CartaEscolhidaParaJogar();

                    PontosPreferidos.Clear();
                    PontosSecundarios.Clear();

                    for (int i = 0; i < cartasPontos.Count; i++)
                    {
                        if (cartasPontos[i].CartaAtiva == null)
                        {
                            if (CartaPontosControle.Referencia.CartaPontosJogador[i].CartaAtiva != null)
                            {
                                PontosPreferidos.Add(cartasPontos[i]);
                            }
                            else
                            {
                                PontosSecundarios.Add(cartasPontos[i]);
                            }
                        }
                    }



                    iteracao = 50;
                    while (PontoEscolhido != null && iteracao > 0 && PontosPreferidos.Count + PontosSecundarios.Count > 0)
                    {
                        
                        if (PontosPreferidos.Count > 0)
                        {
                            int EscolherPonto = Random.Range(0, PontosPreferidos.Count);
                            PontoEscolhido = PontosPreferidos[EscolherPonto];

                            PontosPreferidos.RemoveAt(EscolherPonto);
                        }
                        else
                        {
                            int selectPoint = Random.Range(0, PontosSecundarios.Count);
                            PontoEscolhido = PontosSecundarios[selectPoint];

                            PontosSecundarios.RemoveAt(selectPoint);
                        }
                    }

                    JogarCarta(CartaEscolhida, PontoEscolhido);

                    //Confere se pode jogar outra
                    CartaEscolhida = CartaEscolhidaParaJogar();
                    iteracao--;

                    yield return new WaitForSeconds(CartaPontosControle.Referencia.TempoEntreAtaques);



                    break;


                case AIType.ModoDefensivo: //Sua prioridade é atacar onde voce jogou cartas, para poupar vida

                    CartaEscolhida = CartaEscolhidaParaJogar();

                    PontosPreferidos.Clear();
                    PontosSecundarios.Clear();

                    for (int i = 0; i < cartasPontos.Count; i++)
                    {
                        if (cartasPontos[i].CartaAtiva == null)
                        {
                            if (CartaPontosControle.Referencia.CartaPontosJogador[i].CartaAtiva == null)
                            {
                                PontosPreferidos.Add(cartasPontos[i]);
                            }
                            else
                            {
                                PontosSecundarios.Add(cartasPontos[i]);
                            }
                        }
                    }



                    iteracao = 50;
                    while (PontoEscolhido != null && iteracao > 0 && PontosPreferidos.Count + PontosSecundarios.Count > 0)
                    {

                        if (PontosPreferidos.Count > 0)
                        {
                            int EscolherPonto = Random.Range(0, PontosPreferidos.Count);
                            PontoEscolhido = PontosPreferidos[EscolherPonto];

                            PontosPreferidos.RemoveAt(EscolherPonto);
                        }
                        else
                        {
                            int selectPoint = Random.Range(0, PontosSecundarios.Count);
                            PontoEscolhido = PontosSecundarios[selectPoint];

                            PontosSecundarios.RemoveAt(selectPoint);
                        }
                    }

                    JogarCarta(CartaEscolhida, PontoEscolhido);

                    //Confere se pode jogar outra
                    CartaEscolhida = CartaEscolhidaParaJogar();
                    iteracao--;

                    yield return new WaitForSeconds(CartaPontosControle.Referencia.TempoEntreAtaques);


                    break;

            }
            yield return new WaitForSeconds(.3f);

            ControleDeBatalha.Referencia.AvancarTurno();
        }
    }

    void SetupMao()
    {
        for (int i = 0; i < TamanhoDaMaoInicial; i++)
        {
            if (CartasAtivas.Count == 0)
            {
                SetupDeck();
            }

            CartasNaMao.Add(CartasAtivas[0]);
            CartasAtivas.RemoveAt(0);
        }
    }

    public void JogarCarta(CartaScriptableObject CartaSO, CartasPontos pontos)
    {
        Carta NovaCarta = Instantiate(CartaParaSpawnar, CartaPontoDeSpawn.position, CartaPontoDeSpawn.rotation);

        NovaCarta.CartaSO = CartaSO;

        NovaCarta.SetupCarta();
        NovaCarta.MoverParaOPonto(pontos.transform.position,
            pontos.transform.rotation);
        pontos.CartaAtiva = NovaCarta;
        NovaCarta.LugarMarcado = pontos;

        CartasNaMao.Remove(CartaSO);

        ControleDeBatalha.Referencia.GastarManaInimigo(CartaSO.CustoDeMana);


    }

    CartaScriptableObject CartaEscolhidaParaJogar()
    {
        CartaScriptableObject CartaParaJogar = null;

        List<CartaScriptableObject> CartasParaJogar = new List<CartaScriptableObject>();
        foreach (CartaScriptableObject Carta in CartasNaMao)
        {
            if (Carta.CustoDeMana <= ControleDeBatalha.Referencia.ManaInimigo)
            {
                CartasParaJogar.Add(Carta);
            }
        }
        if (CartasParaJogar.Count > 0)
        {
            int Escolhida = Random.Range(0, CartasParaJogar.Count);

            CartaParaJogar = CartasParaJogar[Escolhida];
        }


        return CartaParaJogar;

    }
}


