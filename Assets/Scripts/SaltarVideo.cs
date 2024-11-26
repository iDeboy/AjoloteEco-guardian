using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SaltarVideo : MonoBehaviour {

    [SerializeField]
    private VideoPlayer video;

    [SerializeField]
    private string escena;

    [SerializeField]
    private int tiempoSaltar;

    [SerializeField]
    private KeyCode teclaSaltar = KeyCode.Escape;

    private float tiempoApretado = 0f;

    private void Awake() {

        video.Play();
        video.loopPointReached += Video_Terminado;

    }

    private void Video_Terminado(VideoPlayer source) {
        SceneManager.LoadScene(escena);
        Debug.Log("Cinemática terminada.");
    }

    private void SaltarCinematica() {
        video.Stop();
        SceneManager.LoadScene(escena);
        Debug.Log("Cinemática saltada.");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {

    }

    // Update is called once per frame
    private void Update() {

        if (Input.GetKey(teclaSaltar)) {

            tiempoApretado += Time.deltaTime; // Incrementa el tiempo al mantener la tecla
            if (tiempoApretado >= tiempoSaltar) {
                SaltarCinematica();
            }

        }
        else if (Input.GetKeyUp(teclaSaltar)) {
            tiempoApretado = 0f; // Reinicia el tiempo si suelta la tecla
        }

    }
}
