using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMoveSystem : GameSystem
{
    float CameraSpeed = 2f;
    float MinPlayerDelta = 10f;

    float PrevPlayerY = 10f;

    public override void Update()
    {
        float highestPlayer = float.MinValue;

        GameManager.EntityManager.ForEach<PlayerEntity>((playerEntity) =>
        {
            var height = playerEntity.transform.position.y;
            if (height > highestPlayer)
            {
                highestPlayer = height;
            }
        });

        if (
            MinPlayerDelta > Mathf.Abs(highestPlayer)
            ||
            Level.State.Height - Level.State.CurrentHeight > Level.State.MinimumOffsetHeight
        )
        {
            Level.State.CurrentHeight += highestPlayer - PrevPlayerY;
            PrevPlayerY = highestPlayer;
            if (Level.State.Height < Level.State.CurrentHeight)
            {
                Level.State.Height = Level.State.CurrentHeight;
            }
            return;
        }

        Level.State.CurrentHeight += highestPlayer;

        if (Level.State.Height < Level.State.CurrentHeight)
        {
            Level.State.Height = Level.State.CurrentHeight;
        }

        PrevPlayerY = 0;

        float deltaHeight = 0;
        deltaHeight = Mathf.Lerp(deltaHeight, highestPlayer, CameraSpeed * Time.deltaTime);
        Level.State.CameraHeight += deltaHeight;

        GameManager.EntityManager.ForEach<WallEntity>((wallEntity) =>
        {
            wallEntity.transform.position -= new Vector3(0, deltaHeight);

            if (wallEntity.transform.position.y < -16f || wallEntity.transform.position.y > 16f)
            {
                wallEntity.transform.position = new Vector3(wallEntity.transform.position.x, 0);
            }
        });

        GameManager.EntityManager.ForEach<PlayerEntity>((playerEntity) =>
        {
            playerEntity.transform.position -= new Vector3(0, deltaHeight);
        });
    }

}
