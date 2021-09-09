using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootTarget : MonoBehaviour
{

    public Transform[] legTargets;
    public float rayCastDistance;
    private int nLegs;
    private Vector3[] oldLegPosition;
    private Vector3[] relativeLegPosition;
    private Vector3[] oldHitPoint;
    private Vector3 lastHitPoint;
    private Vector3 velocity;
    public float stepLimit;
    public float velocityMultiplier;
    public float gizmoRadius;

    private RaycastHit hit;
    
    void Start()
    {
        nLegs = legTargets.Length;
        oldLegPosition = new Vector3[nLegs];
        relativeLegPosition = new Vector3[nLegs];
        oldHitPoint = new Vector3[nLegs];
        Ray ray = new Ray(transform.position,Vector3.up);
        Physics.Raycast(ray,out hit);
        for(int i=0; i<nLegs;i++)
        {
            oldLegPosition[i] = legTargets[i].position;
            relativeLegPosition[i] = hit.point-legTargets[i].position;
            oldHitPoint[i] = hit.point;
        }

    }
    void FixedUpdate()
    {
        velocity = (hit.point - lastHitPoint);

        for(int i=0; i<nLegs;i++)
        {
            legTargets[i].position = oldLegPosition[i];
        }

        Ray ray = new Ray(transform.position,Vector3.up);
        Physics.Raycast(ray,out hit,rayCastDistance);
        Debug.Log((hit.point - legTargets[0].position).magnitude);

        if(hit.collider != null)
        {
            for(int i=0; i<nLegs;i++)
            {
                if((hit.point - legTargets[i].position).magnitude > stepLimit)
                {
                    legTargets[i].position += (hit.point - oldHitPoint[i])*(1f+velocity.magnitude*velocityMultiplier);
                    oldLegPosition[i] = legTargets[i].position;
                    oldHitPoint[i] = hit.point;
                }
            }
        }

        lastHitPoint = hit.point;
    }
        

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(hit.point + new Vector3(0,-0.1f,0),gizmoRadius);
        }
}
