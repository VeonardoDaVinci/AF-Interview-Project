namespace AFSInterview.Items
{
	using System.Collections.Generic;
	using UnityEngine;
	using static UnityEditor.Progress;

	public class InventoryController : MonoBehaviour
	{
		[SerializeField] private List<Item> items;
		[SerializeField] private int money;
        [SerializeField] private int itemSellMaxValue;
        public int Money => money;
		public int ItemsCount => items.Count;

		private ItemsManager itemsManager;

        private void Start()
        {
			itemsManager = ItemsManager.Instance;
            itemsManager.ItemPickedUp += ItemsManager_ItemPickedUp;
            itemsManager.SetMoneyOnDisplay(money);
        }

        private void ItemsManager_ItemPickedUp(Item item)
        {
            PickupItem(item);
        }

        private void PickupItem(Item item)
        {
            switch (item.Type)
            {
                case ItemType.Generic:
                    AddItem(item);
                    break;
                case ItemType.Consumable:
                    if (item.PickupItem.ItemData != null && item.PickupItem.ItemData != item.ItemData)
                    {
                        PickupItem(item.PickupItem);
                        break;
                    }
                    AddMoney(item.Value);
                    break;
            }
            Debug.Log("Picked up " + item.Name + " with value of " + item.Value + " and now have " + ItemsCount + " items");
        }

        private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
                SellAllItemsUpToValue(itemSellMaxValue);
        }

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
                AddMoney(itemValue);
                items.RemoveAt(i);
            }
        }

        private void AddMoney(int itemValue)
        {
            money += itemValue;
            itemsManager.SetMoneyOnDisplay(money);
        }

        public void AddItem(Item item)
		{
			items.Add(item);
		}
	}
}