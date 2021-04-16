using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomQueue<T> : MonoBehaviour
{
    private List<T> queue = new List<T>();
    private int counter = 0;
    private int queueSize = 7;

    public bool enqueue(T value)
    {
        if (queue.Count == queueSize)
            return false;
        
        queue.Add(value);
        counter++;
        return true;
    }

    public object dequeue()        //public T dequeue() gint nicht wegen dem null
    {
         if (queue.Count == 0)
             return null;
        
         return queue[counter--];
    }

    public T peek()
    {
        return queue[counter];
    }

    public bool isFull()
    {
        if (queue.Count == queueSize)
            return true;

        return false;
    }

    public bool isEmpty()
    {
        if (queue.Count == 0)
            return true;

        return false;
    }

    public void resetQueue()
    {
        counter = queue.Count;
    }
}
