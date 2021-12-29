namespace Features.Player.Controller.CharacterInput
{
    public interface IJumpInput
    {
        bool JumpPerformed { get; set; }

        bool JumpBuffered { get; }
        bool JumpCanceled { get; set; }
    }
}