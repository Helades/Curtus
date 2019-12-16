using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Se programa el seguimieento de los enemigos hacia el protagonista.
/// </summary>

public class SeguirEnemigo : MonoBehaviour {

	// Use this for initialization
	public Transform enemy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		this.transform.position = new Vector2 (enemy.position.x, enemy.position.y);
	}
}
