using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSystem : GameSystem
{
    public override void Update()
    {
        if (!Input.GetKeyDown("space"))
        {
            return;
        }

        GameManager.EntityManager.ForEach<PlayerEntity, PhysicEntity>((playerEntity, physicEntity) => {
            if (playerEntity.RightWall != null)
            {
                physicEntity.Velocity = new Vector3(-40f, 50f);
            }
            else if (playerEntity.LeftWall != null)
            {
                physicEntity.Velocity = new Vector3(40f, 50f);
            }
        });
    }

}
