using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSystem : GameSystem
{
    public override void Update()
    {
        GameManager.EntityManager.ForEach<PlayerEntity, PhysicEntity>((playerEntity, physicEntity) => {
            var colliders = GameManager.CollisionManager.Get(playerEntity.RightCollider, "RightWall");
            if (colliders != null && colliders.Count > 0)
            {
                if (playerEntity.RightWall == null)
                {
                    var wall = colliders[0].gameObject;
                    playerEntity.RightWall = wall;
                    physicEntity.Velocity = new Vector3(0, 50f);
                }
            }
            else
            {
                playerEntity.RightWall = null;
            }
        });

        GameManager.EntityManager.ForEach<PlayerEntity, PhysicEntity>((playerEntity, physicEntity) => {

            var colliders = GameManager.CollisionManager.Get(playerEntity.LeftCollider, "LeftWall");
            if (colliders != null && colliders.Count > 0)
            {
                if (playerEntity.LeftWall == null)
                {
                    var wall = colliders[0].gameObject;
                    playerEntity.LeftWall = wall;
                    physicEntity.Velocity = new Vector3(0, 50f);
                }
            }
            else
            {
                playerEntity.LeftWall = null;
            }
        });
    }

}
