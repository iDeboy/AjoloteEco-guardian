
using UnityEngine;

public class SeguimientoPlayer : MonoBehaviour
{
    public Transform player;                     // Referencia al jugador
    public Vector3 offset = new Vector3(0, 5, -10); // Offset de la cámara respecto al jugador
    public float smoothSpeed = 0.125f;           // Velocidad de suavizado de la cámara
    public float minDistance = 0.5f;             // Distancia mínima permitida desde el jugador
    public LayerMask collisionLayers;            // Capas que deben considerarse como obstáculos

    private Vector3 defaultOffset;

    void Start()
    {
        defaultOffset = offset;
    }

    void LateUpdate()
    {
        // Calcula la posición deseada aplicando la rotación del jugador al offset
        Vector3 rotatedOffset = player.rotation * offset;
        Vector3 desiredPosition = player.position + rotatedOffset;

        // Comprueba si hay algún obstáculo entre el jugador y la cámara
        RaycastHit hit;
        if (Physics.Raycast(player.position, rotatedOffset.normalized, out hit, rotatedOffset.magnitude, collisionLayers))
        {
            // Ajusta la posición de la cámara para que esté justo antes del obstáculo
            float adjustedDistance = hit.distance - minDistance;
            desiredPosition = player.position + rotatedOffset.normalized * Mathf.Max(adjustedDistance, minDistance);
        }

        // Suaviza la transición de la cámara para un movimiento fluido
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Actualiza la posición de la cámara
        transform.position = smoothedPosition;

        // La cámara mira siempre hacia el jugador
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}
