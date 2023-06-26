namespace Features.Player.Controller.CharacterInput
{
    public interface IAirJumpInput
    {
        bool AirJumpPerformed { get; }
        bool AirJumpCanceled { get; }
    }
}