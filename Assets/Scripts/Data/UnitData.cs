using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "UnitData", order = 1)]
    public class UnitData : ScriptableObject
    {
        public UnitAttribute Attribute;
        public int HealthPoints = 1;
        public int ArmorPoints = 1;
        public int AttackInterval = 1;
        public int AttackDamage = 1;
        public DamageOverride AttackDamageOverride;
    }
}
