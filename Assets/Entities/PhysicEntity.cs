using UnityEngine;

public class PhysicEntity : MonoBehaviour
{
    public Vector3 Velocity { set; get; } = new Vector3();
    public Vector3 Force { set; get; } = new Vector3();
}
