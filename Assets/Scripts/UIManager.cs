using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Menu"), Space(2)]
    public GameObject MenuButtons;
    public Button Play, Options, Exit, Cancel;

    [Header("Yes/No"), Space(2)]
    public GameObject Sure;
    public Button Yes, No;

    [Header("Create/Join"), Space(2)]
    public GameObject RoomBox;
    public GameObject CJPanel;
    public Button Create, Copy, Join, Go;

    [Header("Room Info"), Space(2)]
    public GameObject RoomInfo;
    public GameObject RoomCreated, RoomJoin;

    [Header("Lobby"), Space(2)]
    public GameObject LobbyObj;
    public Button StartMatch;
    public TextMeshProUGUI P1, P2;


    private void OnEnable()
    {
        StartMatch.onClick.AddListener(() => StartCoroutine(Lobby.Instance.Load()));
        //Lobby.Instance.LobbyFullEvent.AddListener(() => StartMatch.interactable = true);
    }
    private void OnDisable()
    {
        StartMatch.onClick.RemoveAllListeners();
        //Lobby.Instance.LobbyFullEvent.RemoveAllListeners();
    }

    public void ResetRoomUI()
    {
        CJPanel.SetActive(true);
        Cancel.gameObject.SetActive(true);

        RoomInfo.SetActive(false);
        RoomCreated.SetActive(false);
        RoomJoin.SetActive(false);
        LobbyObj.SetActive(false);
        Sure.SetActive(false);
    }

    public void Back()
    {
        if (RoomCreated.activeInHierarchy || LobbyObj.activeInHierarchy)
            Sure.SetActive(true);
        else if (RoomJoin.activeInHierarchy)
            ResetRoomUI();
        else
        {
            RoomBox.SetActive(false);
            MenuButtons.SetActive(true);
        }
    }

    
}
