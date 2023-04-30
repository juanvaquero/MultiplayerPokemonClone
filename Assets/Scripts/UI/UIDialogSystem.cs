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

    public void SetTextInfoPanel(string text, IEnumerator callback)
    {
        SetEnableCombatDialog(true);
        StartCoroutine(TypeDialog(text, callback));
    }

    public IEnumerator TypeDialog(string text, IEnumerator callback)
    {
        SetEnableCombatDialog(true);

        //Reset the dialog
        _infoPanelText.text = "";
        foreach (var letter in text.ToCharArray())
        {
            _infoPanelText.text += letter;
            yield return new WaitForSeconds(1f / _letterPerSecond);
        }
        if (callback != null)
            StartCoroutine(callback);
    }

    public void SetEnableCombatDialog(bool enable)
    {
        _panel.SetActive(enable);
    }
}
