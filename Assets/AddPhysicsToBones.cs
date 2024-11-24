using UnityEngine;

public class AddPhysicsToBones : MonoBehaviour
{
    public Transform rootBone;  // El armature o hueso raíz de tu modelo
    public float colliderRadius = 0.1f; // Radio del CapsuleCollider

    void Start()
    {
        if (rootBone == null)
        {
            Debug.LogError("RootBone no está asignado en el inspector.");
            return;
        }

        // Inicia la adición de física desde los hijos del armature
        foreach (Transform child in rootBone)
        {
            AddPhysics(child);
        }
    }

    void AddPhysics(Transform bone)
    {
        Debug.Log($"Añadiendo física al hueso: {bone.name}");

        // Añade Rigidbody
        Rigidbody rb = bone.gameObject.AddComponent<Rigidbody>();
        rb.mass = 0.1f;

        // Añade CapsuleCollider
        CapsuleCollider collider = bone.gameObject.AddComponent<CapsuleCollider>();
        collider.radius = colliderRadius;

        // Si el hueso tiene hijos, ajusta la altura del CapsuleCollider
        if (bone.childCount > 0)
        {
            Transform childBone = bone.GetChild(0);
            Vector3 direction = childBone.position - bone.position;
            float distance = direction.magnitude;

            collider.height = distance + colliderRadius * 2;
            collider.direction = 2; // Eje Z
            collider.center = new Vector3(0, 0, distance / 2);
        }
        else
        {
            // Si no tiene hijos, establece valores predeterminados
            collider.height = colliderRadius * 2;
            collider.center = Vector3.zero;
        }

        // Recorre recursivamente los hijos
        foreach (Transform child in bone)
        {
            AddPhysics(child);
        }
    }
}
