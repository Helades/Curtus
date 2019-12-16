using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla la eliminacion de los enemigos cuando se colisiona con su cabeza.
/// </summary>

public class EliminarEnemigo : MonoBehaviour {

	// Use this for initialization

	public GameObject enemy;

    public AudioClip enemydeath;

    public AudioSource audioSrc;
    

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

	void OnCollisionEnter2D (Collision2D col)
    {
		/// Si el jugador colisiona con la cabeza del enemigo y está cayendo en forma de Cubo el enemigo es eliminado .

		if(col.gameObject.tag == "Player")
        {
			if (col.gameObject.GetComponent<PlayerController>().estadoID == PlayerController.StateIds.FallCuadrado) 
			{	
            audioSrc.clip = enemydeath;
            audioSrc.Play();
            Debug.Log("Muerte");
			
			if (enemy.GetComponent<Enemigo>())
            enemy.GetComponent<Enemigo>().muerto = true;

			if (enemy.GetComponent<Enemigo_Punch>())
			enemy.GetComponent<Enemigo_Punch>().muerto = true;
			
			Debug.Log("Illo cabesa");
            col.gameObject.GetComponent<PlayerController>().rb.velocity = new Vector2 (0.0f,  0.0f);
			col.gameObject.GetComponent<PlayerController>().rb.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
			}
		}
	}
}
