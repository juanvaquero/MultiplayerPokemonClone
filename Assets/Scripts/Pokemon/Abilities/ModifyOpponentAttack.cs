
public class ModifyOpponentAttack : Ability
{
    public ModifyOpponentAttack(PokemonDataAbility pokemonDataAbility) : base(pokemonDataAbility)
    {
    }

    public override bool Execute(Pokemon attacker, Pokemon otherPokemon)
    {
        otherPokemon.Attack += _amountStatAffected;

        return base.Execute(attacker, otherPokemon);
    }
}