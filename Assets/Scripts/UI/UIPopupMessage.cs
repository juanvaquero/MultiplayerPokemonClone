using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using Photon.Pun;
public class UIPopupMessage : MonoBehaviour
{
    [SerializeField]
    public GameObject _popupContent;

    [SerializeField]
    UIDialogSystem _uiDialog;

    [SerializeField]
    private Button _confirmButton;
    [SerializeField]
    private Button _refuseButton;

    private IEnumerator _confirmAction;
    private UnityAction _refuseAction;

    private PhotonView _photonView;

    // Start is called before the first frame update
    void Start()
    {
        _photonView = GetComponent<PhotonView>();

        // _confirmButton.onClick.AddListener(ConfirmPopup);
        _confirmButton.onClick.AddListener(ConfirmPopupPun);

        // _refuseButton.onClick.AddListener(RefusePopup);
        _refuseButton.onClick.AddListener(RefusePopupPun);

    }

    public void ShowPopup(string message, IEnumerator confirmAction, UnityAction refuseAction)
    {
        _popupContent.SetActive(true);
        StartCoroutine(_uiDialog.TypeDialog(message));

        _confirmAction = confirmAction;
        _refuseAction = refuseAction;
    }
    [PunRPC]
    public void ConfirmPopup()
    {
        StartCoroutine(ConfirmWithDelay());
    }

    public void ConfirmPopupPun()
    {
        _photonView.RPC("ConfirmPopup", RpcTarget.All);
    }

    private IEnumerator ConfirmWithDelay()
    {
        _popupContent.SetActive(false);

        if (_confirmAction != null)
            yield return _confirmAction;
    }

    [PunRPC]
    public void RefusePopup()
    {
        _popupContent.SetActive(false);
        _refuseAction.Invoke();
    }

    public void RefusePopupPun()
    {
        _photonView.RPC("RefusePopup", RpcTarget.All);
    }
    public bool IsDisplayed()
    {
        return _popupContent.activeSelf;
    }

}
