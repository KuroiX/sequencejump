namespace Features.Player.Controller.CharacterInput
{
    public interface IJumpInput
    {
        bool JumpPerformed { get; set; }
        bool JumpCanceled { get; set; }
    }
}