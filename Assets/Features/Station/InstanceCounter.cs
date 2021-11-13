using System.Collections.Generic;

namespace Features.Station
{
    public class InstanceCounter<T>
    {
        private readonly Dictionary<T, int> _availableInstances;

        public Dictionary<T, int> CurrentAvailableInstances;

        public InstanceCounter(T[] instance, int[] count)
        {
            _availableInstances = new Dictionary<T, int>();
            CurrentAvailableInstances = new Dictionary<T, int>();

            for (int i = 0; i < count.Length; i++)
            {
                _availableInstances[instance[i]] = count[i];
            }
            
            ResetCurrentAvailableInstances();
        }
        
        public void ResetCurrentAvailableInstances()
        {
            foreach (var key in _availableInstances.Keys)
            {
                int amount = _availableInstances[key];
                CurrentAvailableInstances[key] = amount;
            }
        }

        public bool RemoveInstance(T instance)
        {
            bool hasInstanceLeft = HasInstanceLeft(instance);
            
            if (hasInstanceLeft)
            {
                CurrentAvailableInstances[instance]--;
            }
   
            return hasInstanceLeft;
        }

        public bool HasInstanceLeft(T instance)
        {
            return CurrentAvailableInstances[instance] != 0;
        }
    }
}