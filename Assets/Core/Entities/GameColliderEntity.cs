using UnityEngine;

public class GameColliderEntity : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        GameManagerSingleton.Instance.CollisionManager.Add(this, collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        GameManagerSingleton.Instance.CollisionManager.Remove(this, collider);
    }

    private void OnDestroy()
    {
        GameManagerSingleton.Instance.CollisionManager.Destroy(this);
    }
}
