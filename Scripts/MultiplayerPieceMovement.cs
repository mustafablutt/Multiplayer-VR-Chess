using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PhotonView))]
public class MultiplayerPieceMovement : PieceMovement
{
    private PhotonView photonView;
    public override void SelectedPieceMoved(GameObject groundPoint)
    {
        photonView.RPC(nameof(RPC_PieceMovement), RpcTarget.AllBuffered, null);
    }

    [PunRPC]
    public void RPC_PieceMovement(GameObject groudPoint)
    {
        movePiece(groudPoint);
    }

    protected override void Start()
    {
        base.Start();
        photonView = GetComponent<PhotonView>();
    }

    public override void MultiMovetoObjective(Vector3 position)
    {
        photonView.RPC(nameof(RPC_MovetoObjective), RpcTarget.AllBuffered, null);
    }
    [PunRPC]
    public void RPC_MovetoObjective(Vector3 position)
    {
        moveToObjective(position);
    }
}
