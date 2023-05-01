using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

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

    // Start is called before the first frame update
    void Start()
    {
        _confirmButton.onClick.AddListener(ConfirmPopup);
        _refuseButton.onClick.AddListener(RefusePopup);
    }

    public void ShowPopup(string message, IEnumerator confirmAction, UnityAction refuseAction)
    {
        _popupContent.SetActive(true);
        StartCoroutine(_uiDialog.TypeDialog(message));

        _confirmAction = confirmAction;
        _refuseAction = refuseAction;
    }

    public void ConfirmPopup()
    {
        StartCoroutine(ConfirmWithDelay());
    }

    private IEnumerator ConfirmWithDelay()
    {
        _popupContent.SetActive(false);

        if (_confirmAction != null)
            yield return _confirmAction;
    }

    public void RefusePopup()
    {
        _popupContent.SetActive(false);
        _refuseAction.Invoke();
    }

    public bool IsDisplayed()
    {
        return _popupContent.activeSelf;
    }
}
