using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    public class Army : MonoBehaviour
    {
        public List<CombatUnit> Units => units;
        [SerializeField] public List<CombatUnit> units;
        [SerializeField] private BoxCollider armySpawnArea;

        private void Start()
        {
            var spawnAreaBounds = armySpawnArea.bounds;
            foreach(var unit in units)
            {
                var position = new Vector3(
                    Random.Range(spawnAreaBounds.min.x, spawnAreaBounds.max.x),
                    0f,
                    Random.Range(spawnAreaBounds.min.z, spawnAreaBounds.max.z)
                );

                Instantiate(unit.gameObject, position, Quaternion.identity);
                unit.ArmyMembership = this;
            }
        }

        public CombatUnit GetRandomUnit()
        {
            return units[Random.Range(0, units.Count)];
        }

        public void RemoveUnit(CombatUnit unit)
        {
            units.Remove(unit);
        }
    }
}
