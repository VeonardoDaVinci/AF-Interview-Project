using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AFSInterview
{
    public class CombatManager : Singleton<CombatManager>
    {
        private List<CombatUnit> unitsInOrder = new ();
        [SerializeField] private List<Army> armyList;
        [SerializeField] private float timeBetweenActions = 1f;
        [SerializeField] private int currentUnitIndex = 0;
        private bool isCombatInProgress = true;
        private void Start()
        {
            InitializeUnitOrder();
            StartCoroutine(CombatProceedRoutine(timeBetweenActions));
        }


        private IEnumerator CombatProceedRoutine(float timeBetweenSteps)
        {
            while(isCombatInProgress)
            {
                yield return new WaitForSeconds(timeBetweenSteps);
                DoNextCombatAction();
            }
        }

        public void DoNextCombatAction()
        {
            unitsInOrder[currentUnitIndex].DoAction();
            currentUnitIndex++;
            currentUnitIndex %= unitsInOrder.Count;
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
            int removedObjectIndex = unitsInOrder.IndexOf(unit);
            if(removedObjectIndex <= currentUnitIndex)
            {
                currentUnitIndex--;
            }
            unitsInOrder.Remove(unit);
            unit.ArmyMembership.RemoveUnit(unit);
            if(unit.ArmyMembership.Units.Count == 0)
            {
                isCombatInProgress = false;
            }
            
        }
    }
}
