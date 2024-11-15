using UnityEngine;

public class DeteccionBasura : MonoBehaviour {

    private int _basuraRecogida = 0;

    [SerializeField]
    private float RangoDeteccion = 2f;
    [SerializeField]
    private KeyCode TeclaRecoger = KeyCode.E;
    [SerializeField]
    private float TiempoRecoger = 3f;
    [SerializeField]
    private bool MostrarRango = false;

    private Transform objetoCercano;
    private float TiempoPrecionado = 0f;

    private void DetectarObjetoCercano() {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, RangoDeteccion);

        float closestDistance = RangoDeteccion;
        objetoCercano = null;

        foreach (Collider collider in hitColliders) {

            if (collider.CompareTag("Recogible")) {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance) {
                    closestDistance = distance;
                    objetoCercano = collider.transform;
                }
            }
        }
    }

    private void Recoger() {
        if (objetoCercano != null) {
            Debug.Log($"Objeto recogido: {objetoCercano.name}");
            Destroy(objetoCercano.gameObject);
            ++_basuraRecogida;
            Debug.Log($"Basura recogida: {_basuraRecogida}");
        }
    }

    // Resetea el temporizador
    void ReiniciarPrecionado() {
        TiempoPrecionado = 0f;
    }

    private void OnDrawGizmosSelected() {

        if (!MostrarRango) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, RangoDeteccion);
    }

    // Update is called once per frame
    private void Update() {

        DetectarObjetoCercano();

        if (objetoCercano != null && Input.GetKey(TeclaRecoger)) {
            TiempoPrecionado += Time.deltaTime;

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
