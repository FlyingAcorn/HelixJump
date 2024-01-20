using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Image[] tickAndCross;
    [SerializeField] private AudioMixer audioMixer;
    public void SetSensitivity(float level)
    {
        HelixManager.Instance.rotationSpeed = level;
    }

    public void ReverseMovement()
    {
        HelixManager.Instance.reverse = !HelixManager.Instance.reverse;
        tickAndCross[0].gameObject.SetActive(!HelixManager.Instance.reverse);
        tickAndCross[1].gameObject.SetActive(HelixManager.Instance.reverse);
    }
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("SoundMaster", Mathf.Log10(volume) * 20f);
    }
    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("SoundFX", Mathf.Log10(volume) * 20f);
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("SoundMusic", Mathf.Log10(volume) * 20f);
    }
    public void StartGame()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Play);
    }
    public void Leave()
    {
        Application.Quit();
    }
    public void Pause()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Pause);
    }

    public void Continue()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Continue);
    }

   
    public void Restart()
    {
        // burada dummy açıp yapmak yerine for kullanabilirdin.Yüksek ihtimalle o daha verimli 
        //cihana sor
        var dummyList = new List<GameObject>(HelixManager.Instance.pooledActiveHelixes);
        foreach (var t in dummyList)
        {
            t.SetActive(false);
        }

        GameManager.Instance.ScoreCount = 0;
        GameManager.Instance.ComboCount = 1;
        UIManager.Instance.Score();
        UIManager.Instance.Combo();
        GameManager.Instance.UpdateGameState(GameManager.GameState.Play);
    }

    public void Settings()
    {
        if (GameManager.Instance.state == GameManager.GameState.MainMenu)
        {
            UIManager.Instance.panels[1].SetActive(false);
            UIManager.Instance.panels[4].SetActive(true);
        }
        else if (GameManager.Instance.state is GameManager.GameState.Play or GameManager.GameState.Continue)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Settings);
        }
    }

    public void Return()
    {
        UIManager.Instance.panels[4].SetActive(false);
        if (GameManager.Instance.state == GameManager.GameState.MainMenu)
        {
            UIManager.Instance.ChangeUI(1);
        }
        else if (GameManager.Instance.state == GameManager.GameState.Settings) // else de olurdu yuksek ihtimalle
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Continue);
        }
        
    }
}
