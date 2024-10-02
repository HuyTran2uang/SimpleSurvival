using System;
using System.Collections;
using UnityEngine;

public static class Utilities
{
    public static IEnumerator DelayCall(float duration, Action callback)
    {
        yield return new WaitForSeconds(duration);
        callback?.Invoke();
    }
}