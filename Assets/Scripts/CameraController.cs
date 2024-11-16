using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform personaje;
    private float tamCamara;
    private float alturaPantalla;
    // Start is called before the first frame update
    void Start()
    {
        tamCamara = Camera.main.orthographicSize;
        alturaPantalla = tamCamara * 2;

    }

    // Update is called once per frame
    void Update()
    {
        CalcularPosicionCamara();
        
    }

    void CalcularPosicionCamara()
    {
        int pantallaPersonaje = (int)(personaje.position.y / alturaPantalla);
        float alturaCamara = (pantallaPersonaje * alturaPantalla) + tamCamara;
        transform.position = new Vector3(transform.position.x, alturaCamara, transform.position.z);


    }
}
