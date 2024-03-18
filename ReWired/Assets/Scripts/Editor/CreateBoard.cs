using UnityEngine;
using UnityEditor;

public class BoardCreator : Editor
{
    [MenuItem("GameObject/Presets/GameBoard")]
    static void CreateGameBoard()
    {
        GameObject floorTilePrefab = LoadFloorTilePrefab("FloorTile");
        GameObject wallTilePrefab = LoadFloorTilePrefab("WallTile");

        if (floorTilePrefab == null)
        {
            Debug.LogError("FloorTile prefab not found. Make sure it exists in the specified resource folder.");
            return;
        }

        GameObject board = CreateBoardGameObject();

        CreateTiles(board, floorTilePrefab, wallTilePrefab);

        RegisterUndoForBoard(board);
    }

    // Load the FloorTile prefab from the specified resource folder
    static GameObject LoadFloorTilePrefab(string tile)
    {
        return Resources.Load<GameObject>(tile);
    }

    // Create the board GameObject
    static GameObject CreateBoardGameObject()
    {
        return new GameObject("Board");
    }

    // Create tiles as children of the board
    static void CreateTiles(GameObject board, GameObject floorTilePrefab, GameObject wallTilePrefab)
    {
        for (int i = -9; i < 10; i++)
        {
            for (int j = -5; j < 6; j++)
            {
                AdjustTileProperties(board, i, j, floorTilePrefab, wallTilePrefab);
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
    static void AdjustTileProperties(GameObject board, int i, int j, GameObject floorTilePrefab, GameObject wallTilePrefab)
    {
        if (IsEdgeTile(i, j))
        {
            GameObject tile = InstantiateFloorTile(wallTilePrefab, board.transform, new Vector3(i, j, 0));
            tile.tag = "Wall";
        }
        else
        {
            GameObject tile = InstantiateFloorTile(floorTilePrefab, board.transform, new Vector3(i, j, 0));
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