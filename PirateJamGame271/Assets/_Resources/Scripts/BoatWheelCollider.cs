using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatWheelCollider : MonoBehaviour {

    BoatWheel wheelParent;
    public BoatWheel Wheel
    {
        get
        {
            return wheelParent;
        }
    }

    private void Awake()
    {
        wheelParent = transform.parent.GetComponent<BoatWheel>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        wheelParent.OnTriggerEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        wheelParent.OnTriggerExit(other);
    }
}
