using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.IO;

public class Photonroom : MonoBehaviourPunCallbacks,IInRoomCallbacks
{
    public static Photonroom room;
    private PhotonView PV;

    public int currentScene;
    public int multiPlayerScene;


    private void Awake()
    {
        if (room = null)
        {
            room = this;
        }
        else
        {
            if (room != this)
            {
                Destroy(room.gameObject);
                room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        PV = GetComponent<PhotonView>();

    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFineishedLoading;

    }
    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFineishedLoading;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Now we are in room");
        StartGame();
    }
    void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.LoadLevel(multiPlayerScene);
    }

    void OnSceneFineishedLoading(Scene scene,LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if (currentScene == multiPlayerScene)
        {
            CreatePlayer();
        }
        

    }

    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetWorkPlayer"), transform.position, Quaternion.identity);
    }

}
