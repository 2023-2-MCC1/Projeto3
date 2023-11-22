using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public string BatalhaSelectScene;   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
       SceneManager.LoadScene("Dialogo");
    }
    public void ComecarBatalha()
    {
        SceneManager.LoadScene("Batalha");
    }

    public void VoltarParaOInicio()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void IrParaCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }
    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Saindo");
    }
}
