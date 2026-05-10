using UnityEngine;

namespace TASH.Core
{
    /// <summary>
    /// Game configuration constants
    /// </summary>
    public static class Constants
    {
        // Game Rules
        public const int MAX_PLAYERS = 8;
        public const int MIN_PLAYERS = 2;
        public const float COUNTDOWN_TIME = 10f;
        public const float SEEKING_TIME = 300f; // 5 minutes
        public const float HIDING_PREP_TIME = 30f; // Time for hiders to hide
        
        // Player Settings
        public const float PLAYER_SPEED = 5f;
        public const float PLAYER_SPRINT_SPEED = 8f;
        public const float PLAYER_ROTATION_SPEED = 2f;
        public const float PLAYER_HEIGHT = 1.8f;
        
        // Vision Detection
        public const float SEEKER_VISION_RANGE = 50f;
        public const float SEEKER_VISION_ANGLE = 60f;
        public const float HIDER_DETECTION_RADIUS = 2f;
        
        // Hiding System
        public const float HIDING_RADIUS = 3f;
        public const float HIDING_CHECK_INTERVAL = 0.5f;
        
        // Network
        public const int NETWORK_TICK_RATE = 20;
        
        // Tags
        public const string PLAYER_TAG = "Player";
        public const string HIDING_SPOT_TAG = "HidingSpot";
        public const string GROUND_TAG = "Ground";
    }
}