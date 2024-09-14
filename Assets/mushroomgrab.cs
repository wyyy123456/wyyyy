using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class mushroomgrab : MonoBehaviour
{
    public InputActionProperty leftHandTrigger;
    public InputActionProperty rightHandTrigger;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            if (leftHandTrigger.action.triggered || rightHandTrigger.action.triggered)
            {
                Destroy(this.gameObject);
                //add mushroom to your bag
            }
        }
    }
}
