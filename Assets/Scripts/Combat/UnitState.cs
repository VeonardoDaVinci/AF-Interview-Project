using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    public abstract class UnitState
    {
        protected Unit Unit { get; set; }
        protected abstract void DoAction();

        protected UnitState(Unit unit)
        {
            Unit = unit;
        }
    }

    public class ReadyState : UnitState
    {
        protected override void DoAction()
        {
            throw new System.NotImplementedException();
        }
        ReadyState(Unit unit) : base(unit) { }
    }

    public class WaitingState : UnitState
    {
        protected override void DoAction()
        {
            throw new System.NotImplementedException();
        }
        WaitingState(Unit unit) : base(unit) { }
    }

    public class DeadState : UnitState
    {
        protected override void DoAction()
        {
            throw new System.NotImplementedException();
        }
        DeadState(Unit unit) : base(unit) { }
    }
}
