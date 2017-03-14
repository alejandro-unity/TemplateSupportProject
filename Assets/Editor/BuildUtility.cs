using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class BuildUtility  {

	[MenuItem("Support/BuildBundles_WithBuildArray")]
	public static void BuildBundles_WithBuildArray ()
	{
		// Example of building bundles with array 
		string outputPath = "../Bundles";
		Directory.CreateDirectory (outputPath);
		// Create the array of bundle build details.
		AssetBundleBuild[] buildMap = new AssetBundleBuild[2];
		buildMap[0].assetBundleName = "DemoBundle";
		buildMap[0].assetNames = new[] { "Assets/Cube.prefab" };
		BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
		BuildAssetBundleOptions options = BuildAssetBundleOptions.UncompressedAssetBundle | BuildAssetBundleOptions.ForceRebuildAssetBundle;
		BuildPipeline.BuildAssetBundles (outputPath ,  buildMap ,  options , target );
		Debug.Log("Building bundles for " + target.ToString());
	}

	[MenuItem("Support/BuildBundles_Automatic")]
	public static void BuildBundles_Automatic ()
	{
		string outputPath = "../Bundles";
		Directory.CreateDirectory (outputPath);
		BuildAssetBundleOptions options = BuildAssetBundleOptions.UncompressedAssetBundle | BuildAssetBundleOptions.ForceRebuildAssetBundle;
		BuildBundles(options , outputPath);
		Debug.Log("Bundles ready in ../Bundles folder ");
	}

	[MenuItem("Support/BuildBundles_StreamingAssets")]
	public static void BuildBundles_StreamingAssets ()
	{
		if( !Directory.Exists(Application.streamingAssetsPath))
			Directory.CreateDirectory (Application.streamingAssetsPath);		
		BuildAssetBundleOptions options = BuildAssetBundleOptions.UncompressedAssetBundle | BuildAssetBundleOptions.ForceRebuildAssetBundle;
		BuildBundles(options , Application.streamingAssetsPath);
		Debug.Log("Bundles ready in StreamingAssetsPath ");
		AssetDatabase.Refresh();

	}
	public static void BuildBundles(BuildAssetBundleOptions options , string outputPath )
	{
			
		BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
		BuildPipeline.BuildAssetBundles (outputPath ,  options , target  );
		Debug.Log("Building bundles for " + target.ToString());
	}

	[MenuItem("Support/Bundles/Print Contents From Selection")]
	static void PrintContents()
	{
		if( Selection.activeObject == null )
			return;

		AssetBundle bundle = AssetBundle.LoadFromFile( Application.dataPath + AssetDatabase.GetAssetPath( Selection.activeObject ).Remove(0,6) );

		if( bundle != null )
		{
			SerializedObject so = new SerializedObject( bundle );
			System.Text.StringBuilder str = new System.Text.StringBuilder();

			str.Append( "Preload table:\n" );
			foreach( SerializedProperty d in so.FindProperty( "m_PreloadTable" ) )
			{
				if( d.objectReferenceValue != null )
					str.Append( "\t<color=green>" + d.objectReferenceValue.name + "</color> " + d.objectReferenceValue.GetType().ToString() + "\n" );
			}

			str.Append( "Container:\n" );
			foreach( SerializedProperty d in so.FindProperty( "m_Container" ) )
				str.Append( "\t" + d.displayName + "\n" );

			Debug.Log( str.ToString() );
			bundle.Unload( false );
		}
	}


}
