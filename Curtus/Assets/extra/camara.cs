using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Establecemos el seguimiento de la camara respecto al protagonista.
/// </summary>

public class camara : MonoBehaviour {

	// Use this for initialization
	
	public Transform target;
	
	void Start () 
	{
		 
	}
	
	// Update is called once per frame
	void Update ()
	{

		/// Hacemos que la cámara esté siguiendo al personaje del jugador .	
		
		transform.position = new Vector2 (target.position.x, target.position.y);
		
	}
}
