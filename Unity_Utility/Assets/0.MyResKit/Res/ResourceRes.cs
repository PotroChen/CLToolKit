using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRes : Res
{
    public ResourceRes(string asset) : base(asset)
    {

    }

    public override void Load()
    {
        base.Load();
        asset = Resources.Load(Name);
    }

    protected override void UnLoad()
    {
        base.UnLoad();
        Resources.UnloadAsset(asset);
    }


}
