using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RPG.Rooms
{
    public class RoomLocation : ScriptableObject
    {
        [SerializeField] Rect rect = new Rect(10, 10, 300, 150);

        [SerializeField] [TextArea] string text = null;
        [SerializeField] List<string> locations = new List<string>();
        [SerializeField] bool isLocationChild = true;
        [SerializeField] bool hasNPC = false;
        [SerializeField] bool isEntrance = false;
        [SerializeField] bool isExit = false;
        [SerializeField] bool isTrapped = false;
        [SerializeField] bool hasObstacle = false;
        [SerializeField] List<string> movementDirections = new List<string>();
        [SerializeField] string onEnterAction = null;
        [SerializeField] string onExitAction = null;
        //Make Colapsable thingy here for the bools
        [SerializeField] bool northConnection = false;
        [SerializeField] bool eastConnection = false;
        [SerializeField] bool southConnection = false;
        [SerializeField] bool westConnection = false;
        [SerializeField] bool northEastSouthWestConnection = false;
        [SerializeField] bool southEastNorthWestConnection = false;
        [SerializeField] bool southWestNorthEastConnection = false;
        [SerializeField] bool northWestSouthEastConnection = false;

        private bool lastNorthConnection;
        private bool lastSouthConnection;
        private bool lastEastConnection;
        private bool lastWestConnection;

        private void Awake()
        {
            lastNorthConnection = northConnection;
            lastEastConnection = eastConnection;
            lastSouthConnection = southConnection;
            lastWestConnection = westConnection;
        }

        public Rect GetRect()
        {
            return rect;
        }
        public string GetText()
        {
            return text;
        }
        public List<string> GetLocation()
        {
            return locations;
        }
        public bool IsLocationChild()
        {
            return isLocationChild;
        }
        public bool IsEntrance()
        {
            return isEntrance;
        }
        public bool IsExit()
        {
            return isExit;
        }
        public bool IsTrapped()
        {
            return isTrapped;
        }
        public bool HasObstacle()
        {
            return hasObstacle;
        }
        public bool HasNPC()
        {
            return hasNPC;
        }
        public string GetOnEnterAction()
        {
            return onEnterAction;
        }
        public string GetOnExitAction()
        {
            return onExitAction;
        }
        public List<string> MovementDirections()
        {
            return movementDirections;
        }
        public bool GetNorthConnection()
        {
            return northConnection;
        }
        public bool GetNorthEastSouthWestConnection()
        {
            return northEastSouthWestConnection;
        }
        public bool GetEastConnection()
        {
            return eastConnection;
        }
        public bool GetSouthEastNorthWestConnection()
        {
            return southEastNorthWestConnection;
        }
        public bool GetSouthConnection()
        {
            return southConnection;
        }
        public bool GetSouthWestNorthEastConnection()
        {
            return southWestNorthEastConnection;
        }
        public bool GetWestConnection()
        {
            return westConnection;
        }
        public bool GetNorthWestSouthEastConnection()
        {
            return northWestSouthEastConnection;
        }

#if UNITY_EDITOR
        public void OnValidate()
        {
            //if (lastNorthConnection != northConnection)
            //{
            //    SetNorthConnection();
            //    lastNorthConnection = northConnection;
            //}
            //if (lastEastConnection != eastConnection)
            //{
            //    SetEastConnection();
            //    lastEastConnection = eastConnection;
            //}
            //if (lastSouthConnection != southConnection)
            //{
            //    SetSouthConnection();
            //    lastSouthConnection = southConnection;
            //}
            //if (lastWestConnection != westConnection)
            //{
            //    SetWestConnection();
            //    lastWestConnection = westConnection;
            //}
        }
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Room Location");
            rect.position = newPosition;
            EditorUtility.SetDirty(this);
        }
        public void SetText(string newText)
        {
            if (newText != text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText;
                EditorUtility.SetDirty(this);
            }
        }
        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Child Link");
            locations.Add(childID);
            EditorUtility.SetDirty(this);
        }
        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Child Link");
            locations.Remove(childID);
            EditorUtility.SetDirty(this);
        }

        public void SetLocationChild(bool newLocationChild)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            isLocationChild = newLocationChild;
            EditorUtility.SetDirty(this);
        }
        public void SetEntrance(bool newEntrance)
        {
            Undo.RecordObject(this, "Change Entrance");
            isEntrance = newEntrance;
            EditorUtility.SetDirty(this);
        }
        public void SetExit(bool newExit)
        {
            Undo.RecordObject(this, "Change Exit");
            isExit = newExit;
            EditorUtility.SetDirty(this);
        }
        public void SetTraps(bool newTraps)
        {
            Undo.RecordObject(this, "Change traps");
            isTrapped = newTraps;
            EditorUtility.SetDirty(this);
        }
        public void SetNPC(bool newNPC)
        {
            Undo.RecordObject(this, "Remove NPC");
            hasNPC = newNPC;
            EditorUtility.SetDirty(this);
        }
        public void SetObstacles(bool newObstacle)
        {
            Undo.RecordObject(this, "Remove Obstacle");
            hasObstacle = newObstacle;
            EditorUtility.SetDirty(this);
        }
        public void SetMovementLocations(string direction)
        {
            if (movementDirections != null)
            {
                Undo.RecordObject(this, "Remove movement direction");
                movementDirections.Add(direction);
                EditorUtility.SetDirty(this);
            }
            
        }
        
        public void SetConnection(Directions direction)
        {
            bool connection;
            Undo.RecordObject(this, "Remove connection");
            if (!movementDirections.Contains(direction.ToString()))
            {
                movementDirections.Add(direction.ToString());
                connection = true;
            }
            else
            {
                movementDirections.Remove(direction.ToString());
                connection = false;
            }

            switch (direction)
            {
                case Directions.North:
                    northConnection = connection;
                    break;
                case Directions.South:
                    southConnection = connection;
                    break;
                case Directions.East:
                    eastConnection = connection;
                    break;
                case Directions.West:
                    westConnection = connection;
                    break;
                case Directions.NorthEast:
                    northEastSouthWestConnection = connection;
                    break;
                case Directions.SouthEast:
                    southEastNorthWestConnection = connection;
                    break;
                case Directions.NorthWest:
                    northWestSouthEastConnection = connection;
                    break;
                case Directions.SouthWest:
                    southWestNorthEastConnection = connection;
                    break;
            }

            EditorUtility.SetDirty(this);
        }
#endif
    }
}
