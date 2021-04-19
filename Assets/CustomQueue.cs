using System.Collections.Generic;

public class CustomQueue<T> {
    
    private List<T> _list;
    private int _index;
    
    public CustomQueue()
    {
        _list = new List<T>();
        _index = 0;
    }

    /// <summary>
    /// Queue up a T
    /// </summary>
    /// <param name="value">This is the parameter that gets queued up</param>
    /// <returns>true if successful, false if not successful</returns>
    public void Enqueue(T value)
    {
        _list.Add(value);
    }

    /// <summary>
    /// Dequeue a T
    /// </summary>
    /// <returns>T that got dequeued</returns>
    public T Dequeue()        
    {
         if (IsEmpty())
            return default;
        
         return _list[_index++];
    }

    /// <summary>
    /// See the next T to be dequeued
    /// </summary>
    /// <returns>the next T to be dequeued</returns>
    public T Peek()
    {
        return _list[_index];
    }

    /// <summary>
    /// Tells you if the queue is empty
    /// </summary>
    /// <returns>true if empty, false if not empty</returns>
    public bool IsEmpty()
    {
        return _index >= _list.Count;
    }

    /// <summary>
    /// Reset the queue
    /// </summary>
    public void ResetQueue()
    {
        _index = 0;
    }
}