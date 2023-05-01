using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    [SerializeField]
    UIPopupMessage _popupMessage;

    public void ShowConfirmPopup(string message, IEnumerator confirmAction, UnityAction refuseAction)
    {
        _popupMessage.ShowPopup(message, confirmAction, refuseAction);
    }

    public bool IsPopupDisplayed()
    {
        return _popupMessage.IsDisplayed();
    }

}
