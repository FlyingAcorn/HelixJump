using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnGameStateChanged;
    private int _comboCount;
    private int _scoreCount;

    public int ComboCount
    {
        get => _comboCount;
        set => _comboCount = value;
    }

    public int ScoreCount
    {
        get => _scoreCount;
        set => _scoreCount += ComboCount * value;
    }
    
    public enum GameState
    {
        Play,
        Continue,
        Pause,
        Lose,
        Settings,
        MainMenu,
    }
    
    private void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }

    public GameState state;

    public void UpdateGameState(GameState newState)
    {
        var sfxManager = SfxManager.Instance;
        var uiManager = UIManager.Instance;
        var musicManager = MusicManager.Instance;
        state = newState;
        uiManager.CloseAllPanels();
        if (newState == GameState.Pause)
        {
          uiManager.ChangeUI(2);
        }
        if (newState == GameState.Continue)
        {
            uiManager.ChangeUI(0);
        }
        if (newState == GameState.Settings)
        {
            uiManager.ChangeUI(4);
        }
        if (newState == GameState.Play)
        {
            uiManager.ChangeUI(0);
            musicManager.PlayMusic();
            HelixManager.Instance.InitialHelixSpawner();
        }
        if (newState == GameState.MainMenu)
        {
          uiManager.ChangeUI(1);
        }
        if (newState == GameState.Lose)
        {
            uiManager.ChangeUI(3);
        }
        OnGameStateChanged?.Invoke(newState);
    }
    
}