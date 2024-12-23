using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    public int valor = 1;
    public AudioClip sonidoMoneda;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.SumarPuntos(valor);

            FindObjectOfType<CoinManager>().AllCoinsCollected();

            Destroy(this.gameObject);
            AudioManager.Instance.ReproducirSonido(sonidoMoneda);
        }

    }
}
