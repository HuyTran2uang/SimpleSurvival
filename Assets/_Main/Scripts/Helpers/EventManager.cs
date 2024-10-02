using System;
using System.Collections.Generic;

public static class EventManager
{
    // Dictionary để lưu trữ các sự kiện với tên và delegate
    private static readonly Dictionary<string, Delegate> _eventDictionary = new Dictionary<string, Delegate>();

    // Bắt đầu lắng nghe sự kiện với nhiều tham số
    public static void StartListening(string eventName, Delegate listener) //example: Delegate del = new Action<int, float, bool>(MyFunc);
    {
        if (_eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            _eventDictionary[eventName] = Delegate.Combine(thisEvent, listener);
        }
        else
        {
            _eventDictionary.Add(eventName, listener);
        }
    }

    // Ngừng lắng nghe sự kiện với nhiều tham số
    public static void RemoveListening(string eventName)
    {
        if (_eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            _eventDictionary.Remove(eventName);
        }
    }

    // Kích hoạt sự kiện với nhiều tham số
    public static void TriggerEvent(string eventName, params object[] parameters)
    {
        if (_eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.DynamicInvoke(parameters);
        }
    }
}