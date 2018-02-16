using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parenter : MonoBehaviour {

    private List<Transform> tList = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.tag == "Player")
        {
            Debug.Log("player");
            tList.Add(other.transform);
            other.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(tList.Contains(other.transform))
        {
            tList.Remove(other.transform);
            other.transform.parent = null;
        }
    }
}
