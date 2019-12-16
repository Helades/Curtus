using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Controla el guardado y la carga de la partida.
/// </summary>

public static class SaveLoad
{
	public static string saveName = "curtus";
    public static string dataPath = Application.streamingAssetsPath;
    public static string folderName = "Saves";
    public static string extension = ".dat";

    /// <summary>
    /// Se setea el nombre del save. Hay que llamarlo antes de guardar al menos la primera vez.
    /// </summary>
    /// <param name="saveName"></param>
    public static void SetSaveSlot(string saveName)
    {
        SaveLoad.saveName = saveName;
    }

    /// <summary>
    /// Guarda un objeto de una clase serializable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="objectData"></param>
    public static void Save<T>(T objectData)
    {
        string folderPath = Path.Combine(dataPath, folderName);

        if(!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string fullDataPath = Path.Combine(folderPath, saveName + extension);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(fullDataPath);
        bf.Serialize(file, objectData);
        file.Close();

        Debug.Log("File saved to" + fullDataPath);
    }

    /// <summary>
    /// Carga un objeto de una clase serializable, dado el nombre
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public static T Load<T>(string name)
    {
        string fullDataPath = Path.Combine(Path.Combine(dataPath, folderName), name + extension);
        T data = default(T);

        if(File.Exists(fullDataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(fullDataPath, FileMode.Open);
            data = (T)bf.Deserialize(file);
            file.Close();

            Debug.Log("Loading file from: " + fullDataPath);
        }
        else
        {
            Debug.Log("File not found at: " + fullDataPath);
        }

        return data;
    }
    
    public static bool SetCheckPoint(bool checkPoint)
    {
    	//saveName = GameManager.instance.saveName;
    	PlayerData data = Load<PlayerData>(saveName);
		data.checkPoint = checkPoint;
		Debug.Log("SetCheckPoint");
		//Debug.Log("Nuevo: " + checkPoint);
		//Debug.Log("Viejo: " + data.checkPoint);
		//Save<PlayerData>(data);
    	return checkPoint;
    }
    
    public static bool GetCheckPoint()
    {
     //saveName = GameManager.instance.saveName;
    	PlayerData data = Load<PlayerData>(saveName);
    	Debug.Log("GetCheckPoint");
//		Debug.Log( );
		return data.checkPoint;
    }
}