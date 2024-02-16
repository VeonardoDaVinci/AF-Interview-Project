namespace AFSInterview.Items
{
	using System;
	using TMPro;
	using UnityEngine;
    using Random = UnityEngine.Random;

    public class ItemsManager : Singleton<ItemsManager>
	{
        public event Action<Item> ItemPickedUp;
		[SerializeField] private TextMeshProUGUI moneyDisplay;
		[SerializeField] private Transform itemSpawnParent;
		[SerializeField] private GameObject itemPrefab;
		[SerializeField] private BoxCollider itemSpawnArea;
		[SerializeField] private float itemSpawnInterval;
		
		private float nextItemSpawnTime;

        private void Update()
		{
			if (Time.time >= nextItemSpawnTime)
				SpawnNewItem();
			
			if (Input.GetMouseButtonDown(0))
				TryPickUpItem();
		}

		public void SetMoneyOnDisplay(float money)
		{
			moneyDisplay.text = "Money: " + money;
		}

		private void SpawnNewItem()
		{
			nextItemSpawnTime = Time.time + itemSpawnInterval;
			
			var spawnAreaBounds = itemSpawnArea.bounds;
			var position = new Vector3(
				Random.Range(spawnAreaBounds.min.x, spawnAreaBounds.max.x),
				0f,
				Random.Range(spawnAreaBounds.min.z, spawnAreaBounds.max.z)
			);
			
			Instantiate(itemPrefab, position, Quaternion.identity, itemSpawnParent);
		}

		private void TryPickUpItem()
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var layerMask = LayerMask.GetMask("Item");
			if (!Physics.Raycast(ray, out var hit, 100f, layerMask) || !hit.collider.TryGetComponent<IItemHolder>(out var itemHolder))
				return;
			
			var item = itemHolder.GetItem(true);
			ItemPickedUp?.Invoke(item);
		}
	}
}