namespace Features.Player
{
    public interface ICharacterInput
    {
        float Horizontal { get; }
        bool JumpPerformed { get; }
        
        bool JumpBuffered { get; }
        bool JumpCanceled { get; }
        float JumpTimeStamp { get; set; }
        
        float JumpEndTimeStamp { get; }
        
    }
}