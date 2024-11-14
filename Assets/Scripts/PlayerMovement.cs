using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public bool isGrounded;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Determina si el jugador está corriendo o caminando
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
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
