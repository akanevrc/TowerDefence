using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace akanevrc.TowerDefence
{
    public readonly struct RouteMarkPoints
    {
        private readonly MarkPoint[] _marks;

        public RouteMarkPoints(IEnumerable<Vector2Int> intRoute)
        {
            _marks =
                intRoute
                .Select((point, i) => new MarkPoint() { Id = i, Point = (Vector2)point })
                .ToArray();
        }

        public readonly MarkPoint GetFirst()
        {
            return _marks.Length > 0 ? _marks[0] : MarkPoint.None;
        }

        public readonly MarkPoint GetNext(MarkPoint current)
        {
            return _marks.Length > current.Id + 1 ? _marks[current.Id + 1] : MarkPoint.None;
        }
    }
}
