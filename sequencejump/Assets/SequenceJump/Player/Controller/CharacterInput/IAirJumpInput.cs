namespace SequenceJump.Player.Controller.CharacterInput
{
    public interface IAirJumpInput
    {
        bool AirJumpPerformed { get; }
        bool AirJumpCanceled { get; }
    }
}