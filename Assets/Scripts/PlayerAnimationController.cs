using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animation animationComponent; // Referencia al componente Animation
    public Rigidbody playerRigidbody;   // Referencia al Rigidbody del jugador (si usa Rigidbody)
    public string animationName = "Walking"; // Nombre de la animación

    private Vector3 lastPosition;

void Update()
{
    // Obtén la velocidad actual del Rigidbody
    Vector3 velocity = playerRigidbody.linearVelocity;
    
    // Solo toma en cuenta la velocidad en los ejes X y Z, ignora el eje Y (caída o salto)
    Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);
    bool isMoving = horizontalVelocity.magnitude > 0.1f;

    // Depuración
    //Debug.Log($"Velocity: {velocity}, HorizontalVelocity: {horizontalVelocity}, IsMoving: {isMoving}");

    // Controla la animación
    if (isMoving)
    {
        if (!animationComponent.IsPlaying(animationName))
        {
            Debug.Log("Reproduciendo animación...");
            animationComponent.Play(animationName);
        }
    }
    else
    {
        if (animationComponent.IsPlaying(animationName))
        {
            Debug.Log("Deteniendo animación...");
            animationComponent.Stop();
        }
    }
}


}
