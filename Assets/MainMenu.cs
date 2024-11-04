using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Cambia "NombreEscenaJuego" por el nombre de tu escena del juego
        SceneManager.LoadScene("Juego");
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
