using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMoveSystem : GameSystem
{
    float CameraSpeed = 10f;
    float MinPlayerDelta = 10f;
    float MinPlayerSpeedDelta = 2f;
    float MaxPlayerSpeedDelta = 10f;
    float PlayerPredictSpeedDelta = 1f;
    float CameraPredictSpeedOffset = 20f;

    float PrevPlayerY = 10f;

    public override void Update()
    {
        float highestPlayerOffsetFromCenter = float.MinValue;
        float highestPlayerSpeed = 0;

        GameManager.EntityManager.ForEach<PlayerEntity, PhysicEntity>((playerEntity, physicEntity) =>
        {
            var height = playerEntity.transform.position.y;
            if (height > highestPlayerOffsetFromCenter)
            {
                highestPlayerOffsetFromCenter = height;
                highestPlayerSpeed = physicEntity.Velocity.y;
            }
        });

        float absHighestPlayerSpeed = Mathf.Abs(highestPlayerSpeed);

        //if (
        //    MinPlayerDelta > Mathf.Abs(highestPlayerOffsetFromCenter)
        //    ||
        //    Level.State.Height - Level.State.CurrentHeight > Level.State.MinimumOffsetHeight
        //)
        //{
        //    Level.State.CurrentHeight += highestPlayerOffsetFromCenter - PrevPlayerY;
        //    PrevPlayerY = highestPlayerOffsetFromCenter;
        //    if (Level.State.Height < Level.State.CurrentHeight)
        //    {
        //        Level.State.Height = Level.State.CurrentHeight;
        //    }
        //    return;
        //}

        Level.State.CurrentHeight += highestPlayerOffsetFromCenter;

        if (Level.State.Height < Level.State.CurrentHeight)
        {
            Level.State.Height = Level.State.CurrentHeight;
        }

        float cameraPredictOffset = 0f;

        if (absHighestPlayerSpeed > MinPlayerSpeedDelta && highestPlayerSpeed > 0)
        {
            cameraPredictOffset = CameraPredictSpeedOffset * Mathf.Min(1f, (absHighestPlayerSpeed - MinPlayerSpeedDelta) / MaxPlayerSpeedDelta) * highestPlayerSpeed / absHighestPlayerSpeed;
        }

        highestPlayerOffsetFromCenter += cameraPredictOffset;

        //PrevPlayerY = 0;

        //absHighestPlayerSpeed = 1f;

        float deltaCameraHeight = 0;
        float delta = Mathf.Abs(highestPlayerOffsetFromCenter) / CameraSpeed * Time.deltaTime;
        deltaCameraHeight = Mathf.Lerp(deltaCameraHeight, highestPlayerOffsetFromCenter, delta);
        Level.State.CameraHeight += deltaCameraHeight;

        GameManager.EntityManager.ForEach<WallEntity>((wallEntity) =>
        {
            wallEntity.transform.position -= new Vector3(0, deltaCameraHeight);

            if (wallEntity.transform.position.y < -16f || wallEntity.transform.position.y > 16f)
            {
                wallEntity.transform.position = new Vector3(wallEntity.transform.position.x, 0);
            }
        });

        GameManager.EntityManager.ForEach<PlayerEntity>((playerEntity) =>
        {
            playerEntity.transform.position -= new Vector3(0, deltaCameraHeight);
        });
    }

}
