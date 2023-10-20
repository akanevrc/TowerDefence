using System.Collections.Generic;
using UnityEngine;

namespace akanevrc.TowerDefence
{
    [Presenter]
    public class StageFactory
    {
        public Stage Create(IEnumerable<Vector2Int> route)
        {
            return new Stage(route);
        }
    }
}
