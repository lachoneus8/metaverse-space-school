using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySetting : MonoBehaviour
{
    public float GravityValue;

    void Start()
    {
        Physics.gravity = new Vector3(0, -GravityValue, 0);
    }
}
