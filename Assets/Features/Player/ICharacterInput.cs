namespace Features.Player
{
    public interface ICharacterInput
    {
        float Horizontal { get; }
        bool Jump { get; }
        bool Dash { get; }
    }
}