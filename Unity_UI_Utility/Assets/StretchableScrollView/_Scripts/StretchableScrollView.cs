using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StretchableScrollView : MonoBehaviour {
    public RectTransform viewPort;
    public Scrollbar verticalScrollBar;

    private VerticalLayoutGroup verLayGro;
    private RectTransform recTra;
    private ScrollRect scrRec;

	void Start () {
        Init();
        UpdateScrollView();
	}
    private void Init()
    {
        scrRec = GetComponent<ScrollRect>();
        recTra = GetComponent<RectTransform>();
        verLayGro = viewPort.GetComponent<VerticalLayoutGroup>();
        if (verLayGro == null)
        {
            Debug.LogError("此脚本是用来拓展‘VerticalLayoutGroup’，此物体没有‘VerticalLayoutGroup’");
        }
        viewPort.pivot = new Vector2(viewPort.pivot.x, 1f); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            UpdateScrollView();
    }

    private void UpdateScrollView()
    {
        #region 根据子物体计算高度
        float height = 0;
        int top = verLayGro.padding.top;
        int bottom = verLayGro.padding.bottom;
        float spaces = (viewPort.childCount - 1) * verLayGro.spacing;

        float childrenHeight = 0;
        for (int i = 0; i < viewPort.childCount;i++)
        {
            childrenHeight += viewPort.GetChild(0).GetComponent<RectTransform>().rect.height;
        }

        height = top + bottom + spaces + childrenHeight;
        viewPort.sizeDelta = new Vector2(viewPort.sizeDelta.x,height);
        #endregion

        //若高度大于父物体的高度，显示scrollbar
        if (height < recTra.rect.height)
        {
            scrRec.verticalScrollbar = null;
            verticalScrollBar.gameObject.SetActive(false);
        }
        else
        {
            scrRec.verticalScrollbar = verticalScrollBar;
            verticalScrollBar.gameObject.SetActive(true);
        }


    }
}
