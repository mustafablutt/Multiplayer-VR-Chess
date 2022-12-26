using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCamera : MonoBehaviour
{
    [SerializeField] GameObject Player;
    void Start()
    {
        CameraSetup();
    }

    public void CameraSetup()
    {
        if(CreateAndJoinRooms.black == true)
        {
            FlipCamera();
        }
    }
    public void FlipCamera()
    {
        Player.transform.position = new Vector3(-1042.7f, 76.8376f, -1355.7f);
        Player.transform.Rotate(Vector3.up, 180f, Space.World);
    }
}
