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

        int i;
        for (i = 0; i < movements.Length; i++)
        {
            _movementButtons[i].Initialize(movements[i], combatManager.MovementExecuted);
        }

        for (int j = i; j < abilities.Length; j++)
        {
            _movementButtons[j].Initialize(abilities[j], combatManager.AbilityExecuted);
        }
    }

}
