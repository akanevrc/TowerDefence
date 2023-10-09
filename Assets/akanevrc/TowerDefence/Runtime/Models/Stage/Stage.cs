using System.Collections.Generic;
using UnityEngine;

namespace akanevrc.TowerDefence
{
    public struct Stage
    {
        public RouteMarkPoints Route;

        public Stage(IEnumerable<Vector2Int> route)
        {
            Route = new RouteMarkPoints(route);
        }
    }
}
