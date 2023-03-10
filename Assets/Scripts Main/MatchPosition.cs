using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPosition : MonoBehaviour
{
    public GameObject TargetObject;
    public Vector3 Offset;

    void Update()
    {
        transform.position = TargetObject.transform.position + Offset;
    }
}
