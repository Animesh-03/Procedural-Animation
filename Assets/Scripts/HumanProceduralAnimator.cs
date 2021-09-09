using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanProceduralAnimator : MonoBehaviour
{
    public HumanProceduralAnimator otherFoot;
    public Transform raycastOrigin;
    private Vector3 lastLegTargetPosition;
    private Vector3 DefaultLocalLegPosition;
    private Vector3 targetPosition;
    public LayerMask ground;
    public float raycastRange;
    public float stepDistance;
    private float distanceMoved;
    private float lerp;
    public float delayFactor;
    public float footHeightMultiplier;
    public bool canMove;
    public bool isLeftLeg;
    void Start()
    {
        lastLegTargetPosition = transform.position;
        DefaultLocalLegPosition = transform.localPosition;
        canMove = true;
        lerp = 1;
    }

    void Update()
    {
        transform.position = lastLegTargetPosition;
        if(Physics.Raycast(raycastOrigin.position,-Vector3.up,out RaycastHit hit,raycastRange,ground))
        {
            distanceMoved = Vector3.Distance(lastLegTargetPosition,hit.point);

            if(distanceMoved>stepDistance && lerp == 1 && !otherFoot.canMove && canMove)
            {
                targetPosition = (hit.point + DefaultLocalLegPosition);
                targetPosition.y = hit.point.y - 0.1f;
                lerp = 0;
                
            }
        }

        if(lerp<1)
        {
            lastLegTargetPosition = Vector3.Lerp(transform.position,targetPosition,lerp/delayFactor);
            lastLegTargetPosition.y += Mathf.Sin(lerp*Mathf.PI)*footHeightMultiplier;
            lerp += Time.deltaTime/delayFactor;
            transform.up = hit.normal;

            if(isLeftLeg)
            {
                transform.eulerAngles = new Vector3(0,180,0);
            }

            if(lerp > 1)
            {
                lerp = 1;
            }
        }
        else
        {
            canMove = false;
            otherFoot.canMove = true;
        }

    }

}
