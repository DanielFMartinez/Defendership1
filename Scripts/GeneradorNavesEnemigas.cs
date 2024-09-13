using System.Collections;
using UnityEngine;

public class GeneradorNavesEnemigas : MonoBehaviour
{
    [SerializeField] private GameObject naveEnemigaPrefab;
    [SerializeField] private float tiempoEntreGeneraciones = 2f;

    // Rango en X (-9 a 9) y rango en Y (5 a 7)
    private float rangoXMin = -9f;
    private float rangoXMax = 9f;
    private float rangoYMin = 5f;
    private float rangoYMax = 7f;

    private bool generandoNaves = true; // Para controlar el ciclo de generación

    private void Start()
    {
        StartCoroutine(GenerarNaves());
    }

    private IEnumerator GenerarNaves()
    {
        while (generandoNaves)
        {
            GenerarNave();  // Generar nave enemiga
            yield return new WaitForSeconds(tiempoEntreGeneraciones);  // Esperar el tiempo establecido
        }
    }

    private void GenerarNave()
    {
        // Generar coordenadas aleatorias dentro de los rangos especificados
        float posX = Random.Range(rangoXMin, rangoXMax);
        float posY = Random.Range(rangoYMin, rangoYMax);

        Vector2 posicionGeneracion = new Vector2(posX, posY);
        Instantiate(naveEnemigaPrefab, posicionGeneracion, Quaternion.identity);
    }

    // Método para detener la generación de naves si es necesario
    public void DetenerGeneracion()
    {
        generandoNaves = false;
    }

    // Método para reanudar la generación
    public void ReanudarGeneracion()
    {
        if (!generandoNaves)
        {
            generandoNaves = true;
            StartCoroutine(GenerarNaves());
        }
    }
}
