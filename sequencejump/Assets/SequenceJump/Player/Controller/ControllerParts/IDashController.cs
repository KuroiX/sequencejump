namespace SequenceJump.Player.Controller.ControllerParts
{
    public interface IDashController
    {
        bool IsDashing { get; }
        void Dash(float direction);
        void DashEnd();
        void HandleFixedUpdate();
    }
}