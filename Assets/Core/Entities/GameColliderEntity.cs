using UnityEngine;

public class GameColliderEntity : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        GameManagerSingleton.Instance.CollisionManager.Add(this, col);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        GameManagerSingleton.Instance.CollisionManager.Remove(this, col);
    }

    private void OnDestroy()
    {
        GameManagerSingleton.Instance.CollisionManager.Destroy(this);
    }
}
