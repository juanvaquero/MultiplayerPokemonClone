public class Movement
{
    public string Name { get; }
    public int Power { get; }
    public int Accuracy { get; }

    public Movement(PokemonDataMovement pokemonDataMovement)
    {
        Name = pokemonDataMovement.Name;
        Power = pokemonDataMovement.Power;
        Accuracy = pokemonDataMovement.Accuracy;
    }


}