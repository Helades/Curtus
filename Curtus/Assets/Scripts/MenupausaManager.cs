using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// Funcionamiento del menú de pausa (tecla P).

public class MenupausaManager : MonoBehaviour 
{

	/// Llama a la imágen del menú de pausa.
	public Image pausa;

	void Start () {

		Time.timeScale = 1;
	}

	void Update ()
	{
		/// Cuando se pulsa la letra P se activa el menú de pausa.

		if (Input.GetKeyDown(KeyCode.P))
		{
			TogglePauseMenu();
		}
	}

		/// Cuando el juego está en pausa congela el tiempo dentro del juego.
	public void TogglePauseMenu()
	{
		pausa.gameObject.SetActive(!pausa.gameObject.activeSelf );
		if (pausa.gameObject.activeSelf) 
			Time.timeScale = 0;
		else 
			Time.timeScale = 1;
	}
	
	//public void Play_menu()
	//{
		//StartCoroutine(LoadYourAsyncSceneFade("menuprincipal"));
	//}

}
