using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour {

    protected static Dictionary<string, UIPanel> panels = new Dictionary<string, UIPanel>();

    private static Transform uiRootTrans;
    protected static Transform UIRootTrans
    {
        get
        {
            if (uiRootTrans == null)
                uiRootTrans = GameObject.Find("Canvas").transform;
            return uiRootTrans;
        }
    }

    public void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        panels.Add(this.name, this);
    }

    public static void OpenPanel<TPanel>() where TPanel : UIPanel
    {
        string panelName = typeof(TPanel).ToString();
        GameObject prefab = Resources.Load<GameObject>(panelName);
        GameObject instance = GameObject.Instantiate(prefab, UIPanel.UIRootTrans);
    }

    public void ClosePanel()
    {
        Destroy(this.gameObject);
        panels.Remove(this.name);
    }
}
