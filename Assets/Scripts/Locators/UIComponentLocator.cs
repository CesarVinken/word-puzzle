using System;
using System.Collections.Generic;
using UnityEngine;

public class UIComponentLocator : IServiceLocator
{
    private UIComponentLocator() { }

    private readonly Dictionary<string, IUIComponent> _services = new Dictionary<string, IUIComponent>();

    public static UIComponentLocator Instance { get; private set; }

    public static void Setup()
    {
        Instance = new UIComponentLocator();
    }

    public T Get<T>() where T : IUIComponent
    {
        string key = typeof(T).Name;
        if (!_services.ContainsKey(key))
        {
            ConsoleLog.Error(LogCategory.General, $"{key} not registered with {GetType().Name}");
            throw new InvalidOperationException();
        }

        return (T)_services[key];
    }

    public void Register<T>(T service) where T : IUIComponent
    {
        string key = typeof(T).Name;
        if (_services.ContainsKey(key))
        {
            ConsoleLog.Error(LogCategory.General, $"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
            return;
        }

        _services.Add(key, service);
    }

    public void Deregister<T>() where T : IUIComponent
    {
        string key = typeof(T).Name;
        if (!_services.ContainsKey(key))
        {
            ConsoleLog.Error(LogCategory.General, $"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
            return;
        }

        _services.Remove(key);
    }
}
