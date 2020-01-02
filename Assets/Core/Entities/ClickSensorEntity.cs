using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class ClickSensorEntity : MonoBehaviour
{
    public Collider Collider;

    void Start()
    {
        if (Collider == null)
        {
            Collider = transform.Find("ClickSensor").GetComponent<Collider>();
        }
    }
}
