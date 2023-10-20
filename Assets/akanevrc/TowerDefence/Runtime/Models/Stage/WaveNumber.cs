using System;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct WaveNumber
    {
        public int World;
        public int Stage;
        public int Wave;

        public int ToInt()
        {
            return (World * 1000 + Stage) * 1000 + Wave;
        }
    }
}
