using System;
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
        set
        {
            if (value == 0)
            {
                _scoreCount = 0;
            }
            else
            {
                _scoreCount += ComboCount * value;
            }
        } 
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
          uiManager.HighScore();
        }
        if (newState == GameState.Lose)
        {
            uiManager.ChangeUI(3);
            uiManager.HighScore();
        }
        OnGameStateChanged?.Invoke(newState);
    }
}