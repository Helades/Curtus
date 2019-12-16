using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Este script controla las cajas capaces de ser movidas dentro del juego.

public class CajaImportante : MonoBehaviour {

    private Rigidbody rb;

    // Use this for initialization
    void Start () {

        /// Obtenemos el componente Rigidbody.
        rb = GetComponent<Rigidbody>();
    }
	
	/// Update is called once per frame.
	void Update () {

        /// Si la caja se cae, la volvemos a colocar en la posiciín inicial.
        if (rb.position.y < -10)
        {
            rb.position = new Vector3(7.0f, 3.0f, 4.0f);
        }
    }
}
