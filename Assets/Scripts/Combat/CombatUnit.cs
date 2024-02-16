using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    public class CombatUnit : MonoBehaviour
    {
        [HideInInspector] public Army ArmyMembership;
        public UnitData UnitData => data;
        [SerializeField] private UnitData data;
        private UnitState state;

        public int HealthPoints => health;
        private int health;


        private void Awake()
        {
            state = new WaitingState(this);
            health = UnitData.HealthPoints;
        }

        public void ChangeState(UnitState state)
        {
            this.state = state;
        }

        public void DoAction()
        {
            if (state == null) state = new WaitingState(this);
            state.DoAction();
        }

        public void TakeDamage(int amount)
        {
            health -= amount;
            transform.DOScale(0.5f, 0.1f).SetLoops(2, LoopType.Yoyo);
            if (health > 0) return;
            ChangeState(new DeadState(this));
            DoAction();

        }
    }
}
