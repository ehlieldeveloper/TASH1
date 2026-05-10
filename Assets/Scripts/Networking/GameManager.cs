using UnityEngine;
using Mirror;
using TASH.Core;
using TASH.Player;

namespace TASH.Networking
{
    /// <summary>
    /// Main game manager handling game logic and state
    /// </summary>
    public class GameManager : NetworkBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        private GameState gameState;
        private GameObject[] players;
        
        [SyncVar]
        private int seekerCount = 1;
        
        [SyncVar]
        private int hiderCount = 0;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            if (!isServer) return;
            
            gameState = new GameState();
            gameState.SetState(GameStateType.Lobby);
        }

        private void Update()
        {
            if (!isServer) return;

            gameState.UpdateTimer(Time.deltaTime);
            UpdateGameState();
        }

        private void UpdateGameState()
        {
            switch (gameState.CurrentState)
            {
                case GameStateType.Lobby:
                    UpdateLobby();
                    break;
                case GameStateType.Countdown:
                    UpdateCountdown();
                    break;
                case GameStateType.HidingPrep:
                    UpdateHidingPrep();
                    break;
                case GameStateType.Seeking:
                    UpdateSeeking();
                    break;
                case GameStateType.GameOver:
                    UpdateGameOver();
                    break;
            }
        }

        private void UpdateLobby()
        {
            if (NetworkServer.connections.Count >= Constants.MIN_PLAYERS)
            {
                gameState.SetState(GameStateType.Countdown);
                AssignRoles();
            }
        }

        private void UpdateCountdown()
        {
            if (gameState.StateTimer >= Constants.COUNTDOWN_TIME)
            {
                gameState.SetState(GameStateType.HidingPrep);
            }
        }

        private void UpdateHidingPrep()
        {
            if (gameState.StateTimer >= Constants.HIDING_PREP_TIME)
            {
                gameState.SetState(GameStateType.Seeking);
            }
        }

        private void UpdateSeeking()
        {
            if (gameState.StateTimer >= Constants.SEEKING_TIME ||
                gameState.HidersFound >= gameState.TotalHiders)
            {
                gameState.SetState(GameStateType.GameOver);
            }
        }

        private void UpdateGameOver()
        {
            // Game over logic
            if (gameState.StateTimer >= 5f)
            {
                gameState.SetState(GameStateType.Lobby);
                gameState.ResetHidersFound();
            }
        }

        private void AssignRoles()
        {
            players = GameObject.FindGameObjectsWithTag(Constants.PLAYER_TAG);
            seekerCount = Mathf.Max(1, players.Length / 3);
            hiderCount = players.Length - seekerCount;
            gameState.TotalHiders = hiderCount;

            for (int i = 0; i < players.Length; i++)
            {
                PlayerStats stats = players[i].GetComponent<PlayerStats>();
                if (stats != null)
                {
                    PlayerRole role = i < seekerCount ? PlayerRole.Seeker : PlayerRole.Hider;
                    stats.SetRole(role);
                }
            }
        }

        public GameState GetGameState() => gameState;
    }
}