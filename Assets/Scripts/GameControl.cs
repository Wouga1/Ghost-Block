using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField] LevelGenerator levelGenerator;

    private TileType[,] tiles;

    private void Start()
    {
        tiles = new TileType[20, 32];
        tiles[10, 12] = TileType.Wall;
        levelGenerator.GenerateRoom(tiles);
    }

    public bool CanMove(Vector2Int _currentPosition, Vector2Int _direction, bool _canPush = true)
    {
        Vector2Int checkTilePosition = _currentPosition + _direction;
        TileType checkTile = tiles[checkTilePosition.y, checkTilePosition.x];
        if (checkTile == TileType.Wall)
        {
            return false;
        }
        if (checkTile == TileType.Block)
        {
            if (!_canPush) return false;
            return CanMove(checkTilePosition, _direction);
        }
        return true;
    }

    public Vector2Int WorldPositionToTilePosition(Vector2 _position)
    {
        Vector2 tilePosition = _position - levelGenerator.GetBottomLeftPosition();
        return Vector2Int.RoundToInt(tilePosition);
    }
}
