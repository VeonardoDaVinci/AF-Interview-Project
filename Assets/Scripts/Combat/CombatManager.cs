using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AFSInterview
{
    public class CombatManager : Singleton<CombatManager>
    {
        [SerializeField] private List<CombatUnit> unitsInOrder;
        [SerializeField] private List<Army> armyList;

        private void Start()
        {
            InitializeUnitOrder();
        }
        private void InitializeUnitOrder()
        {
            List<CombatUnit> tempUnitList = new List<CombatUnit>();
            foreach(Army a in armyList)
            {
                tempUnitList.AddRange(a.Units);
            }
            while(tempUnitList.Count > 0)
            {
                int rngIndex = Random.Range(0,tempUnitList.Count);
                unitsInOrder.Add(tempUnitList[rngIndex]);
                tempUnitList.RemoveAt(rngIndex);
            }
        }

        public Army GetRandomArmy()
        {
            return armyList[Random.Range(0, armyList.Count)];
        }

        public Army GetRandomEnemyArmy(Army friendlyArmy)
        {
            Army randomArmy = GetRandomArmy();
            return randomArmy != friendlyArmy ? randomArmy : GetRandomEnemyArmy(friendlyArmy);
        }

        public CombatUnit GetRandomUnit()
        {
            return unitsInOrder[Random.Range(0, unitsInOrder.Count)];
        }

        public void RemoveUnitFromCombat(CombatUnit unit)
        {
            unitsInOrder.Remove(unit);
            unit.ArmyMembership.RemoveUnit(unit);
        }
    }
}
