using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using space;
using UnityEngine;
using WorldItems;
using Object = UnityEngine.Object;

public class World
{
    private readonly List<List<WorldItemType>> _fields;

    public readonly IEnumerable<int> ValidXRange;
    public readonly IEnumerable<int> ValidYRange;

    public readonly int Width;
    public readonly int Height;
    
    public World(int width, int height)
    {
        Width = width;
        Height = height;
        _fields = Enumerable.Range(0, Height)
            .Select(_ => new List<WorldItemType>(Enumerable.Repeat(WorldItemType.Dirt, Width)))
            .ToList();
        ValidXRange = Enumerable.Range(0, Width);
        ValidYRange = Enumerable.Range(0, Height);
    }
    
    public void SpawnItems(float gridSize)
    {
        var worldItemList = Resources.LoadAll<WorldItem>("Scriptable Objects");
        for (var row = 0; row < Height; row++)
        {
            for (var col = 0; col < Width; col++)
            {
                var item = _fields[row][col];
                Object.Instantiate(worldItemList.FirstOrDefault(i => i.type == item)?.prefab, new Vector3(row, col) * gridSize,
                    Quaternion.identity);
            }
        }
    }

    public bool IsValid(Coord coord)
    {
        return ValidXRange.Contains(coord.X) && ValidYRange.Contains(coord.Y);
    }

    public void SetField(Coord coord, WorldItemType item)
    {
        _fields[coord.Y][coord.X] = item;
    }

    public WorldItemType GetField(Coord coord)
    {
        return _fields[coord.Y][coord.X];
    }
    
    [CanBeNull]
    public Coord Find(Func<WorldItemType, bool> condition)
    {
        Coord result = null;
        _foreachCoord(coord =>
        {
            if (!condition(GetField(coord))) return;
            result = coord;
        });
        return result;
    }
    
    public List<Coord> FindAll(Func<WorldItemType, bool> condition)
    {
        var result = new List<Coord>();
        _foreachCoord(coord =>
        {
            if (condition(GetField(coord)))
            {
                result.Add(coord);
            }
        });
        return result;
    }

    private void _foreachCoord(Action<Coord> block)
    {
        foreach (var x in ValidXRange)
        {
            foreach (var y in ValidYRange)
            {
                var c = new Coord(x, y);
                block(c);
            }
        }
    }

    public int Count(Func<WorldItemType, bool> condition)
    {
        var result = 0;
        _foreachCoord(coord =>
        {
            if (!condition(GetField(coord))) return;
            result++;
        });
        return result;
    }
}