using UnityEngine;
public class UIManager : Singleton<UIManager>
{
    public GameObject[] panels;
    public void CloseAllPanels()
    {
        foreach (var t in panels)
        {
            t.SetActive(false);
        }
    }
    public void ChangeUI( int panelIdx)
    {
        panels[panelIdx].SetActive(true);
    }
}
