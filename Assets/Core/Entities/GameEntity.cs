using UnityEngine;
using System.Collections.Generic;

public class GameEntity : MonoBehaviour
{
    void Start()
    {
        GameManagerSingleton.Instance.EntityManager.Add(this.gameObject);
    }

    void OnDestroy()
    {
        GameManagerSingleton.Instance.EntityManager.Remove(this.gameObject);
    }
}
