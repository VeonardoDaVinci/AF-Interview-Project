using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    public class Army : MonoBehaviour
    {
        public List<CombatUnit> Units => spawnedUnits;
        private List<CombatUnit> spawnedUnits = new();
        [SerializeField] private List<GameObject> unitPrefabs;
        [SerializeField] private BoxCollider armySpawnArea;
        [SerializeField] private float unitSafeSpawnRadius = 2f;
        private void Start()
        {
            if(armySpawnArea == null) GetComponent<BoxCollider>();

            foreach(var prefab in unitPrefabs)
            {
                var position = GetRandomPositionForUnit();
                var spawnUnit = Instantiate(prefab, position, Quaternion.identity);
                CombatUnit unit = spawnUnit.GetComponent<CombatUnit>();
                unit.ChangeState(new WaitingState(unit));
                unit.ArmyMembership = this;
                spawnedUnits.Add(unit);
            }
        }

        private Vector3 GetRandomPositionForUnit()
        {
            var spawnAreaBounds = armySpawnArea.bounds;
            var position = new Vector3(
                    Random.Range(spawnAreaBounds.min.x, spawnAreaBounds.max.x),
                    0f,
                    Random.Range(spawnAreaBounds.min.z, spawnAreaBounds.max.z)
                );
            foreach (var spawn in spawnedUnits)
            {
                if (Vector3.Distance(position, spawn.transform.position) < unitSafeSpawnRadius)
                {
                    position = GetRandomPositionForUnit();
                }
            }
            return position;
        }

        public CombatUnit GetRandomUnit()
        {
            if(spawnedUnits.Count == 0) return null;
            return spawnedUnits[Random.Range(0, spawnedUnits.Count)];
        }

        public void RemoveUnit(CombatUnit unit)
        {
            Destroy(unit.gameObject);
            spawnedUnits.Remove(unit);
        }
    }
}
