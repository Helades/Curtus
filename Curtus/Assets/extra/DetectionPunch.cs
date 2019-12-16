using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este script controla la deteccion del personaje EnemigoPuch.
/// </summary>

public class DetectionPunch : MonoBehaviour {

	public Transform player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		this.transform.position = new Vector2 (player.position.x, player.position.y);
	}

	void OnTriggerEnter2D (Collider2D col) {

		/// Si se detecta a un enemigo de tipo Punch dentro del radio de detección de ataque se activa el ataque de "puñetazo" .

		if(col.gameObject.tag == "Punch")
        {
			Debug.Log("Preparando_Punch");
            col.GetComponent<Enemigo_Punch>().punchetazo = true;
		}
	}

	void OnTriggerExit2D (Collider2D col) {

		/// Si se detecta a un enemigo de tipo Punch saliendo del radio de detección de ataque se desactiva el ataque de "puñetazo" .

		if(col.gameObject.tag == "Punch")
        {
			Debug.Log("Adiosito");
            col.GetComponent<Enemigo_Punch>().punchetazo = false;
		}
	}
}
