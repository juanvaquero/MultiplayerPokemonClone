using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIPokemonInfoPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _pokemonNameText;
    [SerializeField]
    private TextMeshProUGUI _pokemonHealthText;
    [SerializeField]
    private Slider _pokemonHealthbar;

    private int _maxHealth;

    public void Initialize(string name, int maxHealth, int health)
    {
        _maxHealth = maxHealth;
        _pokemonNameText.text = name;
        _pokemonHealthbar.maxValue = maxHealth;
        UpdateHealthInfo(health);
        //TODO suscribe a event of update health in pokemon
    }

    public void UpdateHealthInfo(int health)
    {
        _pokemonHealthText.text = health + "/" + _maxHealth;
        _pokemonHealthbar.value = health;//TODO review how to asing better the value of slider.
    }

}