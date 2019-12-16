using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
	public GameManager gameManager;
	public Rigidbody2D rb;
	public float speed;
    private bool isGrounded, dobleSalto, bucle, play, deathplay;
    public bool muerte, cuadradofall;
    public Transform ca;
    private float dobleSaltoListo, movimientoX, movimientoY;
    public Animator animator;
    public AudioClip deathcurtus, jump, walk, win, trans, impact;
    public AudioSource audioSrc;
	public PlayerData data;
    public StateIds estadoID = StateIds.Quieto;

    public enum StateIds {
    Quieto,
    Caminar,
    Caer,
    TransCuadrado,
    FallCuadrado,
    Ascendente ,
    Muerte
}

    void Start () {

        isGrounded = true;
        dobleSalto = true;
        deathplay = false;

        if(SaveLoad.GetCheckPoint())
       		gameManager.Load();
    }
	
	// Update is called once per frame
	void Update () {

        if (this.transform.position.y < -50) {

        /// Si el personaje se cae, a cierta profundidad entra en el estado Muerte .
                
        muerte = true;

        } else {

        /// El personaje se mueve con A y D .
     
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, rb.velocity.y);
       
        }

		movimientoX = rb.velocity.x;
        movimientoY = rb.velocity.y;

        switch (estadoID) {

            case StateIds.Quieto:

            audioSrc.loop = false;
            audioSrc.Stop();
            play = false;
            bucle = false;

            animator.SetBool ("Andando", false);
            animator.SetBool ("Quieto", true);
            animator.SetBool ("Saltando", false);
            animator.SetBool ("Cayendo", false);
            animator.SetBool ("FallCube", false);
            animator.SetBool ("TransCube", false);

            break;

            case StateIds.Caminar:

            if (movimientoX > 0)
                transform.localScale = new Vector2 (1, 1);
            if (movimientoX < 0)
                transform.localScale = new Vector2 (-1, 1);

            if (audioSrc.clip != walk) { 
                audioSrc.clip = walk;
                audioSrc.Play();
            }

            if (!bucle && (audioSrc.clip = walk)) {
                
                audioSrc.loop = true;
                bucle = true;

                if (!play)
                {
                    audioSrc.Play();
                    play = true;
                }
            }
            
            animator.SetBool ("Andando", true);
            animator.SetBool ("Quieto", false);
            animator.SetBool ("Saltando", false);
            animator.SetBool ("Cayendo", false);
            animator.SetBool ("FallCube", false);
            animator.SetBool ("TransCube", false);

            break;

            case StateIds.Caer:

            if (movimientoX > 0)
                transform.localScale = new Vector2 (1, 1);
            if (movimientoX < 0)
                transform.localScale = new Vector2 (-1, 1);

            animator.SetBool ("Andando", false);
            animator.SetBool ("Quieto", false);
            animator.SetBool ("Saltando", false);
            animator.SetBool ("Cayendo", true);
            animator.SetBool ("FallCube", false);
            animator.SetBool ("TransCube", false);

            break;

            case StateIds.Ascendente:

            if (movimientoX > 0)
                transform.localScale = new Vector2 (1, 1);
            if (movimientoX < 0)
                transform.localScale = new Vector2 (-1, 1);

            animator.SetBool ("Andando", false);
            animator.SetBool ("Quieto", false);
            animator.SetBool ("Saltando", true);
            animator.SetBool ("Cayendo", false);
            animator.SetBool ("FallCube", false);
            animator.SetBool ("TransCube", false);

            break;

            case StateIds.TransCuadrado:

            rb.velocity = new Vector2 (0.0f,  0.0f);

            animator.SetBool ("Andando", false);
            animator.SetBool ("Quieto", false);
            animator.SetBool ("Saltando", false);
            animator.SetBool ("Cayendo", false);
            animator.SetBool ("FallCube", false);
            animator.SetBool ("TransCube", true);

            if (!deathplay) {
            audioSrc.clip = trans;
            audioSrc.Play();
            audioSrc.loop = false;
            bucle = false;
            play = false;
            deathplay = true;
            }
        
            break;

            case StateIds.FallCuadrado:

            deathplay = false;

            if (movimientoX > 0)
                transform.localScale = new Vector2 (1, 1);
            if (movimientoX < 0)
                transform.localScale = new Vector2 (-1, 1);

            rb.gravityScale = 2;    
            animator.SetBool ("FallCube", true);
            speed = 200;

            animator.SetBool ("Andando", false);
            animator.SetBool ("Quieto", false);
            animator.SetBool ("Saltando", false);
            animator.SetBool ("Cayendo", false);
            animator.SetBool ("FallCube", true);
            animator.SetBool ("TransCube", false);

            break;

            case StateIds.Muerte:

            animator.SetBool ("Muerto", true);
            animator.SetBool ("Andando", false);
            animator.SetBool ("Quieto", false);
            animator.SetBool ("Saltando", false);
            animator.SetBool ("Cayendo", false);
            animator.SetBool ("FallCube", false);
            animator.SetBool ("TransCube", false);
            muerte = true;
            rb.gravityScale = 0;
            rb.velocity = new Vector2 (0.0f,  0.0f);
            GetComponent<PolygonCollider2D>().enabled = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
            

            if (!deathplay) {
            audioSrc.clip = deathcurtus;
            audioSrc.Play();
            audioSrc.loop = false;
            bucle = false;
            play = false;
            deathplay = true;
            }

            break;
        }

        if (muerte) {

            estadoID = StateIds.Muerte;
        }

        if (estadoID != StateIds.FallCuadrado && estadoID != StateIds.TransCuadrado && !muerte) {
            
            rb.gravityScale = 1;
            speed = 400;
        }

        if ((movimientoX > 0 || movimientoX < 0) && isGrounded && !muerte) {
            
            estadoID = StateIds.Caminar; 
        }
        
        if (movimientoX == 0 && movimientoY == 0 && estadoID != StateIds.FallCuadrado && estadoID != StateIds.TransCuadrado && !muerte) {
        	
            estadoID = StateIds.Quieto;
        }

        if (Input.GetButtonDown ("Down") && !isGrounded && estadoID != StateIds.FallCuadrado && estadoID != StateIds.TransCuadrado && !muerte) 
        {
            estadoID = StateIds.TransCuadrado;
        }

        if (Input.GetButtonDown ("Jump") && isGrounded && estadoID != StateIds.FallCuadrado && estadoID != StateIds.TransCuadrado && !muerte) {

                rb.velocity = new Vector2 (0.0f,  0.0f);
                rb.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
                isGrounded = false;
                audioSrc.clip = jump;
                audioSrc.Play();
                audioSrc.loop = false;
                bucle = false;
                play = false;

                Debug.Log("Salto");

                animator.SetBool ("Andando", false);
                animator.SetBool ("Quieto", false);
                animator.SetBool ("Saltando", true);
                animator.SetBool ("Cayendo", false);
                animator.SetBool ("FallCube", false);
                animator.SetBool ("TransCube", false);
            
        } else if (Input.GetButtonDown ("Jump") && dobleSalto && dobleSaltoListo > 10 && estadoID != StateIds.FallCuadrado && estadoID != StateIds.TransCuadrado && !muerte) {
		
            rb.velocity = new Vector2 (0.0f,  0.0f);
			    rb.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
                dobleSalto = false;
                dobleSaltoListo = 0;
                audioSrc.clip = jump;
                audioSrc.Play();
                audioSrc.loop = false;
                bucle = false;
                play = false;

                Debug.Log("Doble_Salto");

                animator.SetBool ("Andando", false);
                animator.SetBool ("Quieto", false);
                animator.SetBool ("Saltando", true);
                animator.SetBool ("Cayendo", false);
                animator.SetBool ("FallCube", false);
                animator.SetBool ("TransCube", false);
        }     

        if (rb.velocity.y > 0 && isGrounded == false && estadoID != StateIds.FallCuadrado && estadoID != StateIds.TransCuadrado && !muerte) {
            
            estadoID = StateIds.Ascendente;
        } 

        if (rb.velocity.y < 0 && isGrounded == false && estadoID != StateIds.FallCuadrado && estadoID != StateIds.TransCuadrado && !muerte) {
            
            estadoID = StateIds.Caer;
        }

        if (isGrounded == false && dobleSalto && !muerte) {
            
            dobleSaltoListo++;
        
        }   
        ca.position = new Vector2 (rb.position.x, rb.position.y);
    }
    

    
    void OnCollisionEnter2D(Collision2D col)
    {
        // Si estás en contacto con un cuerpo "Suelo" se considerará que estás en el suelo
        if (col.gameObject.tag == ("Suelo"))
        {
            rb.velocity = new Vector2 (0.0f,  0.0f);
            estadoID = StateIds.Quieto;

            if (isGrounded == false) {

                isGrounded = true;
                dobleSalto = true;
            }
        }

        if (col.gameObject.tag == ("SueloRompible"))
        {
            rb.velocity = new Vector2 (0.0f,  0.0f);

            if (isGrounded == false) {

            isGrounded = true;
            dobleSalto = true;
            }

            if (estadoID == StateIds.FallCuadrado) {
            Destroy(col.gameObject);
            }
        }

        if (col.gameObject.tag == ("Cabeza") && estadoID != StateIds.FallCuadrado) {
  
                estadoID = StateIds.Muerte;     
        }

        if (col.gameObject.tag == "Bala")
        {
            muerte = true;
            Destroy(col.gameObject);
        }
    }
    

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter2D (Collider2D col) {

        if (col.gameObject.tag == "Enemigo") {
        
            if (!muerte) {
    			estadoID = StateIds.Muerte;
            }         
        }

        if (col.gameObject.tag == "manzana")
        {
            audioSrc.clip = win;
            audioSrc.Play();
            Destroy(col.gameObject);
        }
    }

    public void Muerte () {

        gameManager.Lose();
	}

    public void TransCube () {

        estadoID = StateIds.FallCuadrado;
	}
    
    public void Save()
    {
        data.saved_position = new PlayerData.MyVector3(transform.position);
    }

    public void Load()
    {
        transform.position = data.saved_position.getVector();
    }
    
}