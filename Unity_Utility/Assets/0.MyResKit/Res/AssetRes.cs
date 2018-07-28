using UnityEngine;

public class AssetRes : Res {

    private string assetBundleName;

    public AssetRes(string assetName,string assetBundleName) : base(assetName)
    {
        this.assetBundleName = assetBundleName;
    }


    public override void Load()
    {
        base.Load();

    }

    protected override void UnLoad()
    {
        base.UnLoad();
    }

}
