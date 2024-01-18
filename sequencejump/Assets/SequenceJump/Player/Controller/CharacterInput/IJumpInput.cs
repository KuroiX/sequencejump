namespace SequenceJump.Player.Controller.CharacterInput
{
    public interface IJumpInput
    {
        bool JumpPerformed { get; }
        bool JumpCanceled { get; }
    }
}