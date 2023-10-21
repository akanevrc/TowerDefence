using System;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct WaveNumber : IKindType<WaveNumber>
    {
        public int World;
        public int Stage;
        public int Wave;

        public WaveNumber FromInt(int i)
        {
            return new WaveNumber
            {
                World = i / 1000_000,
                Stage = i / 1000 % 1000,
                Wave = i % 1000
            };
        }

        public readonly int ToInt()
        {
            return (World * 1000 + Stage) * 1000 + Wave;
        }
    }
}
