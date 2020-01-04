using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;
using System.Threading.Tasks;

public class GameLevel : MonoBehaviour
{
    private new Camera camera;
    public Camera Camera {
        get {
            if (camera == null) {
                camera = Camera.main;
            }
            return camera;
        }
        set {
            camera = value;
        }
    }
    public virtual GameSystem[] Systems { get; }
    public GameManagerState GameManager { get; protected set; }
    protected IEnumerable<IDisposable> Subscriptions { get; set; } = null;
    public GameLevelState State { get; } = new GameLevelState();

    public GameObject Add(GameObject obj, Vector3 pos)
    {
        var instance = Instantiate(obj, transform);
        instance.transform.position = pos;
        return instance;
    }

    public GameObject Add(GameObject obj, float x, float y, float z)
    {
        var instance = Instantiate(obj, transform);
        instance.transform.position = new Vector3(x, 0, z);
        return instance;
    }

    public void Remove(GameObject obj, float delay = 0f)
    {
        Destroy(obj, delay);
    }

    void Awake()
    {
        GameManager = GameManagerSingleton.Instance;

        LoadLevel();

        foreach (var system in Systems)
        {
            system.Level = this;
            system.GameManager = GameManager;
            system.Init();
        }

        Subscriptions = Systems
            .Where(system => system.Subscriptions != null)
            .SelectMany(system => system.Subscriptions);
    }

    void OnDestroy()
    {
        if (Subscriptions != null) {
            foreach (var subscription in Subscriptions)
            {
                subscription.Dispose();
            }
        }
    }

    public virtual void LoadLevel()
    {
        //foreach (var item in Actions)
        //{
        //    if (item.Value.ButtonPrefab == null)
        //    {
        //        item.Value.ButtonPrefab = Resources.Load<GameObject>(item.Value.ButtonPrefabPath);
        //    }
        //}
        Debug.Log("Level loaded!");
    }
}
