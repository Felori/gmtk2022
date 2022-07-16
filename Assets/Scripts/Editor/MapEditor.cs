using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameMap))]
public class MapEditor : Editor
{
    enum PaintMode
    {
        PLACE,
        ERASE
    }

    bool editMode = false;
    PaintMode paintMode = PaintMode.PLACE;

    static TilePallette pallette;
    GameTile selectedTile = null;

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

            paintMode = (PaintMode)EditorGUILayout.EnumPopup("Paint Mode", paintMode);


            if (paintMode == PaintMode.PLACE)
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
                    if(paintMode == PaintMode.PLACE)
                    {
                        if (selectedTile) map.PlaceTile(selectedTile, x, y);
                    }
                    if(paintMode == PaintMode.ERASE)
                    {
                        map.RemoveTile(x, y);
                    }
                }

                map.EditModeCursor.transform.position = new Vector3(x, 0.01f, y);
            }
        }
    }

    private void OnDestroy()
    {
        ((GameMap)target).EditModeCursor.SetActive(false);
    }
}
