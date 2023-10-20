using System;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct StageNumber
    {
        public int World;
        public int Stage;

        public int ToInt()
        {
            return (World * 1000 + Stage) * 1000;
        }
    }
}
