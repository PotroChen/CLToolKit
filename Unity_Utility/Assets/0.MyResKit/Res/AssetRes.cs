using UnityEngine;

public class AssetRes : Res {

    private string assetBundleName;
    private AssetBundleRes assetBundleRes;

    public AssetRes(string assetName,string assetBundleName) : base(assetName)
    {
        this.assetBundleName = assetBundleName;
    }


    public override void Load()
    {
        base.Load();
        assetBundleRes = ResMgr.GetAssetBundleRes(assetBundleName);
        asset = (assetBundleRes.Asset as AssetBundle).LoadAsset(Name);
    }

    protected override void UnLoad()
    {
        base.UnLoad();
        if (asset is GameObject)
        {
        }
        else
        {
            Resources.UnloadAsset(asset);
        }
        assetBundleRes.Release();
    }

}
