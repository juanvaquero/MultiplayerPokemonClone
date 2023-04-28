public class ModifyPlayerAttack : Ability
{
    public ModifyPlayerAttack(PokemonDataAbility pokemonDataAbility) : base(pokemonDataAbility)
    {
    }

    public override void ExecuteAbility(Pokemon playerPokemon, Pokemon otherPokemon)
    {
        base.ExecuteAbility(playerPokemon, otherPokemon);

        playerPokemon.Attack += _amountStatAffected;
    }
}