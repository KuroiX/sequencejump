namespace Features.Player.Controller
{
    public interface IJumpInput
    {
        bool JumpPerformed { get; }
        
        bool JumpBuffered { get; }
        bool JumpCanceled { get; }
    }
}