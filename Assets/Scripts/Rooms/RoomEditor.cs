using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;

namespace RPG.Rooms.Editor
{
    public class RoomEditor : EditorWindow
    {
        Room selectedRoom = null;
        //Directions directions;
        Vector2 scrollPosition;
        [NonSerialized] GUIStyle locationStyle;
        [NonSerialized] string movementLocation;
        //[NonSerialized] string buttonDirectionPressed;


        [NonSerialized] GUIStyle playerNodeStyle;
        [NonSerialized] RoomLocation draggingLocation;
        [NonSerialized] Vector2 draggingOffset;
        [NonSerialized] RoomLocation creatingLocation = null;
        [NonSerialized] RoomLocation deletingNode = null;
        [NonSerialized] RoomLocation linkingParentNode = null;
        [NonSerialized] bool dragginCanvas = false;
        [NonSerialized] Vector2 draggingCanvasOffset;

        const float canvasSize = 4000f;
        const float backgroundSize = 50f;

        [MenuItem ("Window/Room Editor")]

        
        public static void ShowEditorWindow()
        {
            EditorWindow.GetWindow(typeof(RoomEditor), false, "Room Editor");
        }
        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            Room room = EditorUtility.InstanceIDToObject(instanceID) as Room;
            if (room != null)
            {
                ShowEditorWindow();
                return true;
            }
            return false;
        }
        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChange;
            locationStyle = new GUIStyle();
            locationStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            locationStyle.normal.textColor = Color.white;
            locationStyle.padding = new RectOffset(20, 20, 20, 20);
            locationStyle.border = new RectOffset(12, 12, 12, 12);

