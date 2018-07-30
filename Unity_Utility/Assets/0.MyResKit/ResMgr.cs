using System.Collections.Generic;
using UnityEngine;

public class ResMgr {

    /// <summary>
    /// 共享的资源
    /// </summary>
    public static List<Res> SharedLoadedReses = new List<Res>();//Resource里的资源和AssetBundle的资源重名的话，还没有做区分

    public static Res GetRes(string assetName,string assetBundleName = null)
    {
        Res res = SharedLoadedReses.Find(loadedAsset => loadedAsset.Name == assetName);

        if (res != null)
        {
            res.Retain();
            return res;
        }

        if (string.IsNullOrEmpty(assetBundleName))
            res = new ResourceRes(assetName);
        else
            res = new AssetRes(assetName, assetBundleName);

        res.Load();
        SharedLoadedReses.Add(res);
        res.Retain();

        return res;
    }

    public static AssetBundleRes GetAssetBundleRes(string assetName)
    {
        Res res = SharedLoadedReses.Find(loadedAsset => loadedAsset.Name == assetName);

        if (res != null)
        {
            res.Retain();
            return res as AssetBundleRes;
        }

        res = new AssetBundleRes(assetName);
        res.Load();
        SharedLoadedReses.Add(res);
        res.Retain();

        return res as AssetBundleRes;

    }
}
