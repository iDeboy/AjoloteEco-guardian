using UnityEngine;

public class GeneradorBasura : MonoBehaviour
{
    public int NumeroObjetosGenerados { get; private set; }

    [SerializeField]
    private Terrain Terreno;

    [SerializeField]
    private int numeroObjetos = 10;

    [SerializeField]
    private float escala = 0.75f;

    [SerializeField]
    private GameObject[] objetosGenerar; // Array de objetos que pueden ser generados

    private float terrenoXMin;
    private float terrenoXMax;
    private float terrenoZMin;
    private float terrenoZMax;

    private const float alturaMaxima = 3f; // Altura máxima permitida para validar
    private const float alturaFija = 3f;  // Altura fija para posicionar los objetos

    void Start()
    {
        terrenoXMin = Terreno.transform.position.x;
        terrenoXMax = terrenoXMin + Terreno.terrainData.size.x;
        terrenoZMin = Terreno.transform.position.z;
        terrenoZMax = terrenoZMin + Terreno.terrainData.size.z;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = -1;
        GenerarObjetos();
    }

    private void GenerarObjetos()
    {
        int objetosGenerados = 0;

        while (objetosGenerados < numeroObjetos)
        {
            // Genera una posición aleatoria en el terreno en X y Z
            float x = Random.Range(terrenoXMin, terrenoXMax);
            float z = Random.Range(terrenoZMin, terrenoZMax);

            // Obtiene la altura del terreno en la posición (x, z)
            float yTerreno = Terreno.SampleHeight(new Vector3(x, 0, z)) + Terreno.transform.position.y;

            // Verifica si la altura del terreno está dentro del rango permitido
            if (yTerreno <= alturaMaxima)
            {
                // Fija la altura del objeto en la altura deseada
                float y = alturaFija;

                // Crea la posición final del objeto
                Vector3 posicionFija = new Vector3(x, y, z);

                // Selecciona un objeto aleatorio del array
                GameObject objetoSeleccionado = objetosGenerar[Random.Range(0, objetosGenerar.Length)];

                // Instancia el objeto seleccionado
                GameObject objeto = Instantiate(objetoSeleccionado, posicionFija, Quaternion.identity, Terreno.transform);

                // Guarda las escalas originales de todos los hijos
                Transform[] hijos = objeto.GetComponentsInChildren<Transform>();
                Vector3[] escalasOriginales = new Vector3[hijos.Length];
                for (int i = 0; i < hijos.Length; i++)
                {
                    escalasOriginales[i] = hijos[i].localScale;
                }

                // Aplica la escala al objeto principal
                objeto.transform.localScale = objetoSeleccionado.transform.localScale * escala;

                // Restaura las escalas originales de los hijos
                for (int i = 0; i < hijos.Length; i++)
                {
                    hijos[i].localScale = escalasOriginales[i];
                }

                objeto.tag = "Recogible";

                // Opcional: Asegura que el objeto no se mueva
                objeto.isStatic = true;

                // Incrementa el contador de objetos generados
                objetosGenerados++;
            }
        }
        NumeroObjetosGenerados = objetosGenerados; 
    }
}
