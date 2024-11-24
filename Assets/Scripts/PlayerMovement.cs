using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public bool isGrounded;
    public float mouseSensitivity = 100f; // Sensibilidad del mouse
    public float rotationSpeed = 10f;     // Velocidad de rotación suavizada

    private Rigidbody rb;
    private Transform playerBody;
    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // Evita que el Rigidbody gire.
        
        // Oculta el cursor y lo bloquea al centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;

        // Referencia del objeto principal del jugador para la rotación
        playerBody = transform;
    }

    void Update()
    {
        Move();
        Jump();
        Rotate();
    }

void Move()
{
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");

    // Determina si el jugador está corriendo o caminando
    float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

    // Calcula el movimiento en la dirección que está mirando el jugador
    Vector3 movement = (transform.forward * moveVertical + transform.right * moveHorizontal) * speed;

    // Aplica el movimiento al Rigidbody (actualiza la velocidad directamente)
    rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
}



    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

void Rotate()
{
    // No procesa la rotación si el cursor está desbloqueado (el juego está en pausa)
    if (Cursor.lockState != CursorLockMode.Locked)
        return;

    // Rotación en base a la entrada del mouse
    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

    // Rota el objeto jugador en el eje Y para girar a la derecha o izquierda
    float targetRotationY = playerBody.eulerAngles.y + mouseX;

    // Suaviza la rotación utilizando Slerp
    Quaternion targetRotation = Quaternion.Euler(0f, targetRotationY, 0f);
    playerBody.rotation = Quaternion.Slerp(playerBody.rotation, targetRotation, Time.deltaTime * rotationSpeed);
}


    private void OnCollisionEnter(Collision collision)
    {
        // Detecta si el jugador está en contacto con el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
