using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;                // Referencia al objeto del jugador
    public Vector3 offset;                  // Offset para la posición inicial de seguimiento de la cámara
    public Vector3 alternateOffset;         // Offset específico para la posición alternativa al presionar 'R'
    public float followSpeed = 10f;         // Velocidad de seguimiento de la cámara
    public float rotationSpeed = 5f;        // Velocidad de rotación de la cámara hacia el jugador
    public float mouseSensitivity = 100f;   // Sensibilidad del movimiento del mouse

    private bool inAlternatePosition = false;  // Indica si la cámara está en la posición alternativa
    private float pitch = 0f;               // Ángulo de rotación en el eje X
    private float yaw = 0f;                 // Ángulo de rotación en el eje Y

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Inicializa la posición de la cámara en relación al jugador usando el offset
        transform.position = player.position + offset;

        // Inicializa la rotación de la cámara basándose en su orientación actual
        Vector3 angles = transform.eulerAngles;
        pitch = angles.x;
        yaw = angles.y;
    }

    void LateUpdate()
    {
        // Cambia la posición de la cámara cuando se presiona 'R'
        if (Input.GetKeyDown(KeyCode.R))
        {
            inAlternatePosition = !inAlternatePosition;
        }

        // Determina el comportamiento según la posición actual
        if (inAlternatePosition)
        {
            FollowPlayer(alternateOffset);
            RotateWithMouse();
        }
        else
        {
            FollowPlayer(offset);
            RotateWithMouse();
        }
    }

    void FollowPlayer(Vector3 currentOffset)
    {
        // Define la posición deseada de la cámara en relación al jugador usando el offset actual
        Vector3 targetPosition = player.position + currentOffset;
        
        // Suaviza el movimiento de la cámara hacia la posición objetivo
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    void RotateWithMouse()
    {
        // Obtener el movimiento del mouse
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Limita el ángulo de pitch para evitar que la cámara gire completamente
        pitch = Mathf.Clamp(pitch, -30f, 60f);  // Ajusta estos valores para limitar el movimiento vertical

        // Aplica la rotación a la cámara
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
