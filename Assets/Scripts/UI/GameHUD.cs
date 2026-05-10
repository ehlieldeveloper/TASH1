using UnityEngine;
using TMPro;
using TASH.Core;
using TASH.Networking;

namespace TASH.UI
{
    /// <summary>
    /// Manages the game HUD display
    /// </summary>
    public class GameHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI gameStateText;
        [SerializeField] private TextMeshProUGUI playersText;
        [SerializeField] private TextMeshProUGUI hidersFoundText;

        private void Update()
        {
            UpdateHUD();
        }

        private void UpdateHUD()
        {
            if (GameManager.Instance == null) return;

            GameState gameState = GameManager.Instance.GetGameState();

            if (timerText != null)
            {
                timerText.text = $"Time: {gameState.StateTimer:F1}s";
            }

            if (gameStateText != null)
            {
                gameStateText.text = $"State: {gameState.CurrentState}";
            }

            if (hidersFoundText != null)
            {
                hidersFoundText.text = $"Found: {gameState.HidersFound}/{gameState.TotalHiders}";
            }
        }
    }
}