using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;                     // Referencia al jugador
    public Transform movingObject;               // Referencia al objeto en movimiento
    public Vector3 offset = new Vector3(0, 5, -10); // Offset de la cámara respecto al objetivo
    public float smoothSpeed = 0.125f;           // Velocidad de suavizado de la cámara
    public float minDistance = 0.5f;             // Distancia mínima permitida desde el objetivo
    public LayerMask collisionLayers;            // Capas que deben considerarse como obstáculos
    public GameObject objectToDisappear;         // Objeto que desaparece al final

    private Transform currentTarget;             // Objetivo actual de la cámara
    private bool isFocusingMovingObject = true;  // Indica si la cámara está enfocando el objeto en movimiento

    void Start()
    {
        // Comienza con el objeto en movimiento como objetivo
        currentTarget = movingObject;
    }

    void LateUpdate()
    {
        // Cambia el objetivo al jugador si el objeto a desaparecer ya no está activo
        if (isFocusingMovingObject && objectToDisappear != null && !objectToDisappear.activeSelf)
        {
            isFocusingMovingObject = false;
            currentTarget = player;
        }

        // Si no hay objetivo definido, no hacer nada
        if (currentTarget == null) return;

        // Calcula la posición deseada aplicando la rotación del objetivo actual al offset
        Vector3 rotatedOffset = currentTarget.rotation * offset;
        Vector3 desiredPosition = currentTarget.position + rotatedOffset;

        // Comprueba si hay obstáculos entre el objetivo actual y la cámara
        RaycastHit hit;
        if (Physics.Raycast(currentTarget.position, rotatedOffset.normalized, out hit, rotatedOffset.magnitude, collisionLayers))
        {
            // Ajusta la posición de la cámara para evitar el obstáculo
            float adjustedDistance = hit.distance - minDistance;
            desiredPosition = currentTarget.position + rotatedOffset.normalized * Mathf.Max(adjustedDistance, minDistance);
        }

        // Suaviza la transición de la cámara para un movimiento fluido
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Actualiza la posición de la cámara
        transform.position = smoothedPosition;

        // La cámara mira siempre hacia el objetivo actual
        transform.LookAt(currentTarget.position + Vector3.up * 1.5f);
    }
}
