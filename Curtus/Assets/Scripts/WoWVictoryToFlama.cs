using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Este script controla el movimiento de la manzana para que no sea estática.

public class WoWVictoryToFlama : MonoBehaviour {

    public Transform objeto;
    public float ey1;
    public float ey2;
    private int mov = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()   
    {

        /// Simplemente movemos en el eje Y el objeto para que flote.
        objeto.Translate(new Vector3(0.0f, mov * 1.0f, 0.0f) * Time.deltaTime);

        /// Rotamos el objeto para darle más vida.
        objeto.Rotate(new Vector3(mov * 3.0f, 0.0f, -mov * 4.0f));

        /// Cuando baja de ey1 cambia la dirección en el eje positivo de las Y's.
        if (objeto.position.y < ey1)
        {
            mov = 1;
        }

        /// Cuando sobrepasa ey2 cambia la dirección en el eje negativo de las Y's.
        else if (objeto.position.y > ey2)
        {
            mov = -1;
        }
    }
}
