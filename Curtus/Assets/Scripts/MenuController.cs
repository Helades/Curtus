using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// Este script permite usar todas las opciones del menú principal del juego.
public class MenuController : MonoBehaviour {

	public GameManager gameManager;
	public Slider loadBar;
	public Image fadeImage;
	public bool activadofundido; 
	public GameObject  text;
	const string fileName = "curtus";

	void Start ()
	{
		if (activadofundido)
		{
			StartCoroutine(Fundidotransparente());
		}		       
	}

	/// Transición a otras escenas .
	public void Play_carga()
	{
		//SaveLoad.saveName = fileName;
		SaveLoad.SetCheckPoint(false);
		StartCoroutine(LoadYourAsyncSceneFade("carga"));
	}
	public void Play_juego()
	{
		StartCoroutine(LoadYourAsyncSceneBar("juego"));
	}
	public void Play_load()
	{
		//SaveLoad.saveName = fileName;
		SaveLoad.SetCheckPoint(true);
		StartCoroutine(LoadYourAsyncSceneFade("carga"));
	}
	
	public void Play_exit()
	{
		text.SetActive(true);
		Time.timeScale = 0.5f;
		Invoke ("Reset", 2);
	}
	
	void Reset()
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel(Application.loadedLevel);
    }
	
	/// Salida al menu principal .
	public void Exit()
	{
		SceneManager.LoadScene("menuprincipal");
	}

	/// Transición de pantallas .
	/// \param sceneName Crea la barra de carga.

	IEnumerator LoadYourAsyncSceneBar(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
		asyncLoad.allowSceneActivation = false;

        while (loadBar.value < 0.9f)
        {

			loadBar.value = asyncLoad.progress < loadBar.value + 0.02f ? asyncLoad.progress : loadBar.value + 0.02f;

			Debug.Log("Progress: " + loadBar.value);

            yield return null;
        }
		asyncLoad.allowSceneActivation = true;
    }

	/// \param sceneName Crea el color de la barra de carga.
	IEnumerator LoadYourAsyncSceneFade(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
		asyncLoad.allowSceneActivation = false;

		float fadeIntensity = 0;

		while(fadeIntensity < 1)
		{
			fadeIntensity += 0.02f;

			Color col = fadeImage.color;
			col.a = fadeIntensity;

			fadeImage.color = col;

			yield return null;
		}
		
		asyncLoad.allowSceneActivation = true;
    }
	
	/// \param sceneName Hace un fundido del menú de carga a la pantalla del juego.
	IEnumerator Fundidotransparente()
	{
		float fadeIntensity = 1;

		while(fadeIntensity >= 0 && fadeImage != null)
		{
			fadeIntensity -= 0.02f;

			Color col = fadeImage.color;
			col.a = fadeIntensity;

			fadeImage.color = col;

			yield return null;
		}

		StartCoroutine(LoadYourAsyncSceneBar("juego"));

	}
}
