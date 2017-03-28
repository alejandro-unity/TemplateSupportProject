using UnityEngine;
using System.Collections;
using UnityEditor;

public class Utilities  {


	[MenuItem("Support/Android/PrintAndroidNKD")]
	public static void PrintAndroidNKD ()
	{

		var ndkPath = System.Environment.GetEnvironmentVariable("ANDROID_NDK_ROOT");
		Debug.Log(ndkPath);

		EditorUserBuildSettings.androidBuildSubtarget =  MobileTextureSubtarget.ETC2;
	}	

	[MenuItem("Support/iOS/SetBundleID_TeamID")]
	public static void SetBundleID ()
	{
		PlayerSettings.bundleIdentifier  = "com.bogota.soporte1";
		PlayerSettings.iOS.appleDeveloperTeamID = ""; 
		Debug.Log("Auto Signing for iOS"); 
	}

}
