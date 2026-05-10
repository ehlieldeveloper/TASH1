# TASH - Seek and Hide Game

A 3D multiplayer seek-and-hide game developed in Unity with Mirror networking. Set in the streets and environments of Riyadh, Saudi Arabia.

## Features

- **Multiplayer Networking**: Built with Mirror for seamless online gameplay
- **Role-Based Gameplay**: Players are assigned as Seekers or Hiders
- **3D Environment**: Riyadh-inspired city landscapes
- **Vision Detection System**: Seekers use line-of-sight to find hiders
- **Hiding Mechanics**: Hiders can hide in designated areas to avoid detection
- **Game State Management**: Lobby → Countdown → Hiding Prep → Seeking → Game Over
- **Real-time Scoring**: Track hiders found and hiding time

## Project Structure

```
Assets/
├── Scripts/
│   ├── Core/
│   │   ├── Constants.cs         # Game configuration
│   │   ├── GameState.cs         # Game state management
│   │   └── PlayerRole.cs        # Player role enums
│   ├── Player/
│   │   ├── PlayerController.cs  # Movement and input handling
│   │   ├── PlayerStats.cs       # Player data
│   │   ├── VisionDetection.cs   # Seeker vision system
│   │   └── HidingSystem.cs      # Hider mechanics
│   ├── Networking/
│   │   └── GameManager.cs       # Game logic and orchestration
│   └── UI/
│       └── GameHUD.cs           # HUD display
└── Prefabs/
    └── Player.prefab            # Player character prefab
```

## Installation

### Prerequisites
- Unity 2021.3 LTS or later
- Mirror networking library

### Setup Steps

1. **Install Mirror**
   - Window → Package Manager
   - Click `+` → Add package from git URL
   - Paste: `https://github.com/vis2k/Mirror.git?path=/Assets/Mirror`
   - Click Add and wait for import

2. **Create Player Prefab**
   - Right-click in Hierarchy → 3D Object → Cube (temporary model)
   - Name it "Player"
   - Add Components:
     - Character Controller
     - PlayerController
     - PlayerStats
     - VisionDetection
     - HidingSystem
     - NetworkIdentity
   - Create child object "CameraHolder" with Camera inside
   - Drag to Assets/Prefabs/ as Player.prefab
   - Delete from scene

3. **Setup NetworkManager**
   - Create empty GameObject named "NetworkManager"
   - Add Component → NetworkManager (Mirror)
   - Add Component → NetworkManagerHUD (Mirror)
   - Drag Player.prefab to Player Prefab slot

4. **Create GameManager**
   - Create empty GameObject named "GameManager"
   - Add Component → GameManager
   - Mark as "Don't Destroy on Load"

5. **Create HUD Canvas**
   - Right-click Hierarchy → UI → Panel - TextMeshPro
   - Create child TextMeshProUGUI elements:
     - Timer
     - Game State
     - Players
     - Hiders Found
   - Add GameHUD script to panel
   - Assign UI elements in inspector

## How to Play

### Game Flow
1. **Lobby**: Wait for minimum 2 players to connect
2. **Countdown** (10s): Players prepare
3. **Hiding Prep** (30s): Hiders find hiding spots, seekers wait
4. **Seeking** (5 min): Seekers find hiders
5. **Game Over**: Results displayed, new round begins

### Player Roles
- **Seeker**: Find all hiders using vision detection. Range: 50m, Vision Angle: 60°
- **Hider**: Avoid seekers. Hide near hiding spots (radius: 3m)

### Controls
- **WASD**: Move
- **Shift**: Sprint
- **Mouse**: Look around
- **ESC**: Pause (to implement)

## Gameplay Mechanics

### Vision Detection
- Seekers detect hiders within 50m radius
- Vision is cone-shaped (60° angle)
- Line-of-sight required
- Vision visualization with Gizmos in editor

### Hiding System
- Hiders automatically detected as "hiding" when in hiding spots
- Hiding spots have tag: "HidingSpot"
- Detection radius: 3m
- Real-time UI feedback

## Configuration

Edit `Assets/Scripts/Core/Constants.cs` to customize:
- Max players
- Game timing
- Player speeds
- Vision ranges
- And more...

## Networking Details

- **Architecture**: Server-Authoritative
- **Tick Rate**: 20 updates/second
- **SyncVars**: Player role, stats, hiding status
- **Commands**: Hide requests

## Troubleshooting

### Players not appearing
- Verify NetworkIdentity is on Player prefab
- Check NetworkManager has Player Prefab assigned
- Ensure NetworkIdentity is properly configured

### Movement not working
- Verify CharacterController is not in kinematic mode
- Check PlayerController script is enabled
- Ensure camera is child of player

### Vision detection not working
- Check Constants.cs SEEKER_VISION_RANGE and VISION_ANGLE
- Verify players have PlayerStats component
- Ensure Colliders are on player objects

## Future Enhancements

- [ ] Riyadh environment assets and models
- [ ] Sound effects and music
- [ ] Footstep detection system
- [ ] Power-ups and abilities
- [ ] Ranked matchmaking
- [ ] Spectator mode
- [ ] Replay system
- [ ] Cross-platform support

## Contributing

Contributions are welcome! Please:
1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## License

MIT License - feel free to use for personal or commercial projects

## Support

For issues, questions, or suggestions:
- Open an Issue on GitHub
- Check existing documentation
- Review Mirror networking docs: https://mirror-networking.com/

---

**Happy Gaming!** 🎮
