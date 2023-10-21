using System;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct StageNumber : IKindType<StageNumber>
    {
        public int World;
        public int Stage;

        public readonly StageNumber FromInt(int i)
        {
            return new StageNumber
            {
                World = i / 1000_000,
                Stage = i / 1000 % 1000
            };
        }

        public readonly int ToInt()
        {
            return (World * 1000 + Stage) * 1000;
        }
    }
}
