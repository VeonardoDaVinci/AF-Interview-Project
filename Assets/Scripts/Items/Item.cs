namespace AFSInterview.Items
{
	using System;
	using UnityEngine;
    /// <summary>
    /// ItemType.Generic - The item gets picked up as is.
    /// ItemType.Consumable - The item gets consumed. If PickupItem is specified, that is what gets picked up.
	/// Else the player receives money according to specified Value.
    /// </summary>
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