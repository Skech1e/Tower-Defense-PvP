using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Logger;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.SendRate = 40;
        PhotonNetwork.GameVersion = Application.version;
        PhotonNetwork.KeepAliveInBackground = 60;
        PhotonNetwork.MaxResendsBeforeDisconnect = 5;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        TypedLobby lobbyFilter = new("TowerDefensePvP", LobbyType.Default);
        PhotonNetwork.JoinLobby(lobbyFilter);
        L.LogNet("Online.");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        InvokeRepeating(nameof(Reconnect), 1, 5);
    }

    public void Reconnect()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable || PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        else
        {
            CancelInvoke(nameof(Reconnect));
        }
    }
}

