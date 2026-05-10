using UnityEngine;
using Mirror;
using TASH.Core;

namespace TASH.Player
{
    /// <summary>
    /// Tracks player statistics and role
    /// </summary>
    public class PlayerStats : NetworkBehaviour
    {
        [SyncVar]
        private PlayerRole playerRole = PlayerRole.Unassigned;
        
        [SyncVar]
        private string playerName = "Player";
        
        [SyncVar]
        private bool isFound = false;
        
        [SyncVar]
        private float hidingTime = 0f;

        public PlayerRole GetRole() => playerRole;
        public string GetPlayerName() => playerName;
        public bool IsFound() => isFound;
        public float GetHidingTime() => hidingTime;

        [Server]
        public void SetRole(PlayerRole role)
        {
            playerRole = role;
        }

        [Server]
        public void SetPlayerName(string name)
        {
            playerName = name;
        }

        [Server]
        public void FoundBySeeker()
        {
            isFound = true;
        }

        [Server]
        public void ResetStats()
        {
            isFound = false;
            hidingTime = 0f;
        }

        [Server]
        public void UpdateHidingTime(float deltaTime)
        {
            if (!isFound)
            {
                hidingTime += deltaTime;
            }
        }
    }
}