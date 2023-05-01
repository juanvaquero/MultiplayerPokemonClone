using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIPanelNetwork : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _label;

    public TextMeshProUGUI Label
    {
        get { return _label; }
    }

    [SerializeField]
    private TMP_InputField _inputField;

    public TMP_InputField InputField
    {
        get { return _inputField; }
    }

    [SerializeField]
    private Button _okButton;

    public Button OkButton
    {
        get { return _okButton; }
    }
}
