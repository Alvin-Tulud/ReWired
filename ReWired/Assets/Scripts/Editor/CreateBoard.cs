using UnityEngine;
using UnityEditor;

public class BoardCreator : Editor
{
    [MenuItem("GameObject/Presets/GameBoard")]
    static void CreateGameBoard()
    {
        GameObject floorTilePrefab = LoadFloorTilePrefab();

        if (floorTilePrefab == null)
        {
            Debug.LogError("FloorTile prefab not found. Make sure it exists in the specified resource folder.");
            return;
        }

        GameObject board = CreateBoardGameObject();
        Color evenTileColor = Color.black;
        Color oddTileColor = Color.white;

        CreateTiles(board, floorTilePrefab, evenTileColor, oddTileColor);

        RegisterUndoForBoard(board);
    }

    // Load the FloorTile prefab from the specified resource folder
    static GameObject LoadFloorTilePrefab()
    {
        return Resources.Load<GameObject>("FloorTile");
    }

    // Create the board GameObject
    static GameObject CreateBoardGameObject()
    {
        return new GameObject("Board");
    }

    // Create tiles as children of the board
    static void CreateTiles(GameObject board, GameObject floorTilePrefab, Color evenColor, Color oddColor)
    {
        for (int i = -9; i < 10; i++)
        {
            for (int j = -5; j < 6; j++)
            {
                GameObject tile = InstantiateFloorTile(floorTilePrefab, board.transform, new Vector3(i, j, 0));
                AdjustTileProperties(tile, i, j, evenColor, oddColor);
            }
        }
    }

    // Instantiate the floor tile prefab
    static GameObject InstantiateFloorTile(GameObject floorTilePrefab, Transform parent, Vector3 position)
    {
        GameObject tile = PrefabUtility.InstantiatePrefab(floorTilePrefab) as GameObject;
        tile.transform.parent = parent;
        tile.transform.localPosition = position;
        SceneVisibilityManager.instance.DisablePicking(tile, false);
        return tile;
    }

    // Adjust properties of each tile based on position and index
    static void AdjustTileProperties(GameObject tile, int i, int j, Color evenColor, Color oddColor)
    {
        if (IsEdgeTile(i, j))
        {
            tile.GetComponent<SpriteRenderer>().color = evenColor;
            tile.tag = "Wall";
        }
        else
        {
            tile.GetComponent<SpriteRenderer>().color = oddColor;
            tile.tag = "Floor";
        }
    }

    // Check if the tile is on the edge of the board
    static bool IsEdgeTile(int i, int j)
    {
        return i == -9 || i == 9 || j == -5 || j == 5;
    }

    // Register the board for Undo
    static void RegisterUndoForBoard(GameObject board)
    {
        Undo.RegisterCreatedObjectUndo(board, "Create Board");
    }
}