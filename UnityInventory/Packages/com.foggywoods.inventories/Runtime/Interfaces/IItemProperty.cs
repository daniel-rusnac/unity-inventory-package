using System;

namespace FoggyWoods.Inventories
{
    public interface IItemProperty
    {
        string Key {get;}
        object Value {get;}
        Type Type {get;}
    }
}