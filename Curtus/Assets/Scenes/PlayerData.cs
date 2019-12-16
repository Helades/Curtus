using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Guarda los checkpoints.
/// </summary>

[System.Serializable]
public class PlayerData
{

    /// Guardar localizaciones del jugador para los puntos de carga .
    public int points;
    public bool checkPoint;

    [System.Serializable]
    public struct MyVector3
    {
        float x;
        float y;
        float z;

        public MyVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 getVector()
        {
            return new Vector3(x, y, x);
        }
    }

    public MyVector3 saved_position;
}