using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerProfe : MonoBehaviour
{
    public PlayerData data;

    public float speed = 1;

    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed, 0);
    }

    public void Save()
    {
        data.saved_position = new PlayerData.MyVector3(transform.position);
    }

    public void Load()
    {
        transform.position = data.saved_position.getVector();
    }
}