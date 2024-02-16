using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    public class CombatUnit : MonoBehaviour
    {
        public Army ArmyMembership;
        public UnitData UnitData => data;
        [SerializeField] private UnitData data;

        private UnitState state;

        public int HealthPoints => health;
        private int health;
        private CombatUnit(UnitData data)
        {
            state = new WaitingState(this);
            this.data = data;
            this.health = data.HealthPoints;
        }

        public void ChangeState(UnitState state)
        {
            this.state = state;
        }

        public void DoAction()
        {
            state.DoAction();
        }

        public void TakeDamage(int amount)
        {
            health -= amount;
            if (health > 0) return;
            ChangeState(new DeadState(this));
        }
    }
}
