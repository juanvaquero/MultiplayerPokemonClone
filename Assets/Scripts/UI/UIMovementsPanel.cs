using UnityEngine;
using TMPro;

public class UIMovementsPanel : MonoBehaviour
{
    [SerializeField]
    private MovementButton[] _movementButtons;

    public void Initialize(Movement[] movements, Ability[] abilities)
    {
        CombatManager combatManager = GameManager.Instance.CombatManager;
        if (movements == null || abilities == null)
            return;

        int i;
        for (i = 0; i < movements.Length; i++)
        {
            // movements[i].ExecuteMovement()
            _movementButtons[i].Initialize(movements[i].Name, "", null);
        }

        for (int j = i; j < abilities.Length; j++)
        {
            // abilities[i].ExecuteAbility()

            // Here and int the movements buttons is necessary create a auxiliary method in CombatController for get access to Pokemon instances.
            _movementButtons[j].Initialize(abilities[j].Name, abilities[j].Description, null);
        }
    }

}
