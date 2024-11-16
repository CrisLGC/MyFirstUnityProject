using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoCandado : MonoBehaviour
{
    public float duracion = 3f; // Duración en segundos

    void Start()
    {
        this.gameObject.SetActive(false); // Asegúrate de que el texto esté oculto al iniciar
    }

    // Método para mostrar el texto
    public void MostrarTextoTemporal()
    {
             // Establece el mensaje del texto
        this.gameObject.SetActive(true);  // Muestra el texto
        StartCoroutine(OcultarTexto());    // Inicia la corrutina para ocultarlo
    }

    private IEnumerator OcultarTexto()
    {
        yield return new WaitForSeconds(duracion); // Espera la duración especificada
        this.gameObject.SetActive(false);         // Oculta el texto
    }

}
