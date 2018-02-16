using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWheel : MonoBehaviour {

    public bool left;

    private void OnTriggerEnter(Collider other)
    {
        BoatWheelCollider b = other.GetComponent<BoatWheelCollider>();
        if(b != null)
        {
            Debug.Log("collision");
            b.Wheel.TakeControl(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        BoatWheelCollider b = other.GetComponent<BoatWheelCollider>();
        if (b != null)
        {
            Debug.Log("exit");
            b.Wheel.ReleaseControl(left);
        }
    }
}
