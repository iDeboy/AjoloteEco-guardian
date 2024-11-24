using UnityEngine;

public class FlotacionConParticulas : MonoBehaviour
{
    private ParticleSystem burbujas;
    
    void Start()
    {
        // Encuentra el sistema de partículas
        burbujas = GetComponentInChildren<ParticleSystem>();

        // Si el objeto tiene un sistema de partículas
        if (burbujas != null)
        {
            var main = burbujas.main;
            main.startSpeed = 0.5f;
            main.startSize = 0.1f;
            main.startLifetime = 2f;
            burbujas.Play();
        }
    }

    void Update()
    {
        // Puedes hacer que el sistema de partículas cambie dependiendo del movimiento del objeto
        if (transform.position.y > 0) // Si está flotando sobre el agua
        {
            if (!burbujas.isPlaying)
            {
                burbujas.Play();
            }
        }
        else
        {
            if (burbujas.isPlaying)
            {
                burbujas.Stop();
            }
        }
    }
}
