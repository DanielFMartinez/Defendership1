using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float velocidad = 10f;
    public float tiempoDeVida = 2f;

    void Start()
    {
        Destroy(gameObject, tiempoDeVida); // Destruye el proyectil después de un tiempo
    }

    void Update()
    {
        transform.Translate(Vector2.up * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NaveEnemiga"))
        {
            Destroy(other.gameObject); // Destruye la nave enemiga
            Destroy(gameObject); // Destruye el proyectil

            // Incrementa el puntaje
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.IncrementarScore(10); // Incrementa el puntaje en 10 (ajusta según sea necesario)
            }
            else
            {
                Debug.LogError("No se encontró el GameManager en la escena.");
            }
        }
    }
}