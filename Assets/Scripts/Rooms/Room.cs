using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace RPG.Rooms
{
    [CreateAssetMenu(fileName = "New Room", menuName = "Room")]
    public class Room : ScriptableObject, ISerializationCallbackReceiver
    {
       
        [SerializeField] List<RoomLocation> locations = new List<RoomLocation>();
        [SerializeField] Vector2 newNodeOffset = new Vector2(0,0);
        

        Dictionary<string, RoomLocation> locationLookup = new Dictionary<string, RoomLocation>();

#if UNITY_EDITOR
        private void Awake()
        {

        }
#endif
        private void OnValidate()
        {
            locationLookup.Clear();
            foreach (RoomLocation location in GetAllLocations())
            {
                locationLookup[location.name] = location;
            }
        }

        public IEnumerable<RoomLocation> GetAllLocations()
        {
            return locations;
        }
        public RoomLocation GetRootNode()
        {
            return locations[0];
        }
        public IEnumerable<RoomLocation> GetAllLocations(RoomLocation parentNode)
        {
            foreach (string childID in parentNode.GetLocation())
            {
                if (locationLookup.ContainsKey(childID))
                {
                    yield return locationLookup[childID];
                }
            }
        }
        public IEnumerable<RoomLocation> GetPlayerChildren(RoomLocation currentNode)
        {
            foreach (RoomLocation node in GetAllLocations(currentNode))
            {
                if (node.IsLocationChild())
                {
                    yield return node;
                }
            }
        }
        public IEnumerable<RoomLocation> GetAIChildren(RoomLocation currentLocation)
        {
            foreach (RoomLocation location in GetAllLocations(currentLocation))
            {
                if (!location.IsLocationChild())
                {
                    yield return location;
                }
            }
        }
#if UNITY_EDITOR
        public void CreateLocation(RoomLocation parent, string location)
        {
            //Need to go through this line by line to see if duplicate directions can be prevented...
            RoomLocation childNode = MakeNode(parent, location);
            Undo.RegisterCreatedObjectUndo(childNode, "Created Dialogue Node");
            Undo.RecordObject(this, "Added Dialogue Node");
            AddNode(childNode);
        }

        private void ChildNodeFunctions(string location, RoomLocation childNode)
        {            
            switch (location)
            {
                case "South":
                    childNode.SetSouthConnection();
                    NewNodeOffset(0f, -300f);
                    break;
                case "West":
                    childNode.SetWestConnection();
                    NewNodeOffset(600f, 0f);
                    break;
                case "North":
                    childNode.SetNorthConnection();
                    NewNodeOffset(0f, 300f);
                    break;
                case "East":
                    childNode.SetEastConnection(); 
                    NewNodeOffset(-600f, 0f);
                    break;
                case "NorthEast":
                    childNode.SetNorthEastSouthWestConnection();
                    NewNodeOffset(-400f, 200f);
                    break;
                case "NorthWest":
                    childNode.SetNorthWestSouthEastConnection();
                    NewNodeOffset(400f, 200f);
                    break;
                case "SouthEast":
                    childNode.SetSouthEastNorthWestConnection();
                    NewNodeOffset(-400f, -200f);
                    break;
                case "SouthWest":
                    childNode.SetSouthWestNorthEastConnection();
                    NewNodeOffset(400f, -200f);
                    break;
                default:
                    break;
            }
        }

        private void AddNode(RoomLocation childNode)
        {
            locations.Add(childNode);
            OnValidate();
        }
        private Vector2 NewNodeOffset(float x, float y)
        {
            newNodeOffset.x = x;
            newNodeOffset.y = y;
            return newNodeOffset;
        }
        private RoomLocation MakeNode(RoomLocation parent, string location)
        {
            RoomLocation childNode = CreateInstance<RoomLocation>();
            ChildNodeFunctions(location, childNode);
            childNode.name = Guid.NewGuid().ToString();
            if (parent != null)
            {
                parent.AddChild(childNode.name);
                //could potentially put the backwards connection here...
                childNode.AddChild(parent.name);
                //childNode.SetLocationChild(!parent.IsLocationChild()); 
                childNode.SetPosition(parent.GetRect().position + newNodeOffset);
 
            }

            return childNode;
        }

        public void DeleteNode(RoomLocation nodeToDelete)
        {
            Undo.RecordObject(this, "Delete Node");
            locations.Remove(nodeToDelete);
            OnValidate();
            CleanDanglingChildren(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);

        }

        private void CleanDanglingChildren(RoomLocation nodeToDelete)
        {
            foreach (RoomLocation node in GetAllLocations())
            {
                node.RemoveChild(nodeToDelete.name);
            }
        }
#endif

        public void OnBeforeSerialize()
        {
            if (locations.Count == 0)
            {
                RoomLocation childNode = MakeNode(null, null);
                childNode.MovementDirections().Clear();
                AddNode(childNode);
            }
            if (AssetDatabase.GetAssetPath(this) != "")
            {
                foreach (RoomLocation node in GetAllLocations())
                {
                    if (AssetDatabase.GetAssetPath(node) == "")
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }
                }
            }
        }

        public void OnAfterDeserialize()
        {

        }
    }
}
