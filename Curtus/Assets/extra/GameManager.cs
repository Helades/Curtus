using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este script sirve para controlar diversas funciones del juego.
/// </summary>

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject gameOver;
    public GameObject gameLost;
    public float resetDelay;
    public int enemigos;

    public PlayerController player;
    public string saveName;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
        
        enemigos = 0;
    }

    //void Start () {

      //  boton.GetComponent<MenupausaManager>().TogglePauseMenu();
    //}

 
    private void Update()
    {
        if(GameObject.FindGameObjectsWithTag("manzana").Length == 0)
        {
            GameManager.instance.Win();
        }
    }

       /// <summary>
    /// sirve para llamar a la condicion de victoria.
    /// </summary>
    
    
    public void Win()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0.5f;
        Invoke ("Reset", resetDelay);
    }
    
       /// <summary>
    /// sirve para llamar a la condicion de derrota.
    /// </summary>
    
    
    public void Lose()
    {
        gameLost.SetActive(true);
        Time.timeScale = .5f;
        Invoke ("Reset", resetDelay);
    }

    /// <summary>
    /// vuelve a poner el tiempo a 0.
    /// </summary>
    
    void Reset()
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel(Application.loadedLevel);
    }
    
    public int GetEnemigos()
    {
    	return this.enemigos;
    }
    
    public void AddEnemigo()
    {
    	this.enemigos++;
    }
    
    /// <summary>
    /// Sirve para guardar la partida.
    /// </summary>
    
    public void Save()
    {
        Debug.Log("Saving...");
        player.Save();
        SaveLoad.SetSaveSlot(saveName);
        SaveLoad.Save(player.data);
    }

        /// <summary>
    /// Sirve para cargar la partida.
    /// </summary>
    
    
    public void Load()
    {
        Debug.Log("Loading...");
        player.data = SaveLoad.Load<PlayerData>(saveName);
        player.Load();
    }
}
