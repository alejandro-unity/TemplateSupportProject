using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MemoryTestInit : MonoBehaviour {

	// Use this for initialization
	public string[] staticObjectsToDestroy;	
	public string sceneName = "MemoryTestAdditive";

	void Start () 
	{

		Scene scene = SceneManager.GetSceneByName(sceneName);
		
		if(!scene.isLoaded) 
		{
			// Loading the test scene 
			SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
		}
	}
}
