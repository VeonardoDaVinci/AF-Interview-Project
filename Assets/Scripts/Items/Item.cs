namespace AFSInterview.Items
{
	using System;
	using UnityEngine;

	[Serializable]
	public class Item : IItem
	{
		[SerializeField] private string name;
		[SerializeField] private int value;
		[SerializeField] private ItemType type;
		public string Name => name;
		public int Value => value;
		public ItemType Type => type;

        public Item(string name, int value, ItemType type)
		{
			this.name = name;
			this.value = value;
			this.type = type;
		}

		public void Use()
		{
			Debug.Log("Using" + Name);
		}
	}
}