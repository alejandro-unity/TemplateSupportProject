using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MemoryTestInit : MonoBehaviour {

	// Use this for initialization
	public string[] staticObjectsToDestroy;	
	//string sceneName = "MemoryTestAdditive";
	[RuntimeInitializeOnLoadMethod]
	static void OnRuntimeMethodLoad()
    {
        Debug.Log("After scene is loaded and game is running");
		Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName("MemoryTestAdditive");
		if(!scene.isLoaded) 
		{
			// Loading the test scene 
			UnityEngine.SceneManagement.SceneManager.LoadScene("MemoryTestAdditive", LoadSceneMode.Additive);
		}
    }

	
	/*
	void Start () 
	{

		Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
		
		if(!scene.isLoaded) 
		{
			// Loading the test scene 
			UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
		}
	}
	*/
}
