using System;

namespace ItemManagement.Items
{
    public interface IItem : ICloneable
    {
        string Id { get; }
        bool IsStackable { get; }
    }
}