using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class NetworkPlayerMover :MonoBehaviourPunCallbacks
{
    private PhotonView PV;
    DblClick dblClick;
    CardModel card;
    int cardIndex;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        dblClick = GetComponent<DblClick>();
        card = GetComponent<CardModel>();
        if (PV.IsMine)
        {
            GetComponent<BoxCollider2D>().enabled=true;


          


        }
    }

    private void Update()
    {
        if (dblClick.check)
       {

            cardIndex = card.cardIndex;
            PV.RPC("RPC_SendCardIndex", RpcTarget.All,cardIndex);
        
        }

    }

    [PunRPC]
    private void RPC_SendCardIndex(int cardIndex)
    {
        card.cardIndex = cardIndex;
        card.ToggleFace(true);

    }


}
