using System;
public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnGameStateChanged; 
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
        state = newState;
        if (newState == GameState.Pause)
        {
            
        }
        else if (newState == GameState.Play)
        {
            
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
