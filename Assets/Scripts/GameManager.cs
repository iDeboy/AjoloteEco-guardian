using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

using Math = System.Math;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

    private bool _terminado = false;
    private float _tiempoRestante;
    private bool _activo = true;

    [SerializeField]
    private float tiempoTotal = 60f;

    [SerializeField]
    private TMP_Text textoTiempo;

    [SerializeField]
    private Canvas menuPausa;

    [SerializeField]
    private MoveToTarget moveToTarget;

    private bool _isPause;
    private AudioSource[] audioSources;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (moveToTarget != null)
            _activo = moveToTarget.IsFinished;

        else _activo = true;

        _tiempoRestante = tiempoTotal;
        audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.InstanceID);
    }

    // Update is called once per frame
    void Update() {

        if (_terminado) return;

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (_isPause) {
                Resume();
            }
            else {
                Pause();
            }
        }

        if (_isPause) return;

        if (moveToTarget != null)
            _activo = moveToTarget.IsFinished;

        if (_activo) {
            textoTiempo.gameObject.SetActive(true);
            // Reducir el tiempo basado en el tiempo transcurrido
            _tiempoRestante -= Time.deltaTime;

            // Evitar que el tiempo sea negativo
            if (_tiempoRestante <= 0) {
                _tiempoRestante = 0;
                _activo = false; // Detiene el temporizador
                Terminar(); // Acción al terminar el tiempo
            }

            ActualizarTexto(); // Actualiza la interfaz de usuario

        }
        else {
            textoTiempo.gameObject.SetActive(false);
        }



    }

    private void ActualizarTexto() {

        if (textoTiempo == null) return;

        // Muestra el tiempo en formato mm:ss
        int minutes = Math.DivRem((int)_tiempoRestante, 60, out var seconds);

        textoTiempo.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void Terminar() {
        Time.timeScale = 0f;
        // Lógica al terminar el tiempo (ejemplo: mostrar Game Over)
        _terminado = true;

        Debug.Log("¡Se acabó el tiempo!");
        // Puedes llamar aquí una función para reiniciar la escena o mostrar un menú.
    }

    public void Resume() {
        menuPausa.gameObject.SetActive(false); // Oculta el menú de pausa
        Time.timeScale = 1f;          // Reactiva el tiempo del juego
        _isPause = false;             // Indica que el juego ya no está en pausa

        // Reactiva las fuentes de audio
        foreach (AudioSource audio in audioSources) {
            if (audio != null && audio.isPlaying == false) {
                audio.Play();
            }
        }

        // Bloquea y oculta el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause() {
        menuPausa.gameObject.SetActive(true);  // Muestra el menú de pausa
        Time.timeScale = 0f;          // Detiene el tiempo del juego
        _isPause = true;              // Indica que el juego está en pausa

        // Pausa todas las fuentes de audio
        foreach (AudioSource audio in audioSources) {
            if (audio != null && audio.isPlaying) {
                audio.Pause();
            }
        }

        // Desbloquea y muestra el cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMainMenu() {
        Time.timeScale = 1f; // Asegúrate de reactivar el tiempo antes de cambiar de escena
        SceneManager.LoadScene("Inicio"); // Cambia a la escena del menú principal
    }

    public void QuitGame() {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Solo funciona en builds
    }
}
