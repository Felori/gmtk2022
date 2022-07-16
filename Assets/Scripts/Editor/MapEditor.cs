using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameMap))]
public class MapEditor : Editor
{
    enum ToolMode
    {
        PLACE,
        ERASE,
        ROTATE
    }

    enum ObjectType
    {
        TILE,
        FEATURE
    }

    bool editMode = false;
    ToolMode toolMode = ToolMode.PLACE;
    ObjectType objectType = ObjectType.TILE;

    static TilePallette pallette;
    GameTile selectedTile = null;
    Feature selectedFeature = null;

    static Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameMap map = (GameMap)target;

        if(GUILayout.Button("Toggle Edit Mode " + (editMode? "Off" : "On")))
        {
            ToggleEditMode(!editMode);
        }

        if(editMode)
        {
            pallette = (TilePallette)EditorGUILayout.ObjectField("Tile Pallette", pallette, typeof(TilePallette), false);

            toolMode = (ToolMode)EditorGUILayout.EnumPopup("Tool Mode", toolMode);

            objectType = (ObjectType)EditorGUILayout.EnumPopup("Object Type", objectType);

            if (toolMode == ToolMode.PLACE)
            {
                if(objectType == ObjectType.TILE)
                {
                    GUILayout.Label("Selected Tile: " + (selectedTile ? selectedTile.gameObject.name : "None"));

                    if (pallette != null)
                    {
                        foreach (GameTile tile in pallette.Tiles)
                        {
                            if (GUILayout.Button(tile.gameObject.name))
                            {
                                selectedTile = tile;
                            }
                        }
                    }
                }

                if(objectType == ObjectType.FEATURE)
                {
                    GUILayout.Label("Selected Feature: " + (selectedFeature ? selectedFeature.name : "None"));

                    if (pallette != null)
                    {
                        foreach (Feature feature in pallette.Features)
                        {
                            if (GUILayout.Button(feature.gameObject.name))
                            {
                                selectedFeature = feature;
                            }
                        }
                    }
                }
            }
        }
    }

    void ToggleEditMode(bool enabled)
    {
        editMode = enabled;

        GameMap map = (GameMap)target;

        map.EditModeCursor.SetActive(enabled);
    }

    private void OnSceneGUI()
    {
        if(editMode)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            GameMap map = (GameMap)target;

            Vector2 mousePosition = Event.current.mousePosition;
            Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
            if(groundPlane.Raycast(ray, out float dist))
            {
                Vector3 point = ray.GetPoint(dist);
                int x = Mathf.RoundToInt(point.x);
                int y = Mathf.RoundToInt(point.z);

                if(Event.current.type == EventType.MouseDown && Event.current.button == 0)
                {
                    if(toolMode == ToolMode.PLACE)
                    {
                        if(objectType == ObjectType.TILE && selectedTile)
                            map.PlaceTile(selectedTile, x, y);

                        if (objectType == ObjectType.FEATURE && selectedFeature)
                            map.GetTile(x, y)?.PlaceFeature(selectedFeature);
                    }

                    if(toolMode == ToolMode.ERASE)
                    {
                        if(objectType == ObjectType.TILE)
                            map.RemoveTile(x, y);

                        if (objectType == ObjectType.FEATURE)
                            map.GetTile(x, y)?.RemoveFeature();
                    }

                    if(toolMode == ToolMode.ROTATE)
                    {
                        map.GetTile(x, y)?.Rotate();
                    }

                    EditorUtility.SetDirty(map);
                }

                map.EditModeCursor.transform.position = new Vector3(x, 0.05f, y);
            }
        }
    }

    private void OnDestroy()
    {
        ((GameMap)target).EditModeCursor.SetActive(false);
    }
}
