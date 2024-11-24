using UnityEngine;

public class IndicadorRotacion : MonoBehaviour
{
    [SerializeField]
    private float velocidadRotacion = 50f; // Velocidad de rotación en grados por segundo

    void Update()
    {
        // Gira el indicador alrededor del eje Y
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }
}
