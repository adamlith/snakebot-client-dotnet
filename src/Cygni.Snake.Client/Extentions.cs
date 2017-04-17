﻿namespace Cygni.Snake.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Extentions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var t in enumerable)
            {
                action(t);
            }
        }

        public static IEnumerable<MapCoordinate> Neighbours(this MapCoordinate mapCoordinate)
        {
            return mapCoordinate.NeighboursOfDistance(1);
        }

        public static IEnumerable<MapCoordinate> NeighboursOfDistance(this MapCoordinate origo, int manhattanDistance)
        {
            if (manhattanDistance <= 0)
                throw new ArgumentOutOfRangeException(nameof(manhattanDistance));

            yield return new MapCoordinate(origo.X, origo.Y - manhattanDistance);

            for (int y = -manhattanDistance + 1; y < manhattanDistance; y++)
            {
                var x = manhattanDistance - Math.Abs(y);
                yield return new MapCoordinate(origo.X + x, origo.Y + y);
                yield return new MapCoordinate(origo.X - x, origo.Y + y);
            }

            yield return new MapCoordinate(origo.X, origo.Y + manhattanDistance);
        }

        public static IEnumerable<MapCoordinate> NeighboursWithinDistance(this MapCoordinate origo, int manhattanDistance)
        {
            if (manhattanDistance <= 0)
                throw new ArgumentOutOfRangeException(nameof(manhattanDistance));

            for (var i = 1; i <= manhattanDistance; i++)
            {
                foreach (var coordinate in origo.NeighboursOfDistance(i))
                {
                    yield return coordinate;
                }
            }
        }
    }
}