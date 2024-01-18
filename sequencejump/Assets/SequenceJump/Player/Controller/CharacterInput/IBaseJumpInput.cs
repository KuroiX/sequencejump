namespace SequenceJump.Player.Controller.CharacterInput
{
    public interface IBaseJumpInput
    {
        bool BaseJumpPerformed { get; }
        bool BaseJumpCanceled { get; }
    }
}