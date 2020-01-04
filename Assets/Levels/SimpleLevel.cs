using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SimpleLevel : GameLevel
{
    public override GameSystem[] Systems { get; } = {
        new GravitySystem(),
        new PlayerMovementSystem(),
        new PlayerWallSystem(),
        new PhysicSystem(),
        new WorldMoveSystem()
    };

    void Start()
    {
        GameManager.Level = this;
    }
}
