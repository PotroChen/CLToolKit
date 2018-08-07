using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AssetTable {
    public List<AssetBundleInfo> AssetBundleInfos = new List<AssetBundleInfo>();

    private static AssetTable instance;
    public static AssetTable Instance{ get { return instance; }}

    public static void Load()
    {
        Chenlin.SerializeUtil.XmlHelper.DeSerializeFromFile(out instance, Application.streamingAssetsPath + "/AssetBundles/AssetTable.xml");
        if (instance == null)
            Debug.LogError("AssetTable 读取失败");
    }

    /// <summary>
    /// 根据AssetName获取AssetBundleName
    /// </summary>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public string GetAssetBundleName(string assetName)
    {
        //TODO 搜索算法有待更新
        foreach (AssetBundleInfo assetBundleInfo in AssetBundleInfos)
        {
            AssetInfo searchedAssetInfo = assetBundleInfo.AssetInfos.Find(assetInfo => assetInfo.AssetName == assetName);
            if(searchedAssetInfo!=null)
                return searchedAssetInfo.OwnerAssetBundleName;
        }

        return null;
    }
}
