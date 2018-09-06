using UnityEngine.UI;

public class UIMainPanel : UIPanel
{
    protected override void Init()
    {
        base.Init();

        transform.Find("Button").GetComponent<Button>().onClick.AddListener(()=> { UIPanel.OpenPanel<UIGamePanel>(); ClosePanel(); });
    }

}
