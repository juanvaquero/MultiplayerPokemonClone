using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class MovementButton : MonoBehaviour
{
    // private string _movementName;
    // private UnityAction _movementEvent;

    [SerializeField]
    private TextMeshProUGUI _buttonText;
    [SerializeField]
    private Button _button;

    //TODO Implement tooltip for show description

    public void Initialize(string name, string description, UnityAction moveAction)
    {
        _buttonText.text = name;
        _button.onClick.AddListener(moveAction);
    }

}
