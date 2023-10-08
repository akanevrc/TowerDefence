using System;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct UnitState
    {
        public static UnitState None(float time) => new() { Kind = 0, Time = time };
        public static UnitState Ready(float time) => new() { Kind = 1, Time = time };
        public static UnitState Starting(float time) => new() { Kind = 2, Time = time };
        public static UnitState Attack(float time) => new() { Kind = 3, Time = time };
        public static UnitState Ending(float time) => new() { Kind = 4, Time = time };

        public static UnitState[] ReadySequence =
            new UnitState[]
            {
                Ready(float.PositiveInfinity)
            };
        public static UnitState[] AttackSequence =
            new UnitState[]
            {
                Starting(0.2F),
                Attack  (0.0F),
                Ending  (0.4F),
                Ready   (float.PositiveInfinity)
            };

        public int Kind;
        public float Time;
    }
}
