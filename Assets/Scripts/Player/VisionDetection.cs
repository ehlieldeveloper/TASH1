using UnityEngine;
using Mirror;
using TASH.Core;

namespace TASH.Player
{
    /// <summary>
    /// Handles vision detection for seekers to find hiders
    /// </summary>
    public class VisionDetection : NetworkBehaviour
    {
        private PlayerStats playerStats;
        private Collider[] detectedPlayers = new Collider[Constants.MAX_PLAYERS];

        private void Start()
        {
            playerStats = GetComponent<PlayerStats>();
        }

        private void Update()
        {
            if (!isServer) return;
            if (playerStats.GetRole() != PlayerRole.Seeker) return;

            DetectHiders();
        }

        private void DetectHiders()
        {
            int count = Physics.OverlapSphereNonAlloc(
                transform.position,
                Constants.SEEKER_VISION_RANGE,
                detectedPlayers
            );

            for (int i = 0; i < count; i++)
            {
                if (detectedPlayers[i].CompareTag(Constants.PLAYER_TAG))
                {
                    PlayerStats targetStats = detectedPlayers[i].GetComponent<PlayerStats>();
                    
                    if (targetStats != null && targetStats.GetRole() == PlayerRole.Hider && !targetStats.IsFound())
                    {
                        if (IsInVision(detectedPlayers[i].transform))
                        {
                            targetStats.FoundBySeeker();
                        }
                    }
                }
            }
        }

        private bool IsInVision(Transform target)
        {
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, directionToTarget);
            
            return angle < Constants.SEEKER_VISION_ANGLE / 2f;
        }

        private void OnDrawGizmos()
        {
            if (playerStats != null && playerStats.GetRole() == PlayerRole.Seeker)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, Constants.SEEKER_VISION_RANGE);
            }
        }
    }
}