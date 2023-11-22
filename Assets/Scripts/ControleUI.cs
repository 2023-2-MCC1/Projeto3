using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ControleUI : MonoBehaviour
{
    public static ControleUI Referencia;

    private void Awake()
    {
        Referencia = this;
    }

    public TMP_Text JogadorManaTexto, JogadorVidaTexto, InimigoVidaTexto, InimigoManaTexto;

    public GameObject AvisoSemMana;
    public float AvisoSemManaTempo;
    private float AvisoSemManaContagem;
    public GameObject BotaoComprarCarta;
    public GameObject BotaoTerminarTurno;
    public string MenuPrincipalScene, BatalhaSelectScene;

 public GameObject BatalhaEncerradaCena;
 public TMP_Text BatalhaResultadoTexto;
   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AvisoSemManaContagem > 0) //Ao tentar jogar uma carta cujo custo de mana seja maior que sua mana atual, aparece um aviso.
        {
            AvisoSemManaContagem -= Time.deltaTime;

            if (AvisoSemManaContagem <= 0)
            {
                AvisoSemMana.SetActive(false);
            }
        }
    }

    public void SetarManaTexto(int QuantidadeDeMana)
    {
        JogadorManaTexto.text = "MANA " + QuantidadeDeMana;
    }
    public void SetarManaTextoInimigo(int QuantidadeDeMana)
    {
        InimigoManaTexto.text = "MANA " + QuantidadeDeMana;
    }

    public void MostrarAvisoSemMana()
    {
        AvisoSemMana.SetActive(true);
        AvisoSemManaContagem= AvisoSemManaTempo;
    }

    public void ComprandoCarta()
    {
        ControleDeck.Referencia.ComprarCartaPorMana();

    }

    public void TerminarTurno()
    {
        ControleDeBatalha.Referencia.TerminarTurno();
    }

    public void SetarVidaTextoJogador(int QuantidadeDeVida)
    {
        JogadorVidaTexto.text = "Vida do Jogador : " + QuantidadeDeVida;
    }

    public void SetarVidaTextoInimigo(int QuantidadeDeVida)
    {
        InimigoVidaTexto.text = "Vida do Inimigo: " + QuantidadeDeVida;
    }
    public void MenuPrincipal()
    {
        SceneManager.LoadScene(MenuPrincipalScene);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
   
}
