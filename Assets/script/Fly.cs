using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fly : MonoBehaviour
{
    public InputActionReference flyAction;
    public Transform leftHand;
    public Transform head;
    public float flyingSpeed;
    public float maxSpeed;


    // Update is called once per frame
    void Update()
    {
        if (flyAction.action.triggered)
        {
            Thrust();
        }
    }

    private void Thrust()
    {
        Vector3 flyDir = leftHand.position - head.position;
        RaycastHit hit;
        if(Physics.Raycast(head.position, flyDir, out hit))
        {
            if (hit.distance < 1.5f)
            {
                return;
            }
        }
        GetComponent<Rigidbody>().AddForce(flyDir.normalized * flyingSpeed, ForceMode.Force);
    }
}
