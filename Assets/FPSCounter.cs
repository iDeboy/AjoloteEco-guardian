using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText; // Asigna un objeto de texto en el Inspector
    private float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString();
    }
}
