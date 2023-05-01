using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    [SerializeField]
    UIPopupMessage _popupMessage;

    public void ShowConfirmPopup(string message, IEnumerator confirmAction)
    {
        _popupMessage.ShowPopup(message, confirmAction);
    }

    public bool IsPopupDisplayed()
    {
        return _popupMessage.IsDisplayed();
    }

}
