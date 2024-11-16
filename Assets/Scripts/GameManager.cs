using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance { get; private set;}

    public HUD hud;

    public int PuntosTotales { get { return puntosTotales; }}
    private int puntosTotales;

    private int vidas = 3;

    void Awake()
    {
        if(Instance == null){
            Instance = this;
        }else{
            Debug.Log("Mas de un Game Manager en escena");
        }
    }

    public void SumarPuntos(int puntosASumar)
    {
        puntosTotales += puntosASumar;
        Debug.Log(puntosTotales);
        hud.ActualizarPuntos(PuntosTotales);

    }

    public void PerderVida()
    {
        vidas -= 1;
        if(vidas == 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }



        hud.DesactivarVida(vidas);
    }

    public bool RecuperarVida()
    {
        if(vidas == 3){
            return false;
        }
        hud.ActivarVida(vidas);
        vidas += 1;
        return true;
    }



}
