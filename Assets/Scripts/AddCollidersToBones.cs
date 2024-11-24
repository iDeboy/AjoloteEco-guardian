using UnityEngine;

public class AddCollidersToBones : MonoBehaviour
{
    public GameObject rootBone; // Asigna el hueso raíz del esqueleto aquí
    public float colliderRadius = 0.05f; // Radio del Capsule Collider
    public float colliderHeightFactor = 0.8f; // Factor de altura basado en la distancia al hijo más cercano

    void Start()
    {
        if (rootBone == null)
        {
            Debug.LogError("Por favor, asigna el hueso raíz en el inspector.");
            return;
        }

        // Añade colliders a los huesos
        AddColliders(rootBone.transform);
    }

    void AddColliders(Transform bone)
{
    foreach (Transform child in bone)
    {
        Debug.Log("Añadiendo collider al hueso: " + bone.name); // Esto te ayuda a confirmar si detecta los huesos.

        float distanceToChild = Vector3.Distance(bone.position, child.position);

        CapsuleCollider collider = bone.gameObject.AddComponent<CapsuleCollider>();
        collider.radius = colliderRadius;
        collider.height = distanceToChild * colliderHeightFactor;
        collider.direction = 2;

        AddColliders(child);
    }
}

}
