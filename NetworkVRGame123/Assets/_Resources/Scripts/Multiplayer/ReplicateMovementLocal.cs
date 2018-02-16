using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplicateMovementLocal : MonoBehaviour {

    public Transform replicateThis;
    public bool replicateRotation = false;

    private void Update()
    {
        if(replicateThis != null)
        {
            replicateThis.position = transform.position;
            if (replicateRotation)
                replicateThis.rotation = transform.rotation;
        }
    }
}
