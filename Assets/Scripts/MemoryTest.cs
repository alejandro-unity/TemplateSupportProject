using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class MemoryTest : MonoBehaviour {

#if UNITY_IOS
	[DllImport ("__Internal")]
	private static extern int report_memory();
#endif

	public Text  memoryIOS;
	public bool dontDestroyOnLoad = false;
	private Button[] uiButtons;
	public Transform panel;
	List<UnityEngine.Events.UnityAction> listActions = new List<UnityEngine.Events.UnityAction>();
	public string[] staticObjectsToDestroy;	

	public GameObject prefabButton;
	/* 
	TODO :
	add android memory report 	
	*/

	void Start () 
	{
		gameObject.name = "Support::MemoryTest";
		memoryIOS.text = "0";		

		if (dontDestroyOnLoad) 
			DontDestroyOnLoad(this);
		
		// add you method here 
			listActions.Add(LoadEmptyScene);
			listActions.Add(CheckUnloadUnusedAssets);
			listActions.Add(ReportNativeMemory);
			listActions.Add(RestoreScene);
			listActions.Add(DestroyObjects);

		for(int i = 0; i<listActions.Count; i++){
			GameObject button = (GameObject)Instantiate(prefabButton , panel );
			button.SetActive(true);
			
			// set listener and name automatically 
			button.GetComponent<Button>().onClick.AddListener( listActions[i]);
			button.GetComponent<Button>().transform.GetChild(0).GetComponent<Text>().text = listActions[i].Method.ToString();

		}	
		CheckUnloadUnusedAssets();		
	}

	public void LoadEmptyScene(){
		print("LoadEmptyScene");
		SceneManager.LoadScene ("EmptyScene" , UnityEngine.SceneManagement.LoadSceneMode.Single);
		SceneManager.LoadScene("MemoryTestAdditive", LoadSceneMode.Additive);

	}
	public void CheckUnloadUnusedAssets(){
		print("UnloadUnusedAssets");
		Resources.UnloadUnusedAssets();
	}
	public void ReportNativeMemory(){
		#if UNITY_IOS
		print("ReportNativeMemory");
		memoryIOS.text = report_memory().ToString ();
		print(memoryIOS);
		#endif
	}


	public void DestroyObjects (){

		foreach (  string s  in staticObjectsToDestroy)
		{
			GameObject go = GameObject.Find(s);			
			print("Destroy " + s  );
			if (go) Destroy(go);
		}
		/*
		GameObject go = GameObject.Find("StartLevel");
			if (go) Destroy(go);
		GameObject go2 = GameObject.Find("[DOTween]");
			if (go2) Destroy(go);	*/
	}
	public void RestoreScene(){
		print("RestoreScene");
		UnityEngine.SceneManagement.SceneManager.LoadScene (0 , UnityEngine.SceneManagement.LoadSceneMode.Single);
	}

	
}
