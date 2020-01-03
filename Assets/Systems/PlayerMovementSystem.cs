using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSystem : GameSystem
{
    public override void Update()
    {
        GameManager.EntityManager.ForEach<PlayerEntity>((playerEntity) => {
            var colliders = GameManager.CollisionManager.Get(playerEntity.RightCollider, "RightWall");
            if (colliders != null && colliders.Count > 0)
            {
                Debug.Log(colliders[0].name);
            }
        });

        if (!Input.GetKeyDown("space"))
        {
            return;
        }

        GameManager.EntityManager.ForEach<PlayerEntity, PhysicEntity>((playerEntity, physicEntity) => {
            physicEntity.Velocity = new Vector3(20f, 50f);
        });
    }

}
