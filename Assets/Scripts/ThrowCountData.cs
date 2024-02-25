using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ThrowCountData
{
    public int HighestThrowCount { get; }

    public ThrowCountData(int throwAmount)
    {
        HighestThrowCount = 0;
        if (throwAmount > HighestThrowCount)
            HighestThrowCount = throwAmount;
    }
}
