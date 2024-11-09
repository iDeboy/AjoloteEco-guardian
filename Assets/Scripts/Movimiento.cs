using System;
using UnityEngine;

public class Movimiento : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private float mouseSensitivity = 100f;
    [SerializeField]
    private float verticalSpeed = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {

        MoverCamara();

        RotarCamara();

    }

    private void MoverCamara() {

        Vector3 moveDirection = Vector3.zero;

        // Movimiento en el eje horizontal (izquierda y derecha)
        if (Input.GetKey(KeyCode.A)) {
            moveDirection -= transform.right;
        }
        if (Input.GetKey(KeyCode.D)) {
            moveDirection += transform.right;
        }

        // Movimiento en el eje vertical (adelante y atr�s)
        if (Input.GetKey(KeyCode.W)) {
            moveDirection += transform.forward;
        }
        if (Input.GetKey(KeyCode.S)) {
            moveDirection -= transform.forward;
        }

        // Movimiento en el eje de elevaci�n (espacio para subir, shift para bajar)
        if (Input.GetKey(KeyCode.Space)) {
            moveDirection += Vector3.up;
        }
        if (Input.GetKey(KeyCode.LeftShift)) {
            moveDirection += Vector3.down;
        }

        // Aplicar el movimiento usando la velocidad y el tiempo
        transform.position += moveSpeed * Time.deltaTime * moveDirection.normalized;
    }

    private void RotarCamara() {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX, Space.World);
        transform.Rotate(Vector3.right * -Mathf.Clamp(mouseY, -90f, 90f));

    }

}
