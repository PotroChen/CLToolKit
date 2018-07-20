using UnityEngine;
using System.Collections.Generic;

public class ResourceManagerTest
{
    private void Start()
    {

    }
}


public class ResourceLoader : MonoSingleton<ResourceLoader> {

    public List<Res> loadedObjects = new List<Res>();

    public T LoadAsset<T>(string assetName) where T :Object
    {
        Res loadedRes = loadedObjects.Find(loadedAsset => loadedAsset.Name == assetName);

        if (loadedRes != null)
        {
            loadedRes.Retain();
            return loadedRes as T;
        }

        var asset = Resources.Load<T>(assetName);

        Res res = new Res(asset);
        res.Retain();

        loadedObjects.Add(res);

        return res as T;
    }

    public void UnLoadAll()
    {
        for (int i = 0; i < loadedObjects.Count; i++)
        {
            loadedObjects[i].Release();
        }
    }
}

public class Res
{
    public string Name { get { return asset.name; } }

    private Object asset;
    private int refCount;

    public Res(Object asset)
    {
        this.asset = asset;
    }

    public void Retain()
    {
        refCount++;
    }

    public void Release()
    {
        refCount--;
        if (refCount <= 0)
        {
            refCount = 0;
            Resources.UnloadAsset(asset);
        }
    }
}


