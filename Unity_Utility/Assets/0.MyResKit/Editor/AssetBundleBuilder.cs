using UnityEngine;
using UnityEditor;
using System.IO;

public class AssetBundleBuilder : MonoBehaviour {

    [MenuItem("ResKit/BuildAsset")]
    public static void BuildAsset()
    {
        var outputPath = Application.streamingAssetsPath + "/AssetBundles";

        if (!Directory.Exists(outputPath))
        {
            Debug.LogWarning("This path doesn't exit,create a new one");
            Directory.CreateDirectory(outputPath);
        }

        BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.ChunkBasedCompression,
            BuildTarget.StandaloneWindows64);
    }
}
