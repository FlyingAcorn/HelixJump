using TMPro;
using UnityEngine;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI[] scoreTexts;
    [SerializeField] private TextMeshProUGUI[] highScoreTexts;
    [SerializeField] private TextMeshProUGUI combo;
    
    
    
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

    public void Score()
    {
        foreach (var t in scoreTexts)
        {
            t.text = GameManager.Instance.ScoreCount.ToString();
        }
    }

    public void HighScore()
    {
        if (GameManager.Instance.ScoreCount > PlayerPrefs.GetInt("HighScore",0) )
        {
            PlayerPrefs.SetInt("HighScore", GameManager.Instance.ScoreCount);
        }

        foreach (var t in highScoreTexts)
        {
            t.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
    }

    public void Combo()
    {
        combo.text = "x" + (GameManager.Instance.ComboCount -1);
    }
}
