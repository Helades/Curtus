using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Este script controla el movimiento de las plataformas móviles del juego.

public class PlataformaAuto : MonoBehaviour {

    public Transform plataforma;
    private bool moving = false;
    public float ex1, ex2;
    private int mov = 0;

	void Start () {

	}

	void Update () {

        /// Movemos la plataforma en el eje de las X .
        plataforma.Translate(new Vector2(mov * 2.0f, 0.0f) * Time.deltaTime);

        /// Si se mueve más allá del punto ex1 hacemos que se mueva en el eje positivo de las X's .
        if (plataforma.position.x < ex1)
        {
            mov = 1;
           
        }
        /// Si se mueve más allá del punto ex2 hacemos que se mueva en el eje negativo de las X's .
        else if (plataforma.position.x > ex2)
        {
            mov = -1;   
        }
	}

    /// Si el jugador toca el suelo de esta plataforma, el jugador se mantiene en la plataforma moviéndose con esta .
    private void OnCollisionEnter (Collision collision)
    {
        moving = true;
        collision.collider.transform.SetParent(transform);
    }

    /// Si el jugador deja de tocar el suelo de esta plataforma, el jugador deja de ser afectado por esta .
    private void OnCollisionExit(Collision collision)
    {
        collision.collider.transform.SetParent(null);
        moving = false;
        
    }

}
