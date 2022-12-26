using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;



public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    public static bool black = false;

    public InputField createInput;
    public InputField joinInput;

    private void Awake()
    {
        black = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void CreateRoom()
    {
        black = false;
        PhotonNetwork.CreateRoom(createInput.text);
    }



    public void JoinRoom()
    {   black = true;
        PhotonNetwork.JoinRoom(joinInput.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("VRChess");
    }
}