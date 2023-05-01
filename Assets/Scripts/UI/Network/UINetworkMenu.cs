using System.Collections;
using UnityEngine;
using Photon.Pun;

public class UINetworkMenu : MonoBehaviourPunCallbacks
{
    public const string GAME_SCENE = "Game";

    [SerializeField]
    private UIPanelNetwork _createRoomPanel;
    [SerializeField]
    private UIPanelNetwork _joinRoomPanel;

    private void Start()
    {
        _createRoomPanel.OkButton.onClick.AddListener(CreateRoom);
        _joinRoomPanel.OkButton.onClick.AddListener(JoinRoom);
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(_createRoomPanel.InputField.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(_joinRoomPanel.InputField.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(GAME_SCENE);
    }
}
