using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour {

    protected static Dictionary<string, UIPanel> panels = new Dictionary<string, UIPanel>();

    private static Transform uiRootTrans;
    protected static Transform UIRootTrans
    {
        get
        {
            if (!uiRootTrans)
                uiRootTrans = GameObject.Find("Canvas").transform;
            return uiRootTrans;
        }
    }

    private ResLoader resLoader;

    protected virtual void Init()
    {
        panels.Add(this.name, this);
    }

    public static void OpenPanel<TPanel>() where TPanel : UIPanel
    {
        string panelName = typeof(TPanel).ToString();
        string[] splitedName = panelName.Split('.');
        panelName = splitedName[splitedName.Length - 1];

        if (panels.ContainsKey(panelName))
        {
            panels[panelName].Open();
        }
        else
        {
            ResLoader resLoader = new ResLoader();

            GameObject prefab = resLoader.LoadAsset<GameObject>("resources://"+panelName);
            GameObject instance = GameObject.Instantiate(prefab, UIPanel.UIRootTrans);
            TPanel panel = instance.GetComponent<TPanel>();
            panels.Add(panelName,panel);
            panel.resLoader = resLoader;
            panel.Init();
        }
    }

    public void ClosePanel()
    {
        resLoader.UnLoadAll();
        Destroy(this.gameObject);
        panels.Remove(this.name);
    }


    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
