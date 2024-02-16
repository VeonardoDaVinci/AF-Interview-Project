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
        [SerializeField] private float timeBetweenActions = 1f;
        private float currentTimeBetweenActions;
        private int currenUnitIndex;
        private bool isCombatInProgress = true;
        private void Start()
        {
            InitializeUnitOrder();
            currentTimeBetweenActions = timeBetweenActions;
        }

        private void Update()
        {
            if (!isCombatInProgress) return;
            currentTimeBetweenActions -= Time.deltaTime;
            if(currentTimeBetweenActions <= 0)
            {
                currentTimeBetweenActions = timeBetweenActions;
                DoNextCombatAction();
            }
        }

        private void DoNextCombatAction()
        {
            unitsInOrder[currenUnitIndex].DoAction();
            currenUnitIndex++;
            currenUnitIndex %= unitsInOrder.Count;
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
            if(unit.ArmyMembership.Units.Count == 0)
            {
                isCombatInProgress = false;
            }
            
        }
    }
}
