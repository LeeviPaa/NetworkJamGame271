using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour {

    public bool moving = false;
    float maxVelocity = 10;

	void Update () {
	
        if(moving)
        {
            transform.Translate(transform.forward*maxVelocity*Time.deltaTime, Space.World);
        }
	}
    public void AddRotation(float rotation)
    {
        transform.Rotate(new Vector3(0, rotation, 0) * Time.deltaTime);
    }
}
