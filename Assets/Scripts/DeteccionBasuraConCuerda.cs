using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DeteccionBasuraConCuerda : MonoBehaviour {
    private int _basuraRecogida = 0;

    [SerializeField]
    private float RangoDeteccion = 10f;
    [SerializeField]
    private KeyCode TeclaRecoger = KeyCode.E;
    [SerializeField]
    private float TiempoRecoger = 2f;
    [SerializeField]
    private bool MostrarRango = true;
    [SerializeField]
    private TMP_Text textoPuntos;

    [SerializeField]
    private Vector3 tamañoInicialIndicador = new Vector3(1f, 1f, 1f); // Tamaño inicial configurable

    private ObjetoPuntos objetoCercano;
    private GameObject indicadorActual; // Referencia al indicador del objeto cercano
    private float TiempoPrecionado = 0f;
    private int puntos = 0;

    private LineRenderer lineRenderer; // Referencia al LineRenderer

    public Material lineMaterial; // Asigna el material desde el inspector

    private Vector3 escalaOriginalIndicador; // Para almacenar la escala original del indicador

    private void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;

        if (lineMaterial != null) {
            lineRenderer.material = lineMaterial;
        }

        // Configuración de la línea
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    private void DetectarObjetoCercano() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, RangoDeteccion);

        float closestDistance = RangoDeteccion;
        ObjetoPuntos objetoMasCercano = null;

        foreach (Collider collider in hitColliders) {
            //if (collider.CompareTag("Recogible")) {
            if (collider.gameObject.TryGetComponent<ObjetoPuntos>(out var objetoPuntos)) {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance) {
                    closestDistance = distance;
                    objetoMasCercano = objetoPuntos/*collider.transform*/;
                }
            }
        }

        if (objetoMasCercano != objetoCercano) {
            // Cambió el objeto cercano, resetea el indicador actual
            if (indicadorActual != null) {
                ResetearIndicador();
            }

            objetoCercano = objetoMasCercano;

            // Activa el nuevo indicador si hay un objeto cercano
            if (objetoCercano != null) {
                indicadorActual = objetoCercano.gameObject.transform.Find("Indicador")?.gameObject;

                if (indicadorActual != null) {
                    escalaOriginalIndicador = indicadorActual.transform.localScale; // Guarda la escala original
                    indicadorActual.SetActive(true);
                    indicadorActual.transform.localScale = Vector3.Scale(escalaOriginalIndicador, tamañoInicialIndicador); // Aplica el tamaño inicial basado en la escala original
                }

                // Activar la cuerda
                lineRenderer.enabled = true;
            }
            else {
                // Desactivar la cuerda si no hay objeto cercano
                lineRenderer.enabled = false;
            }
        }
    }

    private void ActualizarCuerda() {
        if (lineRenderer.enabled && objetoCercano != null) {
            lineRenderer.SetPosition(0, transform.position); // Punto inicial: jugador
            lineRenderer.SetPosition(1, objetoCercano.gameObject.transform.position); // Punto final: objeto cercano
        }
    }

    private void Recoger() {
        if (objetoCercano != null) {
            Debug.Log($"Objeto recogido: {objetoCercano.name}");

            // Desactiva el indicador antes de destruir el objeto
            if (indicadorActual != null) {
                indicadorActual.SetActive(false);
            }

            puntos += objetoCercano.Puntos;

            textoPuntos.text = $"Puntos: {puntos}";

            Destroy(objetoCercano.gameObject);
            ++_basuraRecogida;
            Debug.Log($"Basura recogida: {_basuraRecogida}");

            // Desactivar la cuerda
            lineRenderer.enabled = false;
        }
    }

    private void ResetearIndicador() {
        if (indicadorActual != null) {
            indicadorActual.transform.localScale = escalaOriginalIndicador; // Restaura la escala original
            indicadorActual.SetActive(false); // Desactiva el indicador
            indicadorActual = null;
        }
    }

    private void ReiniciarPrecionado() {
        TiempoPrecionado = 0f;

        // Si hay un indicador activo, restaura su escala
        if (indicadorActual != null) {
            indicadorActual.transform.localScale = Vector3.Scale(escalaOriginalIndicador, tamañoInicialIndicador);
        }
    }

    private void OnDrawGizmosSelected() {
        if (!MostrarRango) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, RangoDeteccion);
    }

    private void Update() {
        DetectarObjetoCercano();
        ActualizarCuerda();

        if (objetoCercano != null && Input.GetKey(TeclaRecoger)) {
            TiempoPrecionado += Time.deltaTime;

            // Reduce el tamaño del indicador según el progreso de la recolección
            if (indicadorActual != null) {
                float progress = 1f - (TiempoPrecionado / TiempoRecoger);
                indicadorActual.transform.localScale = Vector3.Scale(escalaOriginalIndicador, tamañoInicialIndicador * progress);
            }

            if (TiempoPrecionado >= TiempoRecoger) {
                Recoger();
                ReiniciarPrecionado();
            }
        }
        else if (Input.GetKeyUp(TeclaRecoger) || objetoCercano == null) {
            ReiniciarPrecionado();
        }
    }
}
