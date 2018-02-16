using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardButton : MonoBehaviour {

    BoatMovement bm;
    private void Awake()
    {
        bm = FindObjectOfType<BoatMovement>();
    }

    bool SwitchGate = false;
    private void OnTriggerEnter(Collider other)
    {
        ControlWheel c = other.GetComponent<ControlWheel>();

        if(c != null && bm != null)
        {
            if(SwitchGate)
            {
                bm.moving = true;
                SwitchGate = !SwitchGate;
            }
            else
            {
                bm.moving = false;
                SwitchGate = !SwitchGate;
            }
        }
    }
}
