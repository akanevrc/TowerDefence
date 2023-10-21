using System;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct UnitState
    {
        public enum KindType
        {
            None,
            Ready,
            Starting,
            Attack,
            Ending
        }

        public static UnitState None(float time) => new() { Kind = KindType.None, Time = time };
        public static UnitState Ready(float time) => new() { Kind = KindType.Ready, Time = time };
        public static UnitState Starting(float time) => new() { Kind = KindType.Starting, Time = time };
        public static UnitState Attack(float time) => new() { Kind = KindType.Attack, Time = time };
        public static UnitState Ending(float time) => new() { Kind = KindType.Ending, Time = time };

        public static UnitState[] ReadySequence =
            new UnitState[]
            {
                Ready(float.PositiveInfinity)
            };
        public static UnitState[] AttackSequence =
            new UnitState[]
            {
                Starting(0.2F),
                Attack(0.0F),
                Ending(0.4F),
                Ready(float.PositiveInfinity)
            };

        public KindType Kind;
        public float Time;
    }
}
