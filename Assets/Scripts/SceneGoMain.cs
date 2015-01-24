using UnityEngine;
using System.Collections;

public class SceneGoMain : MonoBehaviour 
{
	
	public void GoMainScene(string 	MainGameplay)
	{
		Application.LoadLevel("MainGameplay");
	}
}
