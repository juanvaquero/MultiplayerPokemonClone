
public class ModifyOpponentDefense : Ability
{
    public ModifyOpponentDefense(PokemonDataAbility pokemonDataAbility) : base(pokemonDataAbility)
    {
    }

    public override void ExecuteAbility(Pokemon playerPokemon, Pokemon otherPokemon)
    {
        base.ExecuteAbility(playerPokemon, otherPokemon);

        otherPokemon.Defense += _amountStatAffected;
    }
}