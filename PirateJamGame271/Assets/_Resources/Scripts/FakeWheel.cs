using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeWheel : MonoBehaviour {

    public float rotationAmount = 1;
    public GameObject wheelVisuals;
    private BoatMovement BM;

    private void Awake()
    {
        BM = FindObjectOfType<BoatMovement>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Hand" && BM != null    )
        {
            BM.AddRotation(rotationAmount * Time.deltaTime);
            wheelVisuals.transform.Rotate(0, rotationAmount * Time.deltaTime, 0);
        }
    }
}
