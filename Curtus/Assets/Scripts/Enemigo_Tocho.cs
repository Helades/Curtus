using UnityEngine;
using System.Collections;

/// <summary>
/// aqui se establece el enemigo pinchudo.
/// </summary>

public class Enemigo_Tocho : MonoBehaviour {
	
	private Transform target;
	public GameObject bullet;
	public Transform spawnPoint;
	public float speed, posx1, posx2, direction;
	private float c;
    private bool facingE;
	public bool disparar = false;
	public Rigidbody2D rb;
	public Animator animator;

    void Start () {

	}

	void Awake ()
    {
        /// Establecemos el objetivo, es decir, al jugador, para que los disparos del enemigo .

        target = GameObject.FindGameObjectWithTag ("Player").transform;
    }
	
	void Update () 
	
	{
        /// Movimiento de este enemigo.Siempre que mire al jugador.

        if (target.transform.position.x <= gameObject.transform.position.x && !facingE)
        {
            Flip();
        }

        if (target.transform.position.x >= gameObject.transform.position.x && facingE)
        {
            Flip();
        }
        
        /// <summary>
        /// Al llegar al punto X1 o X2 se mueve al otro punto.
        /// </summary>

        if (transform.position.x < posx1)
            direction = 1;
        if (transform.position.x > posx2)
            direction = -1;
        

        rb.velocity = new Vector2 (direction * speed * Time.deltaTime, rb.velocity.y);
        
        /// <summary>
        /// Dispara cada 6 segundos hacia adelante.
        /// </summary>

		if (disparar) {
		
		c += 1 * Time.deltaTime; 

			if (c > 6) {

				Instantiate (bullet, spawnPoint.position, spawnPoint.rotation);
				c = 0;
			}
		}
	}
	
	/// <summary>
	/// Como gira el enemigo.
	/// </summary>

    private void Flip()
    {
        facingE = !facingE;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}