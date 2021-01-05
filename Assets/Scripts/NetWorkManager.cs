using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
public class NetWorkManager : MonoBehaviourPunCallbacks
{
   
    [SerializeField] Transform[] spawnPoint;
    int PlayerPoint = 0;
    int EnemyPoint = 1;

    [SerializeField] float cardOffset;

    public static NetWorkManager netWorkManager;


    GameObject player;

    RoomInfo rooms;
    PhotonView PV;



    Deck deck;


   
    private void Awake()
    {
        netWorkManager = this;
    }
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        deck = GetComponent<Deck>();



       PhotonNetwork.ConnectUsingSettings();//connect to photon Master Server

    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Player Connected to Master");
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void OnGUI()
    {
     if(GUI.Button(new Rect(10, 40, 100, 28), "Connect!"))
        {
            PhotonNetwork.JoinRandomRoom();
        }

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed there are no open room");
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Trying to new room");
        int RandomRoomName = Random.Range(0, 100000);
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom("room" + RandomRoomName, roomOptions);

    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Now we are in room");
        Debug.Log(PhotonNetwork.PlayerList.Length);
        StartGame();
    }
    void StartGame()
    {
      //  if (!PhotonNetwork.IsMasterClient)
        //    return;

        CreatePlayer();
    }
    private void CreatePlayer()
    {
        if (PhotonNetwork.PlayerList.Length == 1)
        {
            int cardCount = 0;
            deck.Shuffle();

            foreach(int i in deck.Getcards())
            {
                if (cardCount < 13)
                {
                    float co = cardOffset * cardCount;
                    Vector3 temp = spawnPoint[PlayerPoint].transform.position + new Vector3(co, 0f);
                    GameObject cardCopy = (GameObject)PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Card"), temp, Quaternion.identity);


                    CardModel cardModel = cardCopy.GetComponent<CardModel>();
                    cardModel.cardIndex = i;
                    cardModel.ToggleFace(true);

                    cardCount++;
                 }
                else if (cardCount < 26)
                {
                    var properties = new ExitGames.Client.Photon.Hashtable();
                    string card = "card" + cardCount;
                    properties.Add(card, i);
                    PhotonNetwork.CurrentRoom.SetCustomProperties(properties);

                    cardCount++;
                }
                else
                {
                    break;
                }


            }
            
        }
        else
        {
            for(int i = 13; i < 26; i++)
            {
                float co = cardOffset * (i-13);
                Vector3 temp = spawnPoint[EnemyPoint].transform.position + new Vector3(co, 0f);

                GameObject cardCopy =(GameObject) PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Card"), temp, Quaternion.identity);
                CardModel cardModel = cardCopy.GetComponent<CardModel>();
                string card = "card" + i;
                int indexnumber = (int)PhotonNetwork.CurrentRoom.CustomProperties[card];

                cardModel.cardIndex = indexnumber;
                cardModel.ToggleFace(true);

            }

            
        }


    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room");
        CreateRoom();
    }




    private void Update()
    {
        
    }



}
