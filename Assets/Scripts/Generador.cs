using UnityEngine;

public class Generador : MonoBehaviour {


    [SerializeField]
    private int numero_objetos;

    [SerializeField]
    private GameObject objeto_generar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        var spawnAreaSize = meshRenderer.bounds.size - new Vector3(1, 0, 1);

        for (int i = 0; i < numero_objetos; ++i) {
            // Genera una posición aleatoria dentro del área especificada
            float xPosition = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
            float zPosition = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);
            var spawnPosition = new Vector3(xPosition, 0, zPosition);

            // Instancia el objeto en la posición generada relativa a `ObjectSpawner`
            var objeto_generado = Instantiate(objeto_generar, transform.position + spawnPosition, Quaternion.identity, transform);

            float altura_objeto = objeto_generado.GetComponent<Renderer>().bounds.extents.y;
            objeto_generado.transform.position = new Vector3(
                spawnPosition.x,
                transform.position.y + altura_objeto,
                spawnPosition.z
            );
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
