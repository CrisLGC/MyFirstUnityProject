using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI puntos;
    public GameObject[] vidas;

    public GameObject panelPausa;       // Referencia al panel de pausa
    private bool estaPausado = false;   // Estado del juego




    void Start()
    {
        Time.timeScale = 1f;
        // Asegurarse de que el panel de pausa esté oculto al inicio
        panelPausa.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        puntos.text = GameManager.Instance.PuntosTotales.ToString();

        // Verificar si se presiona la tecla Esc para activar/desactivar pausa
        if (Input.GetKeyDown(KeyCode.Escape)) // Puedes usar otra tecla como KeyCode.P
        {
            if (estaPausado)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }

    public void ActualizarPuntos(int puntosTotales)
    {
        puntos.text = puntosTotales.ToString();
    }

    public void DesactivarVida(int indice)
    {
        vidas[indice].SetActive(false);
    }
    public void ActivarVida(int indice)
    {
        vidas[indice].SetActive(true);
    }

    public void ActivarTCandado()
    {
    }

    // Pausar el juego
    private void PausarJuego()
    {
        estaPausado = true;
        panelPausa.SetActive(true);
        Time.timeScale = 0f;  // Pausar el tiempo del juego
    }

    // Reanudar el juego
    private void ReanudarJuego()
    {
        estaPausado = false;
        panelPausa.SetActive(false);
        Time.timeScale = 1f;  // Reanudar el tiempo del juego
    }

    public void IrMenuInicio()
    {
        Debug.Log("Funciona");
        SceneManager.LoadScene("MainMenu");
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }







}
