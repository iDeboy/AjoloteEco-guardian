using UnityEngine;

public class GeneradorBasura : MonoBehaviour {

    [SerializeField]
    private Terrain Terreno;

    [SerializeField]
    private int numeroObjetos = 10;

    [SerializeField]
    private float escala = 0.75f;

    [SerializeField]
    private GameObject objetoGenerar;

    private float terrenoXMin;
    private float terrenoXMax;
    private float terrenoZMin;
    private float terrenoZMax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

        terrenoXMin = Terreno.transform.position.x;
        terrenoXMax = terrenoXMin + Terreno.terrainData.size.x;
        terrenoZMin = Terreno.transform.position.z;
        terrenoZMax = terrenoZMin + Terreno.terrainData.size.z;

        GenerarObjetos();
    }

    private void GenerarObjetos() {
        for (int i = 0; i < numeroObjetos; i++) {
            // Genera una posición aleatoria en el terreno en X y Z
            float x = Random.Range(terrenoXMin, terrenoXMax);
            float z = Random.Range(terrenoZMin, terrenoZMax);

            // Obtiene la altura del terreno en la posición (x, z)
            float y = Terreno.SampleHeight(new Vector3(x, 0, z)) + Terreno.transform.position.y;

            // Crea el objeto en la posición obtenida
            Vector3 posicionAleatoria = new(x, y, z);
            GameObject objeto = Instantiate(objetoGenerar, posicionAleatoria, Quaternion.identity, Terreno.transform);
            objeto.transform.localScale = Vector3.one * escala;
            objeto.tag = "Recogible";
            
            // Opcional: Asegura que el objeto no se mueva
            objeto.isStatic = true;
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
