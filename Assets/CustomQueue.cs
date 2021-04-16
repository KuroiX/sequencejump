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

    public T Dequeue()        
    {
         if (IsEmpty())
            return default;
        
         return _list[_index++];
    }

    public T Peek()
    {
        return _list[_index];
    }

    public bool IsEmpty()
    {
        return _index >= _list.Count;
    }

    public void ResetQueue()
    {
        _index = 0;
    }
}