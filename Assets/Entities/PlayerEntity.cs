using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    public float Offset = 5f;
    public GameObject LeftWall { set; get; }
    public GameObject RightWall { set; get; }

    public GameColliderEntity RightCollider { set; get; }
    public GameColliderEntity LeftCollider { set; get; }

    private void Awake()
    {
        RightCollider = transform.Find("RightCollider").GetComponent<GameColliderEntity>();
        LeftCollider = transform.Find("LeftCollider").GetComponent<GameColliderEntity>();
    }
}
