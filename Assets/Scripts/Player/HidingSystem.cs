using UnityEngine;
using Mirror;
using TASH.Core;

namespace TASH.Player
{
    /// <summary>
    /// Handles hiding mechanics for hiders
    /// </summary>
    public class HidingSystem : NetworkBehaviour
    {
        [SyncVar]
        private bool isHiding = false;
        
        private PlayerStats playerStats;
        private float checkInterval = Constants.HIDING_CHECK_INTERVAL;
        private float checkTimer = 0f;

        public bool IsHiding() => isHiding;

        private void Start()
        {
            playerStats = GetComponent<PlayerStats>();
        }

        private void Update()
        {
            if (!isServer) return;
            if (playerStats.GetRole() != PlayerRole.Hider) return;

            checkTimer -= Time.deltaTime;
            
            if (checkTimer <= 0f)
            {
                CheckHidingStatus();
                checkTimer = checkInterval;
            }
        }

        private void CheckHidingStatus()
        {
            Collider[] nearbyObjects = Physics.OverlapSphere(
                transform.position,
                Constants.HIDING_RADIUS
            );

            foreach (Collider col in nearbyObjects)
            {
                if (col.CompareTag(Constants.HIDING_SPOT_TAG))
                {
                    isHiding = true;
                    return;
                }
            }

            isHiding = false;
        }

        [Command]
        public void CmdRequestHide()
        {
            CheckHidingStatus();
        }
    }
}