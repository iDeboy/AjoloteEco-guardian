using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        
        SceneManager.LoadScene("Niveles");
    }
    public void Nivel1()
    {
        
        SceneManager.LoadScene("Nivel1");
    }
    public void Nivel2()
    {
        
        SceneManager.LoadScene("Nivel2");
    }
    public void Atras()
    {
        
        SceneManager.LoadScene("Inicio");
    }
    public void OpenOptions()
    {
        // Lógica para abrir el menú de opciones
        Debug.Log("Abrir Opciones");
    }

    public void QuitGame()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}
