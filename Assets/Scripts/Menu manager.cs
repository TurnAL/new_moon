using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menumanager : MonoBehaviour
{
    [SerializeField] GameObject PainelInicial;
    [SerializeField] GameObject PainelFinal;
    public void Jogar()
    {
       SceneManager.LoadScene(2); 
    }

    public void Options()
    {
        PainelInicial.SetActive(false);
        PainelFinal.SetActive(true);
    }

    public void CloseOptions()
    {
        PainelFinal.SetActive(false);
        PainelInicial.SetActive(true);
        
    }

    public void Exit()
    {
        Debug.Log("Saiu");
        Application.Quit();
    }

}
