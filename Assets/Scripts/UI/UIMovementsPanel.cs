using UnityEngine;
using TMPro;

public class UIMovementsPanel : MonoBehaviour
{
    [SerializeField]
    private MovementButton[] _movementButtons;

    public void Initialize(Movement[] movements, Ability[] abilities, CombatManager combatManager)
    {
        if (movements == null || abilities == null)
            return;

        _movementButtons[0].Initialize(movements[0], combatManager.MovementExecuted);
        _movementButtons[1].Initialize(abilities[0], combatManager.AbilityExecuted);
    }

}
