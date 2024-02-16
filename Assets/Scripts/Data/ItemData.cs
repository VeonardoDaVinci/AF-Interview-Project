using AFSInterview.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace AFSInterview
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "ItemData", order = 1)]
    public class ItemData : ScriptableObject
    {
        public string Name;
        public int Value;
        public ItemType Type;
        public Item PickupItem;
    }
}
