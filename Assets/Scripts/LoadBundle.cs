using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBundle : MonoBehaviour {

	//Test Script for loading bundles

	public AssetBundle bundle;
	// Set to your Home 
	/*
		Your home path is =  /Volumes/Data/files.unity3d.com/yourUser/
		Unity SFTP server  homes.hq.unity3d.com 
		public url is http://files.unity3d.com/yourUser/
	*/
	public string urlServer = "http://files.unity3d.com/alejandro/";
	public string bundleName = "demobundle";
	public string[] gosInbundleName = new string[]{ "demobundle" };
	public bool cleanCache = true;
	public bool unload = true;
	WWW www;
	string log = "";

	void Start ()
	{	
			
	}

	void OperateBundle ()
	{
		print ("Openning bundle.....");
		for (int i = 0; i < gosInbundleName.Length; i++) {
			GameObject go = bundle.LoadAsset<GameObject> (gosInbundleName [i]);
			Instantiate (go);
		}
		if (unload) 
		{
			bundle.Unload (false);
			//bundle.Unload (true);
		}
	}
	IEnumerator LoadFromStreammingAsset ()
	{

		if (bundle != null)
			bundle.Unload (true);

		AssetBundleCreateRequest bundleLoadRequest = AssetBundle.LoadFromFileAsync (Application.streamingAssetsPath +  "/"+  bundleName );
		yield return bundleLoadRequest;
		bundle = bundleLoadRequest.assetBundle;
		if (bundle == null) {
			Debug.Log ("Failed to load AssetBundle!");
			yield break;
		}	

		OperateBundle();
	}
	IEnumerator LoadBundleInternet ()
	{
		
		if( cleanCache )
			Caching.CleanCache();
		
		while( !Caching.ready )
		{
			yield return null;
		}
		
		if (bundle != null)
			bundle.Unload (true);

		www = WWW.LoadFromCacheOrDownload( urlServer + bundleName , 1 );
		Debug.Log("Loading from " + urlServer + bundleName);
		while(!www.isDone){
			log = "Loading bundle : "+ www.progress;
			yield return null;
		}
		print ("Loading form internet");
		bundle = www.assetBundle;
		OperateBundle();
		//www.Dispose();
		
	}
	void OnGUI()
	{
		if(GUILayout.Button("Load Bundle From StreamingAssets" , GUILayout.Width(Screen.width/3) , GUILayout.Height(Screen.height/3))){

			StartCoroutine(LoadFromStreammingAsset());
		}
		if(GUILayout.Button("Load Bundle From internet " , GUILayout.Width(Screen.width/3) , GUILayout.Height(Screen.height/3))){

			StartCoroutine(LoadBundleInternet());
		}

		GUILayout.Label(log);

	}
}
