using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemig0 : MonoBehaviour

{
    private bool puedeAtacar = true;
    public float cooldownAtaque;

    private SpriteRenderer spriteRenderer;

    public Transform player;
    public float detectionRadius = 5.0f;
    public float speed = 2.0f;

    private Rigidbody2D rb;
    private Vector2 movement;

    private bool recibiendoDamage;
    public float fuerzaGolpe;
    public int vidaEnemy;
    private bool estaMuerto;

    public float rangoMovimiento = 5f;   // Rango ajustable de movimiento
    private float posicionInicial;       // Posición inicial del enemigo
    private bool moviendoDerecha = true; // Dirección del movimiento
    private Animator animator;

    private bool atacando;



    //private bool mirandoDerechaE = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Guarda la posición inicial en el eje X
        posicionInicial = transform.position.x;
    }

    void Update()
    {
        MoverEnemigo();
        

        /*float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if(distanceToPlayer < detectionRadius){
            Vector2 direction = (player.position - transform.position).normalized;

            movement = new Vector2(direction.x, 0);

            
        }else{
            movement = Vector2.zero;
        }
        if(!recibiendoDamage)
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);*/


    }

    // Manejar colisión con el jugador
     private void OnCollisionEnter2D(Collision2D other){
    
        if(other.gameObject.CompareTag("Player")){
            atacando = true;
            animator.SetBool("Atacando", atacando);

            if(puedeAtacar == false){
                return;
            }

            puedeAtacar = false;

            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;


            GameManager.Instance.PerderVida();

            other.gameObject.GetComponent<CharacterController>().AplicarGolpe();
            

            Invoke("ReactivarAtaque", cooldownAtaque);
        }
    }


    /// Este método se llama cuando la espada del personaje entra en el colisionador de disparo adjunto a este objeto.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Espada")){
            Vector2 direccionDamage = new Vector2(collision.gameObject.transform.position.x, 0);

            RecibeDamage(direccionDamage, 1);

        }
    }


    /// Método que aplica daño al enemigo.

    /// <param name="direccion">Dirección desde la cual proviene el daño.</param>
    /// <param name="cantDamage">Cantidad de daño a aplicar.</param>
    public void RecibeDamage(Vector2 direccion, int cantDamage)
    {
        if(recibiendoDamage == false){
            vidaEnemy = vidaEnemy - cantDamage;
            recibiendoDamage = true;
            if(vidaEnemy <= 0){
                estaMuerto = true;
                DeleteBody();

            }else{
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.2f).normalized;
                rb.AddForce(rebote * fuerzaGolpe, ForceMode2D.Impulse);
                StartCoroutine(DesactivaDamage());

            }

        }
    }


    /// Corrutina que desactiva el estado de recibir daño después de un breve retraso.

    IEnumerator DesactivaDamage()
    {
        yield return new WaitForSeconds(0.4f);
        recibiendoDamage = false;
        rb.velocity = Vector2.zero;
    }

    /// Elimina el objeto de juego asociado con este script.

    public void DeleteBody()
    {
        Destroy(this.gameObject);
    }
    


    /// Reactiva la capacidad de ataque del enemigo.

    void ReactivarAtaque()
    {
        puedeAtacar = true;

        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;
    }


    /// Dibuja una esfera de alambre roja en la vista de la escena para representar el radio de detección del enemigo.
    /// 
    /// Este método es llamado por Unity cuando el objeto está seleccionado en el editor.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    /*void GestionarOrientacionEnemy()
    {
        if((rb.velocity.x<0) || (rb.velocity.x>0)){
            mirandoDerechaE = !mirandoDerechaE;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }*/

    private void MoverEnemigo()
    {
        // Calcula los límites izquierdo y derecho basados en el rango de movimiento
        float limiteDerecho = posicionInicial + rangoMovimiento;
        float limiteIzquierdo = posicionInicial - rangoMovimiento;

        // Mueve el enemigo hacia la derecha o izquierda
        if (moviendoDerecha)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            // Cambia de dirección si llega al límite derecho
            if (transform.position.x >= limiteDerecho)
            {
                moviendoDerecha = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            // Cambia de dirección si llega al límite izquierdo
            if (transform.position.x <= limiteIzquierdo)
            {
                moviendoDerecha = true;
            }
        }
    }


    /// Desactiva el ataque del enemigo estableciendo la bandera 'atacando' a false
    /// y actualizando el parámetro "Atacando" del animador en consecuencia.

    public void DesactivaAtaqueEnemy()
    {
        atacando = false;
        animator.SetBool("Atacando", atacando);
    }



}
