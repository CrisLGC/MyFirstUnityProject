using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CoinManager : MonoBehaviour
{
    public TextMeshProUGUI textoTMP; // Referencia al objeto TextMeshProUGUI
    public float duracion = 3f;      // Duración en segundos

    void Start()
    {
        textoTMP.gameObject.SetActive(false); // Ocultar el texto al inicio
    }

    // Método para mostrar el texto temporalmente
    public void MostrarTextoTemporal()
    {             // Cambia el texto
        textoTMP.gameObject.SetActive(true);  // Muestra el texto
        StartCoroutine(OcultarTexto());       // Inicia la corrutina para ocultarlo
    }

    private IEnumerator OcultarTexto()
    {
        yield return new WaitForSeconds(duracion); // Espera la duración especificada
        textoTMP.gameObject.SetActive(false);      // Oculta el texto
    }

    public void AllCoinsCollected()
    {
        if(transform.childCount==1)
        {
            Debug.Log("Has conseguido todas las monedas del nivel");
            MostrarTextoTemporal();
            Destroy(GameObject.Find("Padlock").gameObject);
        }
    }



}
