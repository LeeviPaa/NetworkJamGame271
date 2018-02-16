using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : Photon.MonoBehaviour {

    public Text status;
    public GameObject localPlayer;
    public GameObject multiplayerPlayerPrefab;
    private GameManager GM;
    [SerializeField]
    private List<Transform> spawnList = new List<Transform>();

    private void Start()
    {
        GM = transform.parent.GetComponent<GameManager>();

        if (GM != null)
        {
            PhotonNetwork.ConnectUsingSettings("0.1");
            InvokeRepeating("UpdateConnectionStatus", 1, 1);
        }
        else
            Debug.LogError("Game manager not found!!");
    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log("joined to lobby");
        PhotonNetwork.JoinOrCreateRoom("VRtest1", null, null);
    }

    int index = 0;
    public virtual void OnJoinedRoom()
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            GameObject Player = PhotonNetwork.Instantiate(multiplayerPlayerPrefab.name, spawnList[index].position, spawnList[index].rotation, 0);
            Player.GetComponent<PlayerPawn>().PossessPlayer(GM.VRTrackedScriptGet.GetVRMotionTrackingReferences());
            index++;
        }
        else
        {
            GameObject Player = PhotonNetwork.Instantiate(multiplayerPlayerPrefab.name, spawnList[index].position, spawnList[index].rotation, 0);
            index++;
        }

    }

    private void UpdateConnectionStatus()
    {
        if(status != null)
            status.text = PhotonNetwork.connectionStateDetailed.ToString();
    }
}
