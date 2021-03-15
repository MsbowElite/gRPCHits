namespace gRPCHits
{
    public class Counter
    {
        private int _currentValue = 0;

        public int CurrentValue { get => _currentValue; }

        public void Increment()
        {
            _currentValue++;
        }
    }
}