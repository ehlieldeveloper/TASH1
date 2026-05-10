namespace TASH.Core
{
    /// <summary>
    /// Enum for different game states
    /// </summary>
    public enum GameStateType
    {
        Lobby,          // Waiting for players
        Countdown,      // Starting game countdown
        HidingPrep,     // Hiders preparing to hide
        Seeking,        // Main game phase
        GameOver        // Game ended
    }

    /// <summary>
    /// Tracks current game state
    /// </summary>
    public class GameState
    {
        public GameStateType CurrentState { get; private set; }
        public float StateTimer { get; private set; }
        public int HidersFound { get; private set; }
        public int TotalHiders { get; private set; }

        public GameState()
        {
            CurrentState = GameStateType.Lobby;
            StateTimer = 0f;
            HidersFound = 0;
            TotalHiders = 0;
        }

        public void SetState(GameStateType newState)
        {
            CurrentState = newState;
            StateTimer = 0f;
        }

        public void UpdateTimer(float deltaTime)
        {
            StateTimer += deltaTime;
        }

        public void IncrementHidersFound()
        {
            HidersFound++;
        }

        public void ResetHidersFound()
        {
            HidersFound = 0;
        }
    }
}