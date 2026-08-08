using UnityEngine;

namespace WorldItems
{
    [CreateAssetMenu(fileName = "WorldItem",menuName = "Scriptable Objects/WorldItem")]
    public class WorldItem : ScriptableObject
    {
        public WorldItemType type;
        public GameObject prefab;
    }

    public enum WorldItemType
    {
        Bomb,
        Diamond,
        Dirt,
        Empty,
        Monster,
        Player,
        Rock
    }
}