using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UIPopupMessage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _messageText;

    [SerializeField]
    private Button _confirmButton;
    [SerializeField]
    private Button _refuseButton;

    private UnityAction _confirmAction;

    // Start is called before the first frame update
    void Start()
    {

        _confirmButton.onClick.AddListener(ConfirmPopup);
        _refuseButton.onClick.AddListener(RefusePopup);
    }

    public void ShowPopup(string message, UnityAction confirmAction)
    {
        gameObject.SetActive(true);
        _messageText.text = message;

        _confirmAction = confirmAction;
    }

    public void ConfirmPopup()
    {
        if (_confirmAction != null)
            _confirmAction.Invoke();
        Debug.LogError("ConfirmPopup!");
    }

    public void RefusePopup()
    {
        gameObject.SetActive(false);
    }
}
