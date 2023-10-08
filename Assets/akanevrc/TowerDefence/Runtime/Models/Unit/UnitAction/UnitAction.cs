using System;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct UnitAction
    {
        public UnitState[] Sequence;
        public int Index;
        public float RemainingTime;

        public readonly UnitState CurrentState => Index < Sequence.Length ? Sequence[Index] : UnitState.None(0.0F);

        public UnitAction(UnitState[] sequence)
        {
            Sequence = sequence;
            Index = 0;
            RemainingTime = Sequence.Length == 0 ? float.PositiveInfinity : Sequence[0].Time;
        }
    }
}
