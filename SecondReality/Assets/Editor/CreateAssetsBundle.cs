using UnityEditor;


public class CreateAssetsBundle 
{
    [MenuItem("Assets/AssetBundles/Build AssetBundles All")]
    public static void BuildAllAssetsBundle()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/Android", BuildAssetBundleOptions.None, BuildTarget.Android);
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/Ios", BuildAssetBundleOptions.None, BuildTarget.iOS);
    }

    [MenuItem("Assets/AssetBundles/Build AssetBundles Android")]
    public static void BuildAndroidAssetsBundle()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/Android", BuildAssetBundleOptions.None, BuildTarget.Android);
    }

    [MenuItem("Assets/AssetBundles/Build AssetBundles Ios")]
    public static void BuildIosAssetsBundle()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/Ios", BuildAssetBundleOptions.None, BuildTarget.iOS);
    }
}
