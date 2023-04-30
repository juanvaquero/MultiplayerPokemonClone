using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class MovementButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _buttonText;
    [SerializeField]
    private Button _button;

    private CombatAction _combatAction;
    private UnityAction<CombatAction> _eventAction;

    public void Initialize(CombatAction combatAction, UnityAction<CombatAction> action)
    {
        _combatAction = combatAction;
        _eventAction = action;

        _buttonText.text = combatAction.Name;
        _button.onClick.RemoveAllListeners();// Reset button listeners
        _button.onClick.AddListener(MoveAction);
    }

    private void MoveAction()
    {
        _eventAction.Invoke(_combatAction);
    }

}
