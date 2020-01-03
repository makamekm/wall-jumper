using System.Collections.Generic;
using UnityEngine;

public class GameCollisionManager
{
    private Dictionary<GameColliderEntity, Dictionary<string, List<Collider2D>>> Collisions = new Dictionary<GameColliderEntity, Dictionary<string, List<Collider2D>>>();

    public void Add(GameColliderEntity entity, Collider2D collider)
    {
        if (!Collisions.ContainsKey(entity))
        {
            Collisions.Add(entity, new Dictionary<string, List<Collider2D>>());
        }

        string tag = collider.tag == null ? "" : collider.tag;
        if (!Collisions[entity].ContainsKey(tag))
        {
            Collisions[entity].Add(tag, new List<Collider2D>());
        }

        Collisions[entity][tag].Add(collider);
    }

    public void Remove(GameColliderEntity entity, Collider2D collider)
    {
        string tag = collider.tag == null ? "" : collider.tag;

        if (Collisions.ContainsKey(entity) && Collisions[entity].ContainsKey(tag))
        {
            Collisions[entity][tag].Remove(collider);
        }
    }

    public void Destroy(GameColliderEntity entity)
    {
        if (Collisions.ContainsKey(entity))
        {
            Collisions.Remove(entity);
        }
    }

    public List<Collider2D> Get(GameColliderEntity entity, string tag = "")
    {
        if (Collisions.ContainsKey(entity) && Collisions[entity].ContainsKey(tag))
        {
            return Collisions[entity][tag];
        }
        return null;
    }

    public void Clear()
    {
        Collisions.Clear();
    }
}
