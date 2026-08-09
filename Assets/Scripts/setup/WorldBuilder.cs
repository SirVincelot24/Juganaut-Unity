using System;
using System.Collections.Generic;
using System.Linq;
using space;
using UnityEngine;
using WorldItems;
using Random = UnityEngine.Random;

namespace setup
{
    public static class WorldBuilder
    {
        public static World CreateWorld(
            int width, int height,
            Vector2 diamondCountRange,
            Vector2 monsterCountRange,
            Vector2 bombCountRange,
            Vector2 rockCountRange,
            Coord playerCoord
            )
        {
            var world = new World(width, height);
            // create items
            CreateItems(world, rockCountRange, playerCoord, () => WorldItemType.Rock);
            CreateItems(world, monsterCountRange, playerCoord, () => WorldItemType.Monster);
            CreateItems(world, bombCountRange, playerCoord, () => WorldItemType.Bomb);
            CreateItems(world, diamondCountRange, playerCoord, () => WorldItemType.Diamond);

            world.SetField(playerCoord, WorldItemType.Player);
            
            foreach (var coord in playerCoord.Neighbors())
            {
                switch (world.GetField(coord))
                {
                    case WorldItemType.Diamond:
                        break;
                    default:
                        world.SetField(coord, WorldItemType.Dirt);
                        break;
                }
            }
            return world;
        }

        private static Coord GetValidCoord(World world, Coord playerCoord)
        {
            var xRange = world.ValidXRange.Where(i => i != playerCoord.X);
            var yRange = world.ValidYRange.Where(i => i != playerCoord.Y);
            var xIndex = Random.Range(0, world.Width - 1);
            var yIndex = Random.Range(0, world.Height - 1);
            return new Coord(xRange.ElementAt(xIndex), yRange.ElementAt(yIndex));
        }

        private static void CreateItems(World world, Vector2 itemCountRange, Coord playerCoord,
            Func<WorldItemType> itemFactory)
        {
            var itemCount = Random.Range(itemCountRange.x, itemCountRange.y);
            for (var i = 0; i < itemCount; i++)
            {
                world.SetField(GetValidCoord(world, playerCoord), itemFactory());
            }
        }
    }
}