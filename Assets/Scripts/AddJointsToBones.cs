using UnityEngine;

public class AddJointsToBones : MonoBehaviour
{
    public Transform rootBone;

    void Start()
    {
        AddJoints(rootBone);
    }

    void AddJoints(Transform bone)
    {
        foreach (Transform child in bone)
        {
            ConfigurableJoint joint = child.gameObject.AddComponent<ConfigurableJoint>();
            joint.connectedBody = bone.GetComponent<Rigidbody>();

            // Configuración básica del Joint
            joint.xMotion = ConfigurableJointMotion.Limited;
            joint.yMotion = ConfigurableJointMotion.Limited;
            joint.zMotion = ConfigurableJointMotion.Limited;

            // Ajusta los límites según tus necesidades
            SoftJointLimit limit = new SoftJointLimit();
            limit.limit = 0.5f;
            joint.linearLimit = limit;

            AddJoints(child);
        }
    }
}
