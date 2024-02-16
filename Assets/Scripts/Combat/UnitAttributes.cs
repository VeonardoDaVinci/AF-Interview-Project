using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    [Flags]
    public enum UnitAttribute
    {
        None = 0,
        Light = 1,
        Armored = 2,
        Mechanichal= 4
    }

    [Serializable]
    public struct DamageOverride
    {
        public UnitAttribute Attribute;
        public int AttackDamage;
    }

}
