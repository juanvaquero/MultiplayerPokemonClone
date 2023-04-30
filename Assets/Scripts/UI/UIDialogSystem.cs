using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIDialogSystem : MonoBehaviour
{
    [SerializeField]
    private float _letterPerSecond = 30f;

    [SerializeField]
    private GameObject _panel;
    [SerializeField]
    private TextMeshProUGUI _infoPanelText;

    public IEnumerator TypeDialog(string text)
    {
        SetEnableDialog(true);

        //Reset the dialog
        _infoPanelText.text = "";
        foreach (var letter in text.ToCharArray())
        {
            _infoPanelText.text += letter;
            yield return new WaitForSeconds(1f / _letterPerSecond);
        }
    }

    public void SetEnableDialog(bool enable)
    {
        _panel.SetActive(enable);
    }
}
