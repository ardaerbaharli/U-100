using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helpers
{
    private static Camera _camera;


    private static PointerEventData _eventDataCurrentPosition;
    private static List<RaycastResult> _results;

    public static Camera Camera
    {
        get
        {
            if (_camera == null) _camera = Camera.main;
            return _camera;
        }
    }

    public static bool IsCursorOverUI()
    {
        _eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        _results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
        return _results.Count > 0;
    }

    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera, out var result);
        return result;
    }

    public static Hashtable Hash(params object[] args)
    {
        var hashTable = new Hashtable(args.Length / 2);
        if (args.Length % 2 != 0)
        {
            Debug.LogError("Tween Error: Hash requires an even number of arguments!");
            return null;
        }

        var i = 0;
        while (i < args.Length - 1)
        {
            hashTable.Add(args[i], args[i + 1]);
            i += 2;
        }

        return hashTable;
    }
}