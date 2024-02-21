using System;
using UnityEngine;
public abstract class Module
{
    public string ID;
    public abstract void Delete();
    public abstract void InternalUpdate();
    protected abstract void _onCreate();
    protected abstract void _onDelete();
    
}
