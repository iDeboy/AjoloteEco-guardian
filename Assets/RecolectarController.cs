using UnityEngine;
using UnityEngine.InputSystem;

public class RecolectarController : MonoBehaviour
{
    private bool recolectando = false;

    private void Update()
    {
        if (recolectando)
        {
            // Ejecutar lógica de recolección
            Debug.Log("Recolectando...");
        }
    }

    public void OnRecolectar(InputAction.CallbackContext context)
    {
        if (context.started) recolectando = true;
        if (context.canceled) recolectando = false;
    }
}
