using UnityEngine;

public class DeteccionBasura : MonoBehaviour
{
    private int _basuraRecogida = 0;

    [SerializeField]
    private float RangoDeteccion = 2f;
    [SerializeField]
    private KeyCode TeclaRecoger = KeyCode.E;
    [SerializeField]
    private float TiempoRecoger = 3f;
    [SerializeField]
    private bool MostrarRango = false;

    [SerializeField]
    private Vector3 tamañoInicialIndicador = new Vector3(1f, 1f, 1f); // Tamaño inicial configurable

    private Transform objetoCercano;
    private GameObject indicadorActual; // Referencia al indicador del objeto cercano
    private float TiempoPrecionado = 0f;

    private void DetectarObjetoCercano()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, RangoDeteccion);

        float closestDistance = RangoDeteccion;
        Transform objetoMasCercano = null;

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Recogible"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    objetoMasCercano = collider.transform;
                }
            }
        }

        if (objetoMasCercano != objetoCercano)
        {
            // Cambió el objeto cercano, resetea el indicador actual
            if (indicadorActual != null)
            {
                ResetearIndicador();
            }

            objetoCercano = objetoMasCercano;

            // Activa el nuevo indicador si hay un objeto cercano
            if (objetoCercano != null)
            {
                indicadorActual = objetoCercano.Find("Indicador")?.gameObject;
                if (indicadorActual != null)
                {
                    indicadorActual.SetActive(true);
                    indicadorActual.transform.localScale = tamañoInicialIndicador; // Reinicia al tamaño configurado
                }
            }
        }
    }

    private void Recoger()
    {
        if (objetoCercano != null)
        {
            Debug.Log($"Objeto recogido: {objetoCercano.name}");

            // Desactiva el indicador antes de destruir el objeto
            if (indicadorActual != null)
            {
                indicadorActual.SetActive(false);
            }

            Destroy(objetoCercano.gameObject);
            ++_basuraRecogida;
            Debug.Log($"Basura recogida: {_basuraRecogida}");
        }
    }

    private void ResetearIndicador()
    {
        if (indicadorActual != null)
        {
            indicadorActual.transform.localScale = tamañoInicialIndicador; // Restaura la escala inicial
            indicadorActual.SetActive(false); // Desactiva el indicador
            indicadorActual = null;
        }
    }

    void ReiniciarPrecionado()
    {
        TiempoPrecionado = 0f;

        // Si hay un indicador activo, restaura su escala
        if (indicadorActual != null)
        {
            indicadorActual.transform.localScale = tamañoInicialIndicador;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!MostrarRango) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, RangoDeteccion);
    }

    private void Update()
    {
        DetectarObjetoCercano();

        if (objetoCercano != null && Input.GetKey(TeclaRecoger))
        {
            TiempoPrecionado += Time.deltaTime;

            // Reduce el tamaño del indicador según el progreso de la recolección
            if (indicadorActual != null)
            {
                float progress = 1f - (TiempoPrecionado / TiempoRecoger);
                indicadorActual.transform.localScale = tamañoInicialIndicador * progress;
            }

            if (TiempoPrecionado >= TiempoRecoger)
            {
                Recoger();
                ReiniciarPrecionado();
            }
        }
        else if (Input.GetKeyUp(TeclaRecoger) || objetoCercano == null)
        {
            ReiniciarPrecionado();
        }
    }
}
