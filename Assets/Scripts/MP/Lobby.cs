using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using static Logger;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Lobby : MonoBehaviourPunCallbacks
{   
    public static Lobby Instance { get; private set; }

    public GameObject Loading;
    public Slider bar;

    const byte code_length = 6;
    const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    readonly TypedLobby lobbyFilter = new("TowerDefensePvP", LobbyType.Default);
    TextMeshProUGUI roomCode;
    TMP_InputField inputCode;

    bool isRoomFull => PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers;
    
    public string GenerateCode() => new(Enumerable.Repeat(characters, code_length).Select(s => s[Random.Range(0, s.Length)]).ToArray());
    public void CreateRoom()
    {
        var pvtRoom = new RoomOptions() { MaxPlayers = 2, IsOpen = true, IsVisible = false, PlayerTtl = 2000, EmptyRoomTtl = 1000 };
        roomCode.text = GenerateCode();
        PhotonNetwork.CreateRoom(roomCode.text, pvtRoom, lobbyFilter, null);
    }

    public void JoinRoom()
    {
        if(PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinRoom(roomCode.text);
            L.LogNet("joining");
        }
    }

    public void RandomRoom()
    {
        var openRoom = new RoomOptions() { MaxPlayers = 2, IsOpen = true, IsVisible = true, PlayerTtl = 1024, EmptyRoomTtl = 1024 };
        PhotonNetwork.JoinRandomOrCreateRoom(typedLobby: lobbyFilter, roomOptions: openRoom);
    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            L.LogNet("Left Room");
        }

    }

    public UnityEvent LobbyFullEvent;


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient && isRoomFull)
        {
            PhotonNetwork.CurrentRoom.IsOpen = PhotonNetwork.CurrentRoom.IsVisible = false;
            LobbyFullEvent.Invoke();
        }
    }

    public IEnumerator Load()
    {
        Loading.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;
        float loadtime = 0;
        bar.value = 0;
        while(!operation.isDone)
        {
            loadtime += Time.unscaledDeltaTime;
            bar.value = Mathf.Lerp(0f, 1f, loadtime);
            yield return null;
        }
    }
}
