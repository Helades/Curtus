using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Este script controla los sonidos del juego.

public class SoundManagerScript : MonoBehaviour
{

    /// Música de fondo .
    public AudioClip music;

    public AudioSource audioSrc;

    // Use this for initialization
    void Start()
    {
        audioSrc.clip = music;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}