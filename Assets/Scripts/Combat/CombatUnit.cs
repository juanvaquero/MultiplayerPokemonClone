using UnityEngine;
using UnityEngine.Events;

public class CombatUnit : MonoBehaviour
{

    private Pokemon _pokemon;
    public Pokemon Pokemon
    {
        get { return _pokemon; }
    }

    [SerializeField]
    private SpriteRenderer _pokemonRenderer;

    public UnityAction<int> HealthChanged;

    public void LoadCombatUnit(Pokemon pokemon, bool isPlayerUnit)
    {
        _pokemon = pokemon;

        _pokemonRenderer.sprite = _pokemon.GetSprite();
        _pokemonRenderer.flipX = isPlayerUnit;
    }

}