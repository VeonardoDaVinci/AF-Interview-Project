namespace AFSInterview.Items
{
	using System.Collections.Generic;
	using UnityEngine;

	public class InventoryController : MonoBehaviour
	{
		[SerializeField] private List<Item> items;
		[SerializeField] private int money;

		public int Money => money;
		public int ItemsCount => items.Count;

		public void SellAllItemsUpToValue(int maxValue)
		{
			int i = 0;
			while (i < items.Count)
			{
				var itemValue = items[i].Value;
				if (itemValue > maxValue)
				{
					i++;
					continue;
				}

				money += itemValue;
				items.RemoveAt(i);
			}
		}

		public void AddItem(Item item)
		{
			items.Add(item);
		}
	}
}