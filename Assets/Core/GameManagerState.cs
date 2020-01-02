using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameManagerState
{
    public GameLevel Level { get; set; }
    public GameConfigManager ConfigManager { get; } = new GameConfigManager();
    public GameInputManager InputManager { get; } = new GameInputManager();
    public GameEntityManager EntityManager { get; } = new GameEntityManager();
    public GameCollisionManager CollisionManager { get; } = new GameCollisionManager();
}
