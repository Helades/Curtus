using UnityEngine;
using System.Collections;

/// <summary>
/// Controla las mecanicas del EnemigoPuch.
/// </summary>

public class Enemigo_Punch : MonoBehaviour {
	
	private Transform target;
	public float speed;
	public Rigidbody2D rb;
	public bool muerto = false, punchetazo, activo = false;
	private float counter = 0;
	private bool facingE = true, normalWalk = true, isGrounded;
	public Animator animator;
	public StateIds estadoID = StateIds.Quieto;

    public enum StateIds {
    Quieto,
    Caminar,
    Muerte,
	Pegar
}

    void Start () {

	}

	void Awake ()
    {
        /// Establecemos el objetivo, es decir, al jugador, para que le persiga el enemigo .

        target = GameObject.FindGameObjectWithTag ("Player").transform;
    }
	
	void Update () 
	
	{	
		/// Establecemos un método para cambiar de estados .

		switch (estadoID) {

			case StateIds.Quieto:

			/// El enemigo simplemente se queda reproduciendo su animación "idle" .

			animator.SetBool ("Quieto", true);
			animator.SetBool ("Muerto", false);
			animator.SetBool ("Caminando", false);
			animator.SetBool ("Punch", false);

			break;

			case StateIds.Caminar:

			/// El enemigo procesa que el jugador le ha sobrepasado .

			if (target.transform.position.x < gameObject.transform.position.x && !facingE) {
		
				normalWalk = false;
				counter++;
				rb.velocity = new Vector2 (- speed * Time.deltaTime, rb.velocity.y);

			}

			if (target.transform.position.x > gameObject.transform.position.x && facingE) {

				normalWalk = false;
				counter++;
				rb.velocity = new Vector2 (speed * Time.deltaTime, rb.velocity.y);

			}

			/// Creamos la posibilidad de girar para el enemigo y un pequeño retraso para que el enemigo tarde en girarse .

			if (target.transform.position.x < gameObject.transform.position.x && !facingE && counter > 20)
			{
				Flip ();
				counter = 0;
				normalWalk = true;
			}
			if (target.transform.position.x > gameObject.transform.position.x && facingE && counter > 20)
			{
				Flip ();
				counter = 0;
				normalWalk = true;
			}
			
			/// El enemigo se mueve en dirección del jugador .

			if (normalWalk) {

				if (target.transform.position.x > gameObject.transform.position.x) {

					rb.velocity = new Vector2 (speed * Time.deltaTime, rb.velocity.y);

				} else if (target.transform.position.x < gameObject.transform.position.x) {

					rb.velocity = new Vector2 (- speed * Time.deltaTime, rb.velocity.y);

				}
			}

			/// El enemigo reproduce su animación de caminar mientras que se mueve de esta manera .

			animator.SetBool ("Quieto", false);
			animator.SetBool ("Muerto", false);
			animator.SetBool ("Caminando", true);
			animator.SetBool ("Punch", false);

			break;

			case StateIds.Muerte:

			/// El enemigo reproduce su animación de muerte .

			animator.SetBool ("Quieto", false);
			animator.SetBool ("Muerto", true);
			animator.SetBool ("Caminando", false);
			animator.SetBool ("Punch", false);

			break;

			case StateIds.Pegar:

			/// El enemigo reproduce su animación de ataque .

			animator.SetBool ("Quieto", false);
			animator.SetBool ("Muerto", false);
			animator.SetBool ("Caminando", false);
			animator.SetBool ("Punch", true);

			break;
		}

		/// Si el enemigo muere se entra al estado de Muerte .

		if (muerto) {

		estadoID = StateIds.Muerte;

		/// Si el enemigo está activo se entra al estado de Caminar .

        } else if (activo) {

		estadoID = StateIds.Caminar; 

		} else if (!activo) {

		// Si el enemigo está inactivo se entra al estado de Quieto .
			
		estadoID = StateIds.Quieto;
		}

		// Si el enemigo está en rango de ataque y sigue vivo entra al estado de Pegar .

		if (punchetazo && !muerto) {

			estadoID = StateIds.Pegar;
		}

	}

	/// Cómo gira el enemigo al ver al jugador .
	private void Flip ()
	{
		facingE = !facingE;
		Vector2 theScale = transform.localScale;
		theScale.x *=-1;
		transform.localScale = theScale;
	}

public void Muerte () {

	/// El enemigo se destruye .

	Destroy(this.gameObject);
}

public void Punch () {

	/// El enemigo se mira hacia donde esté el jugador .

	if (target.transform.position.x < gameObject.transform.position.x && !facingE) {
			
			Flip ();
			normalWalk = true;
	}
	
	if (target.transform.position.x > gameObject.transform.position.x && facingE) {
			
			Flip ();
			normalWalk = true;
	}

	/// Si el enemigo está tocando el suelo se abalanza hacia el jugador .
	
	if (isGrounded) 
	{
		if (target.transform.position.x < gameObject.transform.position.x) 
		{
			rb.AddForce(new Vector2(-5, 5), ForceMode2D.Impulse);
		}

		if (target.transform.position.x > gameObject.transform.position.x) 
		{
			rb.AddForce(new Vector2(5, 5), ForceMode2D.Impulse);
		}

		isGrounded = false;
	}
}

void OnCollisionEnter2D(Collision2D col)
    {
        /// Si está en contacto con un objeto "suelo" se considerará que está en el suelo el enemigo .

        if (col.gameObject.tag == ("Suelo"))
        {
            if (isGrounded == false) {

            isGrounded = true;
            }
        }

        if (col.gameObject.tag == ("SueloRompible"))
        {
            if (isGrounded == false) {

            isGrounded = true;
            }
        }
	}
}
