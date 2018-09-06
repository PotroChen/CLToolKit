using UnityEngine.UI;

public class UIGamePanel : UIPanel
{
    protected override void Init()
    {
        base.Init();

        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => { UIPanel.OpenPanel<UIMainPanel>();  ClosePanel(); });
    }
}
