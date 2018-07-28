using System.Collections.Generic;
using UnityEngine;

public class ResMgr {

    /// <summary>
    /// 共享的资源
    /// </summary>
    public static List<Res> SharedLoadedReses = new List<Res>();

    public static Res GetRes(string assetName,string bundleName = null)
    {
        Res res = SharedLoadedReses.Find(loadedAsset => loadedAsset.Name == assetName);

        if (res != null)
        {
            res.Retain();
            return res;
        }

        if (bundleName == null)
            res = new ResourceRes(assetName);
        else
            res = new AssetRes(assetName, bundleName);

        res.Load();
        SharedLoadedReses.Add(res);
        res.Retain();

        return res;
    }
}
