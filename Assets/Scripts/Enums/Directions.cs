using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Rooms
{
    public enum Directions
    {
        North,
        South,
        East,
        West,
        NorthEast,
        SouthEast,
        NorthWest,
        SouthWest
    }


    class RoomConstants
    {
        public static readonly Dictionary<Directions, Vector2> NodeOffsets = new Dictionary<Directions, Vector2>()
        {
            {Directions.North, new Vector2(0f, 300f) },
            {Directions.South, new Vector2(0f, -300f) },
            {Directions.East, new Vector2(-600f, 0f) },
            {Directions.West, new Vector2(600f, 0) },
            {Directions.NorthEast, new Vector2(-400f, 200f)},
            {Directions.SouthEast, new Vector2(-400f, -200f)},
            {Directions.NorthWest, new Vector2(400f, 200f)},
            {Directions.SouthWest, new Vector2(400f, -200f)}
        };
    }
    
}
