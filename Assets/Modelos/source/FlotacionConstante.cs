using UnityEngine;

public class FlotacionConstante  : MonoBehaviour
{
    public float alturaDeseada = 2f; // Altura en la que quieres que flote
    public float fuerzaDeFlotacion = 15f; // Ajusta la fuerza de flotación
    public float damping = 0.8f; // Factor de amortiguación para suavizar

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // Mantener gravedad activa
    }

    void FixedUpdate()
    {
        // Calcula la diferencia entre la altura deseada y la posición actual
        float diferenciaAltura = alturaDeseada - transform.position.y;

        // Solo aplica fuerza de flotación si el objeto está por debajo de la altura deseada
        if (diferenciaAltura > 0)
        {
            // Aplica fuerza de flotación proporcional a la diferencia de altura
            Vector3 fuerza = Vector3.up * diferenciaAltura * fuerzaDeFlotacion;

            // Agrega una amortiguación para evitar que se pase
            fuerza -= rb.linearVelocity * damping;

            rb.AddForce(fuerza, ForceMode.Acceleration);
        }
    }
}
