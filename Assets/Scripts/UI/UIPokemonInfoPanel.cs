using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
public class UIPokemonInfoPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _pokemonNameText;
    [SerializeField]
    private TextMeshProUGUI _pokemonHealthText;
    [SerializeField]
    private Slider _pokemonHealthbar;

    private int _maxHealth;

    public void Initialize(string name, int maxHealth, int health, CombatUnit combatUnit)
    {
        _maxHealth = maxHealth;
        _pokemonNameText.text = name;
        _pokemonHealthbar.maxValue = maxHealth;
        UpdateHealthInfo(health);

        //Subscribe a event of update health in pokemon
        combatUnit.HealthChanged += UpdateHealthInfo;
    }

    public void UpdateHealthInfo(int health)
    {
        _pokemonHealthText.text = health + "/" + _maxHealth;
        _pokemonHealthbar.value = health;
    }

}