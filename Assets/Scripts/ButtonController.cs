using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    
    // bu setvolume kısmını slider yaptığında canvasa bağlayacaksın
    // value aralığı min 0.0001 ile 1
    //string olarakta mixer grubundaki adını alıcak(exposed kısmındakı sağust)
   
    
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
        else if (GameManager.Instance.state == GameManager.GameState.Settings)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Continue);
        }
        
    }
}
