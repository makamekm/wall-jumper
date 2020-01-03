using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicSystem : GameSystem
{
    public override void Update()
    {
        GameManager.EntityManager.ForEach<PhysicEntity>((physicEntity) => {
            physicEntity.Velocity += physicEntity.Force * Time.deltaTime;
            physicEntity.Force = new Vector3();
            physicEntity.transform.position += physicEntity.Velocity * Time.deltaTime;
        });
    }

}
