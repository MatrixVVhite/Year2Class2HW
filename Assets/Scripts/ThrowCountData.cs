using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCountData
{
    public int HighestThrowCount { get; }

    public ThrowCountData(int throwAmount)
    {
        if (throwAmount > HighestThrowCount)
            HighestThrowCount = throwAmount;
    }
}
