using UnityEngine;
using System.Collections;

/// <summary>
/// Controla las mecanicas de los enemigos basicos.
/// </summary>

public class Enemigo : MonoBehaviour {

	private Transform target;
	public float speed;
	public bool activo = false, muerto = false;
	private float counter = 0;
	// Use this for initialization
	private bool facingE = true, normalWalk = true;
	public Animator animator;

    void Start () {

	}
	void Awake ()
    {
        /// Establecemos el objetivo, es decir, al jugador, para que le persiga el enemigo .

        target = GameObject.FindGameObjectWithTag ("Player").transform;
    }
	
	void Update () 
	
	{	
		if (muerto) {

		/// El enemigo reproduce su animación de muerte.

		animator.SetBool ("Quieto", false);
		animator.SetBool ("Andando", false);
		animator.SetBool ("Muerto", true);

        } else if (activo) {

		/// El enemigo reproduce su animación de caminar mientras que está activo .

		animator.SetBool ("Quieto", false);
		animator.SetBool ("Andando", true);	

		/// El enemigo tiene cierto desfase al seguir al jugador .

		if (target.transform.position.x <= gameObject.transform.position.x && !facingE) {
		
		normalWalk = false;
		counter++;
		transform.position = new Vector2 (transform.position.x + speed * Time.deltaTime, transform.position.y);

		}

		if (target.transform.position.x >= gameObject.transform.position.x && facingE) {

		normalWalk = false;
		counter++;
		transform.position = new Vector2 (transform.position.x - speed * Time.deltaTime, transform.position.y);

		}

		/// Creamos la posibilidad de giro  para el enemigo .

		if (target.transform.position.x <= gameObject.transform.position.x && !facingE && counter > 20)
		{
			Flip ();
			counter = 0;
			normalWalk = true;
		}
		if (target.transform.position.x >= gameObject.transform.position.x && facingE && counter > 20)
		{
			Flip ();
			counter = 0;
			normalWalk = true;
		}
		
		/// Cuando se de cuenta de donde está el jugador se dirige hacia él .

		if (normalWalk) {

		transform.position = Vector2.MoveTowards (transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);

		}
		/// El enemigo simplemente se queda reproduciendo su animación "idle" .
		} 
		else if (!activo) 
		{
		animator.SetBool ("Quieto", true);
		animator.SetBool ("Andando", false);
		}

	}

	// Cómo gira el enemigo al ver al jugador .
	private void Flip ()
	{
		facingE = !facingE;
		Vector2 theScale = transform.localScale;
		theScale.x *=-1;
		transform.localScale = theScale;
	}

public void Muerte () {

	/// Se elimina el enemigo .

	Destroy(this.gameObject);
}
}
