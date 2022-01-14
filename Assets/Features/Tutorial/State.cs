namespace Features.Tutorial
{
    public abstract class State
    {
        protected abstract void OnEnter();
        protected abstract void OnStay();
        protected abstract void OnExit();
    }
}