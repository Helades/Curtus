using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Programacion de la bala.
/// </summary>

public class BulletFire : MonoBehaviour {

	private float bulletForce = 350.0f;
	public GameObject tocho;
    float c = 0;

    private void Update()
    {
    	/// <summary>
    	/// Tras 3 segundos se destruye.
    	/// </summary>
    	/// <param name="target"></param>
        c += c * Time.deltaTime;

            if (c == 3)
            Destroy(this);
    }

    // Use this for initialization
    void OnTriggerEnter2D (Collider2D target) {
    	
    	///Al aparecer automaticamete se dispara.

		if (target.gameObject.tag == "FirePoint") {

			//if (tocho.GetComponent<Enemigo_Tocho>().direction == 1)
			GetComponent<Rigidbody2D>().AddForce(transform.right * bulletForce);

			//if (tocho.GetComponent<Enemigo_Tocho>().direction == -1)
			//GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletForce);

		} 


	}
}
