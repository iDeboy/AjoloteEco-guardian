using UnityEngine;

public class Orbiter : MonoBehaviour
{
    public Transform player;             // Referencia al jugador
    public float orbitDistance = 3f;     // Distancia horizontal al jugador
    public float orbitSpeed = 50f;       // Velocidad de órbita (grados por segundo)
    public float orbitHeight = 1f;       // Altura sobre el jugador
    public float followSpeed = 2f;       // Velocidad a la que sigue al jugador
    public float rotationSpeed = 10f;    // Velocidad de rotación del objeto para sincronizar
    public LayerMask obstacleLayers;     // Capas de objetos que se consideran obstáculos
    public float avoidDistance = 0.5f;   // Distancia para evitar colisiones

    private Vector3 currentDirection;    // Dirección actual del objeto en el plano horizontal

    void Start()
    {
        // Inicializa la dirección en un punto fijo alrededor del jugador en el plano XZ
        Vector3 horizontalDirection = (transform.position - player.position);
        horizontalDirection.y = 0; // Ignora la altura inicial
        currentDirection = horizontalDirection.normalized;
    }

    void Update()
    {
        FollowPlayer();
        OrbitAroundPlayer();
        AvoidCollisions();
        RotateTowardsOrbit();
    }

    void FollowPlayer()
    {
        // Calcula la posición objetivo en el plano horizontal
        Vector3 targetPosition = player.position + currentDirection * orbitDistance;

        // Ajusta la altura del objeto
        targetPosition.y = player.position.y + orbitHeight;

        // Mueve suavemente al objeto hacia la posición objetivo
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    void OrbitAroundPlayer()
    {
        // Calcula la rotación alrededor del jugador en el plano horizontal
        Quaternion rotation = Quaternion.AngleAxis(orbitSpeed * Time.deltaTime, Vector3.up);
        currentDirection = rotation * currentDirection;
    }

    void AvoidCollisions()
    {
        // Realiza un Raycast hacia la posición objetivo
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, currentDirection, out RaycastHit hit, orbitDistance, obstacleLayers))
        {
            // Si hay un obstáculo, ajusta la posición para evitarlo
            Vector3 avoidDirection = Vector3.Cross(Vector3.up, directionToPlayer).normalized; // Desviación lateral
            transform.position += avoidDirection * avoidDistance;
        }
    }
void RotateTowardsOrbit()
{
    Vector3 lookDirection = (player.position - transform.position).normalized;
    if (lookDirection.sqrMagnitude > 0.001f)
    {
        transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }
}



    private void OnDrawGizmos()
    {
        // Dibuja una esfera alrededor del jugador para visualizar la órbita
        if (player != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(player.position + Vector3.up * orbitHeight, orbitDistance);
        }
    }
}
