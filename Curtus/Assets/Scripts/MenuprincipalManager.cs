using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuprincipalManager : MonoBehaviour 
{
	/// Carga del menú principal.
	public void Play()
	{
		SceneManager.LoadScene("menuprincipal");
	}

}
