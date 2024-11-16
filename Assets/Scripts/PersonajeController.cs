using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour

{
    public float velocidad;
    public float fuerzaSalto;
    public int saltosMax;
    Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private bool mirandoDerecha = true;
    public LayerMask capaSuelo;
    private int saltosRestantes;
    private Animator animator;
    public AudioClip sonidoSalto;
    public float fuerzaGolpe;

    private bool puedeMoverse = true;

    private bool atacando;

    private bool canDoubleJump;

    public int maxJumps; // Número máximo de saltos permitido por nivel
    public int remainingJumps;
    public TextMeshProUGUI jumpCounterTMP; // Referencia al texto de TextMeshPro en la UI





    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        //saltosRestantes = saltosMax;
        remainingJumps = maxJumps; // Inicializa los saltos disponibles
        UpdateJumpCounterText(); // Actualiza el texto al iniciar

    }
    // Update is called once per frame
    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto2();
        ProcesarAtaque();
        animator.SetBool("Atacando", atacando);
    }
    bool EstaEnSuelo() /// Verifica si el personaje esta en el suelo
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;

    }


    /*void ProcesarSalto()
    {
        if(EstaEnSuelo()){
            saltosRestantes = saltosMax;
        }
        if(Input.GetKeyDown("space") && saltosRestantes > 0){
            saltosRestantes -= 1;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            rigidBody.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            AudioManager.Instance.ReproducirSonido(sonidoSalto);
        }

    }*/
    void ProcesarSalto2() // procesa el salto del personaje
    {
        if(Input.GetKeyDown("space") && remainingJumps > 0){
            if(EstaEnSuelo()){
                canDoubleJump = true;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, fuerzaSalto);
                remainingJumps--; // Reduce el contador de saltos 
                UpdateJumpCounterText(); // Actualiza el texto cada vez que el jugador salta             
            }else{
                if(Input.GetKeyDown("space")){
                    if(canDoubleJump){
                        rigidBody.velocity = new Vector2(rigidBody.velocity.x, fuerzaSalto);
                        remainingJumps--; // Reduce el contador de saltos
                        UpdateJumpCounterText(); // Actualiza el texto cada vez que el jugador salta 
                        canDoubleJump = false;
                    }
                }
            }
        }else if (remainingJumps == 0){
            RestartLevel();
        }
    }


    void ProcesarMovimiento() // Procesa el movimiento del personaje
    {
        if(puedeMoverse == false){
            return;
        }
        float inputMovimiento = Input.GetAxis("Horizontal");
        if(inputMovimiento != 0f){
            animator.SetBool("isRunning", true);
        }
        else{
            animator.SetBool("isRunning", false);        
        }
        rigidBody.velocity = new Vector2(inputMovimiento * velocidad, rigidBody.velocity.y);
        GestionarOrientacion(inputMovimiento);
    }

    void ProcesarAtaque() // Procesa el ataque al pulsar la tecla L
    {
        if(Input.GetKeyDown(KeyCode.L) && !atacando){
            Atacando();
        }
    }



    void GestionarOrientacion(float inputMovimiento) // Gestiona la orientacion del persoanje
    {
        if((mirandoDerecha==true && (inputMovimiento<0)) || (mirandoDerecha==false && (inputMovimiento>0))){
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

    }

    public void AplicarGolpe() // Aplica un golpe al personaje
    {
        puedeMoverse = false;

        Vector2 direccionGolpe;
        if(rigidBody.velocity.x > 0){
            direccionGolpe = new Vector2(-1, 1);
        }else{
            direccionGolpe = new Vector2(1, 1);
        }

        rigidBody.AddForce(direccionGolpe * fuerzaGolpe);


        StartCoroutine(EsperarYActivarMovimiento());


    }
    // Corrutina que espera un tiempo y luego permite el movimiento del personaje si está en el suelo
    IEnumerator EsperarYActivarMovimiento()
    {
        yield return new WaitForSeconds(0.1f);

        while(EstaEnSuelo()==false){
            yield return null;
        }

        puedeMoverse = true;
    }
   

    public void Atacando() // Activa el estado de ataque
    {
        atacando = true;
    }
    public void DesactivaAtaque() // Desactiva el estado de ataque
    {
        atacando = false;
    }
    void UpdateJumpCounterText()
    {
        // Actualiza el texto en pantalla para mostrar los saltos restantes
        jumpCounterTMP.text = "Saltos Restantes: " + remainingJumps;
    }
    void RestartLevel()
    {
        // Reinicia el nivel cargando de nuevo la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } 


}

