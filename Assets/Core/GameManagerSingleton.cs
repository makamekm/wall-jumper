using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Threading.Tasks;

public class GameManagerSingleton : MonoBehaviour
{
    public static GameManagerState Instance { get; } = new GameManagerState();

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        foreach (GameSystem system in Instance.Level.Systems)
        {
            system.Start();
        }
    }

    void OnDestroy()
    {
        foreach (GameSystem system in Instance.Level.Systems)
        {
            system.OnDestroy();
        }
    }

    void Update()
    {
        Instance.InputManager.Update();
        foreach (GameSystem system in Instance.Level.Systems)
        {
            system.Update();
        }
    }

    void LateUpdate()
    {
        foreach (GameSystem system in Instance.Level.Systems)
        {
            system.LateUpdate();
        }
        Instance.InputManager.LateUpdate();
    }
}
