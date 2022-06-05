using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PlayerControler : MonoBehaviour //algunos de los imputs los detecta solo unity aciendo que algunos controles ya esten estipulados 
{
    
    public float speed, jumpHeight; // velocidad y salto  fuerzxaas
    public float doubleJumpSpeed = 2.5f;
    float velX, velY; // velocidades de x e y 
    Rigidbody2D rb; // colision
    public Transform groundcheck;
    public bool isGrounded;
   
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public float size;  
    Animator anim;
    public GameObject bullet;
    public float fireRate = 0.5f;  //salida de la bala y velocidad
    //float nextFire = 0.0f;
    public bool duck = false;
    int duckID;
    bool gamestarted = false;
   

    public int numJump = 1;
    public int jumpUse = 0;
    public bool canJump = false;

    public bool pausedgame= false;




    public static PlayerControler instance = null; // se crea una variable estatica comun entre los gamecontrller a null

    void Awake()
    {
        if (instance == null)  //comprovamos si se ha creado un gamecontroler
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return; // cortamos la ejecucion 
        }

        Destroy(this.gameObject);
    }

    public void SetPlayerStart(bool start) 
    {
        gamestarted = start;
    }

     public bool GetPlayerStart() 
    { 
        return gamestarted;
    }

    public void StartLevel() 
    {
       WeaponController weaponController = GetComponent<WeaponController>();
       weaponController.StartLevel();

       PlayerHealth playerHealth = GetComponent<PlayerHealth>();
       playerHealth.StartLevel();
    }

    void Start() //se llama al inicio
    {
        rb = GetComponent<Rigidbody2D>(); //coge el dato del rigid bodi
        anim = GetComponent<Animator>(); //coge el dato del apartado de las animaciones 
       
        //DontDestroyOnLoad(canvas);
    }

    void FixedUpdate() 
    {
        if (gamestarted)
        {
            isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, whatIsGround); //comprovamos que esta tocando , si se esta tocando y cuanto se eesta tocando 
            if (isGrounded)
            {
                anim.SetBool("Jump", false);
            }else
            {
                anim.SetBool("Jump", true);
            }

            FlipPlayer();
            Movement();
            Jump();
            Duck();
            PauseGame();
            ResumeGame();
          


        }
    }

    public void Movement()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb.velocity.y;
        rb.velocity = new Vector2(velX * speed, velY); // se efectual el moviminto 

        if (rb.velocity.x != 0)
        {
            anim.SetBool("Walk", true); //comprobamos si anda para pasar la animacion correspondiente 
        }
        else {
            anim.SetBool("Walk", false); //volvemos al estado idle
        }
    }

    public void Jump()

    {

        if (isGrounded)
        {
            jumpUse = 0;
            canJump = true;

        }
        else
        {
            if (jumpUse == 0) 
            {
                canJump = false;           
            }
        }

        if (Input.GetButtonUp("Jump") || !Input.GetButton("Jump")) 
        {
            if (jumpUse != 0) 
            {
                canJump = true;
            
            }
        
        }

        if (Input.GetButton("Jump") && jumpUse < numJump && canJump)       //preionar una tecla en este caso salto
        {
            jumpUse++;
            canJump = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);

        }

    }




    public void Duck() 
    {       
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("Duck", true);      
        } else

        {
            anim.SetBool("Duck", false);
        }  
    }



    public void FlipPlayer()
    {                                                   //cambiar la posicion del personaje
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);   
        }                                                                
        else if(rb.velocity.x < 0)          //si solo se pone el else el personaje mantiene la animacion hacia la izquierda
        {
            transform.localScale = new Vector3(-1, 1, 1);
           
        }
    }

    public void Reset() 
    {
        GameObject textGameObject;
        textGameObject = GameObject.Find("Canvas");

        Destroy(textGameObject);
        Destroy(gameObject);

    }

    public void DoDamage(float damage) 
    {
        GetComponent<PlayerHealth>().DoDamage(damage);
        

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.CompareTag("Botas"))
        {
            if (collision.gameObject.activeSelf)
            {
                collision.gameObject.SetActive(false);
                numJump += 1;
                Destroy(collision.gameObject);
                HintController.instance.MostrarMensajePorArma(0);
            }
        }
        
    }


    private void PauseGame() 
    {
        
        if (Input.GetButton("Cancel"))
        {
            CanvasController.instance.MenuPausa();
            pausedgame = true;

                  

        } 

       

    }

    private void ResumeGame()

    {
        if (pausedgame)
        {
            if (Input.GetButton("Jump")) 
            {
                CanvasController.instance.MenuPrincipal();
                pausedgame = false;

            }



        }

    }

 



   
}
//transform.localScale = new Vector3(-1, 1, 1);