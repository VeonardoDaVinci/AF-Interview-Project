using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace AFSInterview
{
    public abstract class UnitState
    {
        protected CombatUnit Unit { get; set; }
        public abstract void DoAction();

        protected UnitState(CombatUnit unit)
        {
            Unit = unit;
        }
    }

    public class AttackState : UnitState
    {
        private CombatUnit attackTarget;
        public override void DoAction()
        {
            attackTarget = CombatManager.Instance.GetRandomEnemyArmy(Unit.ArmyMembership).GetRandomUnit();
            if(attackTarget!= null)
            {
                attackTarget.TakeDamage(CalculateAttackDamage());
                Unit.transform.DOScale(1.2f, 0.1f).SetLoops(2, LoopType.Yoyo);
            }
            Unit.ChangeState(new WaitingState(Unit));
        }
        private int CalculateAttackDamage()
        {
            int attack = 0;
            if (attackTarget.UnitData.Attribute.HasFlag(Unit.UnitData.AttackDamageOverride.Attribute) && !Unit.UnitData.AttackDamageOverride.Attribute.Equals(UnitAttribute.None))
            {
                attack = Unit.UnitData.AttackDamageOverride.AttackDamage - attackTarget.UnitData.ArmorPoints;
            }
            else 
            {
                attack = Unit.UnitData.AttackDamage - attackTarget.UnitData.ArmorPoints;
            }
            Debug.Log("Attack made by " + Unit.name + " on " + attackTarget.name + " for " + attack + " damage.");
            return attack > 1 ? attack : 1;
        }
        public AttackState(CombatUnit unit) : base(unit) { }
    }

    public class WaitingState : UnitState
    {
        private int cooldown;
        public override void DoAction()
        {
            cooldown--;
            if(cooldown <= 0)
            {
                Unit.ChangeState(new AttackState(Unit));
                Unit.DoAction();
            }
        }
        public WaitingState(CombatUnit unit) : base(unit) 
        {
            cooldown = unit.UnitData.AttackInterval;
        }
    }

    public class DeadState : UnitState
    {
        public override void DoAction()
        {
            CombatManager.Instance.RemoveUnitFromCombat(Unit);
        }
        public DeadState(CombatUnit unit) : base(unit) { Debug.Log("Create dead state for "+Unit.UnitData.name); }
    }
}