            //Player Node Style
            playerNodeStyle = new GUIStyle();
            playerNodeStyle.normal.background = EditorGUIUtility.Load("node4") as Texture2D;
            playerNodeStyle.normal.textColor = Color.magenta; //doesnt do much
            playerNodeStyle.padding = new RectOffset(20, 20, 20, 20);
            playerNodeStyle.border = new RectOffset(12, 12, 12, 12);

            
        }
        private void OnSelectionChange()
        {
            Room newRoom = Selection.activeObject as Room;
            if (newRoom != null)
            {
                selectedRoom = newRoom;
                Repaint();
            }
        }
        private void OnGUI()
        {
            if (selectedRoom == null)
            {
                EditorGUILayout.LabelField("No Room Selected");
            }
            else
            {
                ProcessEvents();

                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
                Rect canvas = GUILayoutUtility.GetRect(canvasSize, canvasSize);
                Texture2D backgroundTex = Resources.Load("background") as Texture2D;
                Rect texCoords = new Rect(0, 0, canvasSize / backgroundSize, canvasSize / backgroundSize);
                GUI.DrawTextureWithTexCoords(canvas, backgroundTex, texCoords);
                foreach (RoomLocation location in selectedRoom.GetAllLocations())
                {
                    DrawConnections(location);
 
                    
                }
                foreach (RoomLocation location in selectedRoom.GetAllLocations())
                {
                    DrawLocation(location);
                }

                EditorGUILayout.EndScrollView();
                if (creatingLocation != null)
                {
                    selectedRoom.CreateLocation(creatingLocation, movementLocation);
                    creatingLocation = null;
                    movementLocation = null;
                }
                if (deletingNode != null)
                {

                    selectedRoom.DeleteNode(deletingNode);
                    deletingNode = null;
                    movementLocation = null;
                }
            }
        }
        private void ProcessEvents()
        {
            if (Event.current.type == EventType.MouseDown && draggingLocation == null)
            {
                draggingLocation = GetNodeAtPoint(Event.current.mousePosition + scrollPosition);
                if (draggingLocation != null)
                {
                    draggingOffset = draggingLocation.GetRect().position - Event.current.mousePosition;
                    Selection.activeObject = draggingLocation;
                }
                else
                {
                    dragginCanvas = true;
                    draggingCanvasOffset = Event.current.mousePosition + scrollPosition;
                    Selection.activeObject = selectedRoom;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingLocation != null)
            {

                draggingLocation.SetPosition(Event.current.mousePosition + draggingOffset);


                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseDrag && dragginCanvas)
            {
                scrollPosition = draggingCanvasOffset - Event.current.mousePosition;
                GUI.changed = true;

            }
            else if (Event.current.type == EventType.MouseUp && draggingLocation != null)
            {
                draggingLocation = null;
            }
            else if (Event.current.type == EventType.MouseDrag && dragginCanvas)
            {
                dragginCanvas = false;
            }
        }
        private void DrawLocation(RoomLocation location)
        {
            GUIStyle style = locationStyle;
            // Revisit this maybe usefull for things like actions on a location
            if (location.IsLocationChild())
            {
                    style = playerNodeStyle;
            }

            GUILayout.BeginArea(location.GetRect(), style);

            //location.SetText(EditorGUILayout.TextArea(location.GetText(), GUILayout.ExpandHeight(true))); //Textbox location for location text

            GUILayout.BeginHorizontal();
            if (location.MovementDirections().IndexOf(Directions.NorthWest.ToString()) < 0)
            {
                if (GUILayout.Button(Directions.NorthWest.ToString()))
                {
                    creatingLocation = location;
                    location.SetConnection(Directions.NorthWest);
                    movementLocation = Directions.SouthEast.ToString();
                    
                }
            }
            if (location.MovementDirections().IndexOf(Directions.North.ToString()) < 0)
            {
                if (GUILayout.Button(Directions.North.ToString()))
                {
                    creatingLocation = location;
                    location.SetConnection(Directions.North);
                    movementLocation = Directions.South.ToString();
                }
            }
            if (location.MovementDirections().IndexOf(Directions.NorthEast.ToString()) < 0)
            {
                if (GUILayout.Button(Directions.NorthEast.ToString()))
                {
                    creatingLocation = location;
                    location.SetConnection(Directions.NorthEast);
                    movementLocation = Directions.SouthWest.ToString();
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (location.MovementDirections().IndexOf(Directions.West.ToString()) < 0 && !location.GetWestConnection())
            {
                if (GUILayout.Button(Directions.West.ToString()))
                {
                    creatingLocation = location;
                    location.SetConnection(Directions.West);
                    movementLocation = Directions.East.ToString();
                }
            }
            if (GUILayout.Button("Delete Location"))
            {
                deletingNode = location;
            }
            if (location.MovementDirections().IndexOf(Directions.East.ToString()) < 0)
            {
                if (GUILayout.Button(Directions.East.ToString()))
                {
                    creatingLocation = location;
                    location.SetConnection(Directions.East);
                    movementLocation = Directions.West.ToString();                    
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (location.MovementDirections().IndexOf(Directions.SouthWest.ToString()) < 0)
            {
                if (GUILayout.Button(Directions.SouthWest.ToString()))
                {
                    creatingLocation = location;
                    location.SetConnection(Directions.SouthWest);
                    movementLocation = Directions.NorthEast.ToString();
                }
            }
            if (location.MovementDirections().IndexOf(Directions.South.ToString()) < 0)
            {
                if (GUILayout.Button(Directions.South.ToString()))
                {
                    creatingLocation = location;
                    location.SetConnection(Directions.South);
                    movementLocation = Directions.North.ToString();
                }
            }
            if (location.MovementDirections().IndexOf(Directions.SouthEast.ToString()) < 0)
            {
                if (GUILayout.Button(Directions.SouthEast.ToString()))
                {
                    creatingLocation = location;
                    location.SetConnection(Directions.SouthEast);
                    movementLocation = Directions.NorthWest.ToString();
                }
            }       

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            DrawLinkingDirections(location);

            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            location.SetEntrance(GUILayout.Toggle(location.IsEntrance(), "Is Entance"));
            location.SetExit(GUILayout.Toggle(location.IsExit(), "Is Exit"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            location.SetTraps(GUILayout.Toggle(location.IsTrapped(), "Is Trapped"));
            location.SetObstacles(GUILayout.Toggle(location.HasObstacle(), "Has Obstacles"));
            location.SetNPC(GUILayout.Toggle(location.HasNPC(), "Has NPC"));

            GUILayout.EndHorizontal();
            GUILayout.EndArea();

            
        }
        private void DrawLinkingDirections(RoomLocation location)
        {
            
            if (linkingParentNode == null)
            {
                if (GUILayout.Button("Link Locations"))
                {
                    linkingParentNode = location;
                }

            }
            else if (linkingParentNode == location)
            {
                if(GUILayout.Button("Cancel"))
                {
                    linkingParentNode = null;
                }
            }
            else if (linkingParentNode.GetLocation().Contains(location.name))
            {
                if(GUILayout.Button("Unlink"))
                {
                    linkingParentNode.RemoveChild(location.name);
                    //Add in line to remove direction from the list
                    linkingParentNode = null;
                }
            }
            else 
            {
                if (GUILayout.Button("Move Here"))
                {
                    linkingParentNode.AddChild(location.name);
                    //Add direction to list of movement locations
                    
                    linkingParentNode = null;
                }
            }

        }
    
        private void DrawConnections(RoomLocation location)
        {
            Vector3 startPoint = new Vector2(location.GetRect().center.x, location.GetRect().center.y);

            
            foreach (RoomLocation locations in selectedRoom.GetAllLocations(location))
            {
                Vector3 endPoint = new Vector2(locations.GetRect().center.x, locations.GetRect().center.y);
                
                Vector3 controlPointOffset = endPoint - startPoint;
                controlPointOffset.y = 0;
                controlPointOffset.x *= 0.8f;

                Handles.DrawBezier(startPoint, endPoint, startPoint + controlPointOffset, endPoint - controlPointOffset, Color.yellow, null, 4f);
                
                if (endPoint.x < startPoint.x)
                {
                    Handles.DrawBezier(startPoint, endPoint, startPoint + controlPointOffset, endPoint - controlPointOffset, Color.magenta, null, 4f);
                }
                if (endPoint.y > startPoint.y)
                {
                    Handles.DrawBezier(startPoint, endPoint, startPoint + controlPointOffset, endPoint - controlPointOffset, Color.green, null, 4f);
                }
            }
        }
       

        private RoomLocation GetNodeAtPoint(Vector2 point)
        {
            RoomLocation foundNode = null;
            foreach (RoomLocation location in selectedRoom.GetAllLocations())
            {
                if (location.GetRect().Contains(point))
                {
                    foundNode = location;
                }
            }
            return foundNode;
        }
    }
}
