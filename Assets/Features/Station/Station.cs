namespace Features.Station
{
    public class Station
    {
        private static Station _currentStation;

        public void OpenStation()
        {
            _currentStation = this;
        }

        public void HandleOnTriggerEnter(ResettableQueue<int> queue)
        {
            if (_currentStation == this)
            {
                queue.ResetQueue();
            }
        }
    }
}