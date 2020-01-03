using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySystem : GameSystem
{
    Vector3 Gravity = new Vector2(0, -98f);

    public override void Update()
    {
        GameManager.EntityManager.ForEach<PhysicEntity>((physicEntity) => {
            physicEntity.Force += Gravity;
        });
    }

}
