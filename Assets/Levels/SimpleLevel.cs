using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SimpleLevel : GameLevel
{
    public override GameSystem[] Systems { get; } = {
        // new System()
    };

    void Start()
    {
        GameManager.Level = this;
    }
}
