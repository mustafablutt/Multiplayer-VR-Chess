using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Photon.Pun;


public abstract class SelectPiece : MonoBehaviour
{

    // Use this for initialization
    // the renderer is changed to give the selection sensation
    public Renderer groundRend;
    // this it the scrpt that manages the piece movement
    MultiplayerPieceMovement piecemovScript;
    public GameObject selectGo;
    public GameObject col2;

    public List<GameObject> myArr = new List<GameObject>();
    public bool whiteblack;
    Dictionary<Vector3, GameObject> defeatedPositions = new Dictionary<Vector3, GameObject>();
    Dictionary<Vector3, GameObject> defeatedWhitePositions = new Dictionary<Vector3, GameObject>();



    protected virtual void Start()
    {
        piecemovScript = GameObject.FindGameObjectWithTag("pieceMovement").GetComponent<MultiplayerPieceMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // these are the actions performed by trigge calls that simulate the over effect for selection
    public void onPieceOver()
    {
        //Debug.Log("over	");	
        groundRend.transform.rotation = Quaternion.Euler(90, 0, 0);
        groundRend.enabled = true;
    }
    public void onPieceExit()
    {
        if (piecemovScript.selectedGo != gameObject)
        {
            groundRend.enabled = false;
        }


    }

    //this is the function that allows piece selection
    public void onPieceClick()
    {
        // set lastest piece render to false (unselect) only of there is one selected and is not the same as clicked
        if (piecemovScript.selectedGo != null && piecemovScript.selectedGo != gameObject)
        {
            piecemovScript.selectedGo.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        }
        piecemovScript.selectedGo = transform.gameObject;
        // set position as this one:
        piecemovScript.lastPosition = transform.position;
    }


    // this part of the code detects collision betweem pieces
   public void OnTriggerEnter(Collider col)
    {

        var numbers = new List<int> { 1001, 1002, 1003, 1004,
            1005, 1006, 1007, 1008,1009, 1010,1011, 1012, 1013,1014,1015,1016,
        1017,1018,1019,1020,1021,1022,1023,1024,1025,1026
        ,1027,1028,1031,1030,1029,1032,1033,1034};

     

        foreach (var number in numbers)
        {
            if (col.GetComponent<PhotonView>().ViewID == number)
            {
                Destroy(col.gameObject);
                Debug.Log("if ve foreach çalıştı");
            }
        }


        if (col.gameObject.tag != "Untagged")
        {
           
            Vector3 BlacktargetPosition = new Vector3(-1037.24f, 67.85f, -1366.07f);
            Vector3 WhitetargetPosition = new Vector3(-1049.01f, 67.85f, -1358.14f);

            Debug.Log("Collision Has occur");
            piecemovScript.thereIsCollision = true;
            myArr.Add(col.gameObject);
            if (col.gameObject.transform.GetComponent<ChessPiece>().team != piecemovScript.selectedGo.transform.GetComponent<ChessPiece>().team)
            {
                Debug.Log(col.gameObject.tag);
                if (col.gameObject.tag == "king" || col.gameObject.tag == "blackking")
                {
                    Debug.Log("GameOver");
                    PhotonNetwork.LoadLevel("GameOver");

                }
                else
                {
                    selectGo = piecemovScript.selectedGo;
                    piecemovScript.colObject = col.gameObject;

                    if(col.gameObject.GetComponent<ChessPiece>().team == 1)
                    {
                        while (defeatedPositions.ContainsKey(BlacktargetPosition))
                        {
                            // Choose a different position if the target position is already occupied
                            BlacktargetPosition += new Vector3(0f, 0f, 0.5f);
                        }

                        // Add the position to the dictionary
                        defeatedPositions[BlacktargetPosition] = col.gameObject;

                        // Move the defeated chess piece to the target position
                        col.gameObject.transform.localPosition = BlacktargetPosition;
                    }
                    else
                    {
                        while (defeatedWhitePositions.ContainsKey(WhitetargetPosition))
                        {
                            // Choose a different position if the target position is already occupied
                            WhitetargetPosition += new Vector3(0f, 0f, 0.5f);
                        }

                        // Add the position to the dictionary
                        defeatedWhitePositions[WhitetargetPosition] = col.gameObject;

                        // Move the defeated chess piece to the target position
                        col.gameObject.transform.localPosition = WhitetargetPosition;
                    }
                    

                }




            }
            else if (col.gameObject.transform.GetComponent<ChessPiece>().team == piecemovScript.selectedGo.transform.GetComponent<ChessPiece>().team) ;
            {
                selectGo = piecemovScript.selectedGo;
                piecemovScript.colObject = col.gameObject;

            }


        }


    }


    public abstract void onMultiplayerPieceOver();
    public abstract void onMultiplayerPieceExit();
    public abstract void onMultiplayerPieceClick();
    public abstract void onMultiplayerTriggerEnter(Collider col);




}
