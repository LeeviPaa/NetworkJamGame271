using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawn : MonoBehaviour {

    private VRMovementReference VRReference;

    //local transforms
    private Transform localHead;
    private Transform localLeftHand;
    private Transform localRightHand;

    private bool possessed = false;

    private void Awake()
    {
        foreach(Transform child in transform)
        {
            if(child.name == "HEAD")
            {
                localHead = child;
            }
            if(child.name == "RIGHTHAND")
            {
                localRightHand = child;
            }
            if(child.name == "LEFTHAND")
            {
                localLeftHand = child;
            }
        }
    }

    private void Update()
    {
        if(possessed && localLeftHand != null && localRightHand != null && localHead != null)
        {
            UpdateVRTrackingPositions();
        }
    }
    private void UpdateVRTrackingPositions()
    {
        if (VRReference.Body != null)
        {
            transform.position = VRReference.Body.transform.position;
            transform.rotation = VRReference.Body.transform.rotation;
        }

        if (VRReference.RightHand != null)
        {
            localRightHand.position = VRReference.RightHand.position;
            localRightHand.rotation = VRReference.RightHand.rotation;
        }
        if (VRReference.LeftHand != null)
        {
            localLeftHand.position = VRReference.LeftHand.position;
            localLeftHand.rotation = VRReference.LeftHand.rotation;
        }
        if (VRReference.Head != null)
        {
            localHead.position = VRReference.Head.position;
            localHead.rotation = VRReference.Head.rotation;
        }
    }

    public void PossessPlayer(VRMovementReference r)
    {
        VRReference = r;
        possessed = true;
        r.Body.position = transform.position;
    }
}
