using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class UINetworkMenu : MonoBehaviourPunCallbacks
{
    public const string GAME_SCENE = "Game";


    [SerializeField]
    private UIPanelNetwork _playerPanel;
    [SerializeField]
    private UIPanelNetwork _createRoomPanel;
    [SerializeField]
    private UIPanelNetwork _joinRoomPanel;

    private void Start()
    {
        _createRoomPanel.OkButton.onClick.AddListener(CreateRoom);
        _joinRoomPanel.OkButton.onClick.AddListener(JoinRoom);

        _playerPanel.InputField.text = "Player" + Random.Range(1, 20);
    }

    public void CreateRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = _playerPanel.InputField.text;
        PhotonNetwork.CreateRoom(_createRoomPanel.InputField.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = _playerPanel.InputField.text;
        PhotonNetwork.JoinRoom(_joinRoomPanel.InputField.text);
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene(GAME_SCENE);
    }

}
