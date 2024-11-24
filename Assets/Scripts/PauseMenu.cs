using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Referencia al menú de pausa
    public bool isPaused = false;  // Indica si el juego está en pausa
    private AudioSource[] audioSources; // Lista de todas las fuentes de audio

    void Start()
    {
        // Obtén todas las fuentes de audio en la escena al inicio
        audioSources = FindObjectsOfType<AudioSource>();
    }

    void Update()
    {
        // Activa o desactiva el menú de pausa al presionar "Escape"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Oculta el menú de pausa
        Time.timeScale = 1f;          // Reactiva el tiempo del juego
        isPaused = false;             // Indica que el juego ya no está en pausa

        // Reactiva las fuentes de audio
        foreach (AudioSource audio in audioSources)
        {
            if (audio != null && audio.isPlaying == false)
            {
                audio.Play();
            }
        }

        // Bloquea y oculta el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);  // Muestra el menú de pausa
        Time.timeScale = 0f;          // Detiene el tiempo del juego
        isPaused = true;              // Indica que el juego está en pausa

        // Pausa todas las fuentes de audio
        foreach (AudioSource audio in audioSources)
        {
            if (audio != null && audio.isPlaying)
            {
                audio.Pause();
            }
        }

        // Desbloquea y muestra el cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Asegúrate de reactivar el tiempo antes de cambiar de escena
        SceneManager.LoadScene("Inicio"); // Cambia a la escena del menú principal
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Solo funciona en builds
    }
}
