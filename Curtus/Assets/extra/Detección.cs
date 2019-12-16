
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detección : MonoBehaviour {

	public Transform player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		/// Hacemos que el centro del círculo de detección esté donde se encuentre el personaje del jugador .
		
		this.transform.position = new Vector2 (player.position.x, player.position.y);
	}

	void OnTriggerEnter2D (Collider2D col) {

		/// Si se detecta a un enemigo dentro del área, dicho enemigo se activa .

		if(col.gameObject.tag == "Danger" && col.GetComponent<Enemigo>())
        {
			Debug.Log("Detectado");
            col.GetComponent<Enemigo>().activo = true;
		}
		
		if (col.gameObject.tag == "Punch")
        {
			Debug.Log("Detectado");
            col.GetComponent<Enemigo_Punch>().activo = true;
		}

		if (col.gameObject.tag == "Danger" && col.GetComponent<Enemigo_Tocho>())
        {
			Debug.Log("Detectado");
            col.GetComponent<Enemigo_Tocho>().disparar = true;
		}
	}

	void OnTriggerExit2D (Collider2D col) {

		/// Si se detecta a un enemigo saliendo del área, dicho enemigo se desactiva .

		if(col.gameObject.tag == "Danger" && col.GetComponent<Enemigo>())
        {
			Debug.Log("Perdido");
            col.GetComponent<Enemigo>().activo = false;
		} 
		
		if(col.gameObject.tag == "Punch")
        {
			Debug.Log("Perdido");
            col.GetComponent<Enemigo_Punch>().activo = false;
		}

		if (col.gameObject.tag == "Danger" && col.GetComponent<Enemigo_Tocho>())
        {
			Debug.Log("Detener fuego");
            col.GetComponent<Enemigo_Tocho>().disparar = false;
		}
	}
}