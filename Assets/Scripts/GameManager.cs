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
        set => _scoreCount += ComboCount * value;
    }

    protected override void Awake()
    {
        base.Awake();
        UpdateGameState(GameState.Play);
    }

    void Start()
    {


    }

    public enum GameState
    {
        Play,
        Pause,
        Lose,
        Win,
        ChooseLevel,
    }

    public GameState state;

    public void UpdateGameState(GameState newState)
    {
        var sfxManager = SfxManager.Instance;
        var uiManager = UIManager.Instance;
        var musicManager = MusicManager.Instance;
        state = newState;
        if (newState == GameState.Pause)
        {

        }
        else if (newState == GameState.Play)
        {
            musicManager.PlayMusic();
            HelixManager.Instance.InitialHelixSpawner();
        }
        else if (newState == GameState.ChooseLevel)
        {

        }
        else if (newState == GameState.Lose)
        {

        }
        else if (newState == GameState.Win)
        {

        }
        else throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        OnGameStateChanged?.Invoke(newState);
    }
    
}