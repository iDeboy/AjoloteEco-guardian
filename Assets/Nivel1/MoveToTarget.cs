using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Transform initialTarget;         // Objeto B (Primer destino)
    public Transform finalTarget;           // Objeto D (Destino final)
    public GameObject intermediateObject;   // Objeto C (Aparece al alcanzar el segundo destino)
    public GameObject objectToDisappear;    // Objeto que desaparece al alcanzar el segundo destino
    public float speed = 5f;                // Velocidad de movimiento
    public float arrivalDistance = 0.1f;    // Distancia mínima para considerar que llegó

    private Transform currentTarget;        // Objetivo actual del movimiento
    private bool hasReachedFirstTarget = false; // Controla si alcanzó el primer destino

    void Start()
    {
        // Asegúrate de que el objeto intermedio esté desactivado al inicio
        if (intermediateObject != null)
        {
            intermediateObject.SetActive(false);
        }

        // Comienza con el primer destino
        currentTarget = initialTarget;
    }

    void Update()
    {
        // Si no hay un objetivo definido, no hacer nada
        if (currentTarget == null) return;

        // Mueve el objeto actual hacia el objetivo actual
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, step);

        // Mira hacia el objetivo actual
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.1f);

        // Verifica si llegó al objetivo actual
        if (Vector3.Distance(transform.position, currentTarget.position) <= arrivalDistance)
        {
            if (!hasReachedFirstTarget)
            {
                // Si alcanzó el primer destino, cambia el objetivo al destino final
                hasReachedFirstTarget = true;
                currentTarget = finalTarget;
            }
            else
            {
                // Cuando alcanza el destino final
                if (intermediateObject != null)
                {
                    intermediateObject.SetActive(true); // Activa el objeto intermedio
                }

                if (objectToDisappear != null)
                {
                    objectToDisappear.SetActive(false); // Desactiva el objeto que debe desaparecer
                }

                gameObject.SetActive(false); // Desactiva el objeto inicial
            }
        }
    }
}
