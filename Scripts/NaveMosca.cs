using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveMosca : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de descenso
    public float frecuenciaMovimientoX = 2f; // Frecuencia del movimiento ondulante en X
    public float amplitudMovimientoX = 3f; // Amplitud del movimiento ondulante en X
    public AudioClip sonidoExplosion;

    private AudioSource audioSource;
    private float posicionInicialX;

    void Start()
    {
        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("El componente AudioSource no se encontr� en NaveMosca.");
        }

        // Guardar la posici�n inicial en X para calcular el movimiento ondulante
        posicionInicialX = transform.position.x;
    }

    void Update()
    {
        // Movimiento senoidal en el eje X (patr�n de abeja)
        float desplazamientoX = Mathf.Sin(Time.time * frecuenciaMovimientoX) * amplitudMovimientoX;

        // Movimiento hacia abajo en el eje Y
        float nuevaPosicionY = transform.position.y - velocidad * Time.deltaTime;

        // Actualizar la posici�n de la nave
        transform.position = new Vector2(posicionInicialX + desplazamientoX, nuevaPosicionY);

        // Destruir la nave si sale de la pantalla
        if (transform.position.y < -6)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.IncrementarMissed(1);
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Proyectil"))
        {
            Debug.Log("Reproduciendo sonido de explosi�n");
            // Reproducir sonido de explosi�n
            if (audioSource != null && sonidoExplosion != null)
            {
                audioSource.PlayOneShot(sonidoExplosion);
            }
            else
            {
                Debug.LogError("AudioSource o sonidoExplosion no est� asignado.");
            }

            // Destruir la nave despu�s de reproducir el sonido
            Destroy(gameObject, sonidoExplosion.length);
        }
    }
}
