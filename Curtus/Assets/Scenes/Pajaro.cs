using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Se programa el movimiento del pajaro.
/// </summary>

public class Pajaro : MonoBehaviour {

public float inicioP, finalP;
private float cambio = 1;

	void Start () {

	}

	void Update () 
	{
		/// El objeto se gira atendiendo a si se pasa de ciertas coordenadas .
		if (transform.position.x < inicioP) {

			cambio = 1;
			Flip ();
		}
		else if (transform.position.x > finalP) {

			cambio = -1;
			Flip ();
		}

		/// El objeto se mueve atendiendo a donde mira .
		transform.position = new Vector2 (transform.position.x + cambio * 7f * Time.deltaTime , 0.0f);
	}

	private void Flip ()
	{
		/// Cómo gira el objeto .
		Vector2 theScale = transform.localScale;
		theScale.x *=-1;
		transform.localScale = theScale;
	}
}
