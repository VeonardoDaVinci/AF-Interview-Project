using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    public enum ItemType
    {
        Generic,
        Consumable
    }
    public interface IItem
    {
        public string Name { get; }
        public int Value { get; }
        public ItemType Type { get; }
    }
}
