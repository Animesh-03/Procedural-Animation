using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    public float forwardForce;
    private Rigidbody rb;
    private Vector3 velocity;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") != 0)
        {
            rb.velocity = new Vector3(0,0,forwardForce);
        }
    }
}
