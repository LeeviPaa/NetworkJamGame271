using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BoatWheel : MonoBehaviour {

    public Transform TheBoat;
    private BoatMovement boatMovement;

    private Transform grabPosition;
    private Transform stageRotate;
    public float stageRotationModifier = 1;
    //rotation velocity tracking
    private Vector3 prevPos;
    private Vector3 currPos;
    private Vector3 deltaPos;
    private Vector3 deltaA;
    private float deltaAngle = 1;
    public float deltaAngleTick = 5;

    public float followHandSpeed = 10;
    public float releaseVelocityMultiplier = 1.5f;
    public float maxRotspeed = 2;
    public float veloctyDecayModifier = 1;
    private float currRotationVelocity = 0;
    public float CurrRotationVelocity
    {
        get
        {
            return currRotationVelocity;
        }
    }

    private bool controlled = false;
    private ControlWheel currentController;

    private void Awake()
    {

        boatMovement = TheBoat.GetComponent<BoatMovement>();

        foreach(Transform child in transform)
        {
            if(child.name == "GRABPOSITION")
            {
                grabPosition = child;
            }
        }
        if(grabPosition == null)
        {
            Debug.LogError(name + " grab position not found!");
        }

    }


    private void FixedUpdate()
    {
        bool trigger = false;
        if (!controlled)
        {
            if (trigger)
            {
                trigger = !trigger;
            }
            //without hand control
            Rotate();
            VelocityDecay();
        }
        else
        {
            if(!trigger)
            {
                trigger = !trigger;

            }
            //With hand control
            ControlRotate();
        }

        //RotateStage();
    }

    Vector3 prevRotationForward;
    Vector3 currRotationForward;
    void RotateStage()
    {
        prevRotationForward = currRotationForward;
        currRotationForward = transform.forward;

        float rotation = Vector3.Angle(prevRotationForward, currRotationForward)*stageRotationModifier;
        float rightLeftAngle = Vector3.Angle(prevRotationForward, transform.right);

        if (rightLeftAngle > 90)
        {
            boatMovement.AddRotation(rotation);
            //stageRotate.transform.Rotate(0, rotation, 0);
        }
        else
        {
            boatMovement.AddRotation(rotation);
            //stageRotate.transform.Rotate(0, -rotation, 0);
        }
    }

    void Rotate()
    {
        transform.Rotate(0, currRotationVelocity, 0);
    }

    void ControlRotate()
    {
        float step = 5 * Time.deltaTime;
        
        Vector3 targetDir = currentController.transform.position - transform.position;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0);
        Debug.DrawRay(transform.position, newDir, Color.red);

        Vector3 deltaMinus = grabPosition.position - transform.position;
        deltaMinus.y = 0;
        Quaternion Q = Quaternion.FromToRotation(deltaMinus, transform.forward);
        transform.rotation = Quaternion.Lerp(
            transform.rotation, 
            Quaternion.LookRotation(new Vector3(targetDir.x, 0, targetDir.z))*Q, 
            followHandSpeed*Time.deltaTime);

        ControlRotateAngularSpeed();

        if (currentController != null)
        {
            //look at controller?

            
        }
    }

    void ControlRotateAngularSpeed()
    {
        prevPos = currPos;
        currPos = grabPosition.transform.position;

        deltaPos = currPos - prevPos;
        deltaA = prevPos - transform.position;

        float sineA = ((0.5f * deltaPos.magnitude) / deltaA.magnitude);

        //clamp because sine is between -1 and 1
        sineA = Mathf.Clamp(sineA, -1, 1);
        currRotationVelocity = Mathf.Asin(sineA);

        //returns radians for some reason
        currRotationVelocity = currRotationVelocity * (180.0f / Mathf.PI) * releaseVelocityMultiplier;

        //positive or negative
        float angleToRight = Vector3.Angle(grabPosition.right, deltaPos.normalized);
        if(angleToRight > 90)
        {
            currRotationVelocity *= -1;
        }

        deltaAngle += currRotationVelocity;

        if(deltaAngle >= deltaAngleTick ||deltaAngle <= -deltaAngleTick)
        {
            deltaAngle = 0;
            Debug.Log("tick");
        }

    }

    void VelocityDecay()
    {
        if(currRotationVelocity > 0)
        {
            float decay = veloctyDecayModifier * Time.deltaTime;
            decay = Mathf.Clamp(decay, 0, 1);
            currRotationVelocity -= decay;
        }
        if(currRotationVelocity < 0)
        {
            float decay = -veloctyDecayModifier * Time.deltaTime;
            decay = Mathf.Clamp(decay, -1, 0);
            currRotationVelocity -= decay;
        }
        if(Mathf.Abs( currRotationVelocity) < 0.05f )
        {
            currRotationVelocity = 0;
        }
    }
    void ApplyVelocity(float velocity)
    {
        currRotationVelocity = velocity;
    }
    public void TakeControl(Transform controller)
    {
        ControlWheel hand = controller.GetComponent<ControlWheel>();
        if (controller != null && hand != null)
        {
            grabPosition.position = controller.transform.position;
            //grabPosition.localPosition = new Vector3(grabPosition.localPosition.x, 0, grabPosition.localPosition.z);
            grabPosition.transform.rotation = Quaternion.LookRotation(grabPosition.position - transform.position);

            currentController = hand;
            controlled = true;
        }
    }
    public void ReleaseControl(bool left)
    {
        if(currentController != null && left == currentController.left)
            controlled = false;
    }
    public void OnTriggerEnter(Collider other)
    {

    }
    public void OnTriggerExit(Collider other)
    {

    }
}
