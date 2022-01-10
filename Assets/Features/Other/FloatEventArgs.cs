using System;

public class FloatEventArgs : EventArgs
{
    public float Value { get; private set; }

    public FloatEventArgs(float value)
    {
        Value = value;
    }
}
