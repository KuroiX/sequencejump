using System;
using System.Collections.Generic;

namespace Features.StationLogic
{
    public class InstanceCounter<T>
    {
        private readonly Dictionary<T, int> _availableCount;

        public Dictionary<T, int> CurrentCount { get; }

        public InstanceCounter(T[] instance, int[] count)
        {
            _availableCount = new Dictionary<T, int>();
            CurrentCount = new Dictionary<T, int>();

            if (count.Length != instance.Length)
            {
                throw new FormatException(
                    $"Instance length {instance.Length} and count length {count.Length} must be equal.");
            }

            for (int i = 0; i < count.Length; i++)
            {
                if (count[i] < 0)
                {
                    throw new ArgumentException($"Instance count at index {i} is negative, but should only be positive.");
                }
                _availableCount[instance[i]] = count[i];
                CurrentCount[instance[i]] = count[i];
            }
        }
        
        public void Reset()
        {
            foreach (var key in _availableCount.Keys)
            {
                int amount = _availableCount[key];
                CurrentCount[key] = amount;
            }
        }

        public bool Remove(T instance)
        {
            bool hasInstanceLeft = HasLeft(instance);
            
            if (hasInstanceLeft)
            {
                CurrentCount[instance]--;
            }
   
            return hasInstanceLeft;
        }

        public bool HasLeft(T instance)
        {
            return CurrentCount[instance] != 0;
        }
    }
}