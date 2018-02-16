using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct VRMovementReference
{
    public Transform Body;
    public Transform Head;
    public Transform LeftHand;
    public Transform RightHand;
}
public class VRMovementTracking : MonoBehaviour {

    public VRMovementReference tList = new VRMovementReference();

	public VRMovementReference GetVRMotionTrackingReferences()
    {
        return tList;
    }
}
