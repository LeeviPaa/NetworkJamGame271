using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject VRTrackedPlayer;
    public GameObject PlayerPawn;
    public Transform spawnPosition;
    private GameObject Player;
    private VRMovementTracking VRTrackedScript;
    public VRMovementTracking VRTrackedScriptGet
    {
        get
        {
            return VRTrackedScript;
        }
    }

    public bool multiPlayerEnabled = true;
    private PhotonManager photonManager;

    private void Start()
    {
        VRTrackedScript = VRTrackedPlayer.GetComponent<VRMovementTracking>();

        if (VRTrackedScript != null && spawnPosition != null && PlayerPawn != null)
        {

            if (multiPlayerEnabled)
            {
                foreach (Transform child in transform)
                {
                    photonManager = child.GetComponent<PhotonManager>();
                    if (photonManager != null)
                    {
                        photonManager.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                Player = Instantiate(PlayerPawn, null);
                Player.transform.position = spawnPosition.position;
                Player.transform.rotation = spawnPosition.rotation;
                Player.GetComponent<PlayerPawn>().PossessPlayer(VRTrackedScript.GetVRMotionTrackingReferences());
            }
        }
        else
            Debug.LogError("Missing references!!");
    }
}
