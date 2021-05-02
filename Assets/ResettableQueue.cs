using System.Collections.Generic;

/// <summary>
/// Same functionality as a queue, but information is not lost so it can be reset.
/// </summary>
/// <typeparam name="T">Generic type</typeparam>
public class ResettableQueue<T> {
    
    private List<T> _list;
    private int _index;
    
    public ResettableQueue()
    {
        _list = new List<T>();
        _index = 0;
    }

    /// <summary>
    /// Enqueues an item
    /// </summary>
    /// <param name="value">Item that gets enqueued</param>
    /// <returns>true if successful, false if not successful</returns>
    public void Enqueue(T item)
    {
        _list.Add(item);
    }

    /// <summary>
    /// Dequeues the next item
    /// </summary>
    /// <returns>Item that gets dequeued</returns>
    public T Dequeue()        
    {
         if (IsEmpty())
            return default;
        
         return _list[_index++];
    }

    /// <summary>
    /// Shows the next item to be dequeued
    /// </summary>
    /// <returns>Next item to be dequeued</returns>
    public T Peek()
    {
        return _list[_index];
    }

    /// <summary>
    /// Tells you if the queue is empty
    /// </summary>
    /// <returns>True</returns>
    public bool IsEmpty()
    {
        return _index >= _list.Count;
    }

    /// <summary>
    /// Resets the queue
    /// </summary>
    public void ResetQueue()
    {
        _index = 0;
    }

    /// <summary>
    /// Clears the current queue
    /// </summary>
    public void Clear()
    {
        _list.Clear();
        _index = 0;
    }
}