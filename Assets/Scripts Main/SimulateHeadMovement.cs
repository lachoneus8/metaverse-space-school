using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateHeadMovement : MonoBehaviour
{
    public float MinY;
    public float MaxY;
    public float MinX;
    public float MaxX;

    public float LookTime;
    public float MoveTime;

    public int NumLooksBeforeReturn;

    private float lookTimeLeft;
    private int numLooks;

    private float startX;
    private float startY;

    private Quaternion initialLookRot;
    private Quaternion startRot;
    private Quaternion targetRot;

    private float startMoveTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        var startRot = transform.rotation.eulerAngles;
        startX = startRot.x;
        startY = startRot.y;

        numLooks = 0;
        lookTimeLeft = LookTime;

        initialLookRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookTimeLeft > 0)
        {
            lookTimeLeft -= Time.deltaTime;

            if (lookTimeLeft < 0)
            {
                lookTimeLeft = 0;

                numLooks++;
                startRot = transform.rotation;
                startMoveTime = Time.time;

                if (numLooks > NumLooksBeforeReturn)
                {
                    numLooks = 0;
                    targetRot = initialLookRot;
                }
                else
                {
                    PickNewLookTarget();
                }
            }
        }
        else
        {
            var timeSinceMove = Time.time - startMoveTime;

            if (timeSinceMove < MoveTime)
            {
                var t = timeSinceMove / MoveTime;
                var curRot = Quaternion.Slerp(startRot, targetRot, t);
                transform.rotation = curRot;
            }
            else
            {
                lookTimeLeft = LookTime;
                transform.rotation = targetRot;
            }
        }
    }

    private void PickNewLookTarget()
    {
        var x = Random.Range(MinX, MaxX);
        var y = Random.Range(MinY, MaxY);

        targetRot = Quaternion.Euler(x, y, 0);
    }
}
