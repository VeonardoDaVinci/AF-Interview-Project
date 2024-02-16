namespace AFSInterview.Items
{
	using System;
	using UnityEngine;

    public enum ItemType
    {
        Generic,
        Consumable
    }
    [Serializable]
	public class Item
    {
		[SerializeField] private ItemData data;
		public ItemData ItemData => data;
		public string Name => data.Name;
		public int Value => data.Value;
		public ItemType Type => data.Type;
		public Item PickupItem => data.PickupItem;

        public Item(ItemData data)
		{
			this.data= data;
		}

		public void Use()
		{
			Debug.Log("Using" + Name);
		}
	}
}