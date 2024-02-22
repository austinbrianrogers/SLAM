using System;
using UnityEngine;
public abstract class Module
{
    /// <summary>
    /// You must declare a constructor within the inheriting class
    /// that sets up the ID with a unique string. You must also
    /// make sure that you call the _onCreate method if required.
    /// </summary>
    public Module()
    {
        ID = "GenericModule";
        _onCreate();
    }
    public string ID;
    public void Delete()
    {
        _onDelete();
    }
    public abstract void InternalUpdate();
    protected abstract void _onCreate();
    protected abstract void _onDelete();
    
}
