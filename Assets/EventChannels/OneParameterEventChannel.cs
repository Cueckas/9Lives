using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneParamenterChannel<T> : ScriptableObject
{
   event System.Action<T> Delegate;

    public void Broadcast(T obj)
    {
        Delegate?.Invoke(obj);
    }

    public void AddListener(System.Action<T> action)
    {
        Delegate += action;
    }

    public void RemoveListener(System.Action<T> action)
    {
        Delegate -= action;
    }
}
