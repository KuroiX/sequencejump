namespace Features.Player.Controller
{
    public interface IDashController
    {
        bool IsDashing { get; }
        void Dash(float direction);
        void DashEnd();
        void HandleFixedUpdate();
    }
}