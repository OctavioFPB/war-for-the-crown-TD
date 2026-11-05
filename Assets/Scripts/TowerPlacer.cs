using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;  
using UnityEngine.Tilemaps;

///placing function TEMPORARY PLACEHOLDER

///1 = placemode
///placemode+mouseleftclick = place
///esc or placemode+mouserightclick = stopplacemode
///mouserightclick=removeclickedtower

public class TowerPlacer : MonoBehaviour
{
    [Header("Scene refs")]
    [SerializeField] private Tilemap groundTilemap;     
    [SerializeField] private Camera cam;                

    [Header("Tower")]
    [SerializeField] private GameObject towerPrefab;

    [Header("Rules")]
    [Tooltip("Tiles whose asset name starts with any of these can NOT be built on (road_*).")]
    [SerializeField] private string[] blockedTilePrefixes = { "road" };

    [Header("Ghost tint")]
    [SerializeField] private Color validColor   = new Color(0.4f, 1f, 0.4f, 0.65f);
    [SerializeField] private Color invalidColor = new Color(1f, 0.3f, 0.3f, 0.65f);

    private readonly Dictionary<Vector3Int, GameObject> towers = new();
    private GameObject ghost;
    private SpriteRenderer[] ghostRenderers;

    void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    void Update()
    {
        var kb = Keyboard.current;
        var mouse = Mouse.current;
        if (kb == null || mouse == null) return;

        if (kb.digit1Key.wasPressedThisFrame) StartPlacing();

        Vector3Int cell = MouseToCell(mouse.position.ReadValue());

        if (ghost != null)
        {
            bool valid = CanPlace(cell);
            ghost.transform.position = groundTilemap.GetCellCenterWorld(cell);
            ApplySorting(ghost, cell);
            Tint(valid ? validColor : invalidColor);

            bool overUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
            if (mouse.leftButton.wasPressedThisFrame && valid && !overUI)
                Place(cell);

            if (mouse.rightButton.wasPressedThisFrame || kb.escapeKey.wasPressedThisFrame)
                CancelPlacing();
        }
        else if (mouse.rightButton.wasPressedThisFrame)
        {
            Remove(cell);
        }
    }


    void StartPlacing()
    {
        if (ghost != null || towerPrefab == null) return;

        ghost = Instantiate(towerPrefab);
        ghost.name = "TowerGhost";

        foreach (var mb in ghost.GetComponentsInChildren<MonoBehaviour>()) mb.enabled = false;
        foreach (var c in ghost.GetComponentsInChildren<Collider2D>()) c.enabled = false;

        ghostRenderers = ghost.GetComponentsInChildren<SpriteRenderer>();
    }

    void CancelPlacing()
    {
        Destroy(ghost);
        ghost = null;
    }

    void Place(Vector3Int cell)
    {
        var tower = Instantiate(towerPrefab, groundTilemap.GetCellCenterWorld(cell), Quaternion.identity);
        ApplySorting(tower, cell);
        towers[cell] = tower;
    }

    void Remove(Vector3Int cell)
    {
        if (!towers.TryGetValue(cell, out var tower)) return;
        towers.Remove(cell);
        Destroy(tower);
    }

    bool CanPlace(Vector3Int cell)
    {
        if (!groundTilemap.HasTile(cell)) return false;          
        if (towers.ContainsKey(cell)) return false;              

        string tileName = groundTilemap.GetTile(cell).name;
        foreach (var prefix in blockedTilePrefixes)
            if (tileName.StartsWith(prefix, System.StringComparison.OrdinalIgnoreCase))
                return false;
        return true;
    }

    Vector3Int MouseToCell(Vector2 screenPos)
    {
        Vector3 world = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, -cam.transform.position.z));
        world.z = 0f;
        return groundTilemap.WorldToCell(world);
    }

    void ApplySorting(GameObject go, Vector3Int cell)
    {
        int order = 1000 - (cell.x + cell.y);
        foreach (var sr in go.GetComponentsInChildren<SpriteRenderer>())
            sr.sortingOrder = order;
    }

    void Tint(Color c)
    {
        foreach (var sr in ghostRenderers) sr.color = c;
    }
}


///adjust into something closer to planned project TEMPORARY PLACEHOLDER