
public class ModifyPlayerDefense : Ability
{
    public ModifyPlayerDefense(PokemonDataAbility pokemonDataAbility) : base(pokemonDataAbility)
    {
    }

    public override void ExecuteAbility(Pokemon playerPokemon, Pokemon otherPokemon)
    {
        base.ExecuteAbility(playerPokemon, otherPokemon);

        playerPokemon.Defense += _amountStatAffected;
    }
}