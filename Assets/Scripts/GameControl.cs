using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField] LevelGenerator levelGenerator;

    private TileType[,] tiles;
    private SpriteRenderer[,] tileSpriteRenderers;
    public PlayerControl player;
    private int level = 1;

    private void Start()
    {
        LoadLevel(LevelData.level1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadLevelNumber(level);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    private void LoadLevelNumber(int _num)
    {
        if (_num == 1) NewLevel(LevelData.level1);
        else if (_num == 2) NewLevel(LevelData.level2);
        else if (_num == 3) NewLevel(LevelData.level3);
        else Debug.Log("No levels above 3!");
    }

    private void LoadLevel(TileType[,] _tiles)
    {
        tiles = (TileType[,])_tiles.Clone();
        tileSpriteRenderers = levelGenerator.GenerateRoom(tiles, this);
        UpdateTileSprites();
    }

    public bool CanMove(Vector2Int _currentPosition, Vector2Int _direction, bool _canPush = true, bool _block = false)
    {
        Vector2Int checkTilePosition = _currentPosition + _direction;
        TileType checkTile = tiles[checkTilePosition.y, checkTilePosition.x];
        if (checkTile == TileType.Wall)
        {
            return false;
        }
        if (_block && checkTile == TileType.End)
        {
            return false;
        }
        if (checkTile == TileType.Block)
        {
            if (!_canPush) return false;
            return CanMove(checkTilePosition, _direction, true, true);
        }
        return true;
    }

    public void MoveTo(Vector2Int _newPosition, Vector2Int _direction, bool _player = true)
    {
        TileType newTile = tiles[_newPosition.y, _newPosition.x];
        if (newTile == TileType.Block)
        {
            tiles[_newPosition.y, _newPosition.x] = TileType.Floor;
            MoveTo(_newPosition + _direction, _direction, false);
            UpdateTileSprites();
        }
        else if (newTile == TileType.End)
        {
            //win level
            level++;
            if (level == 4) level = 1;
            LoadLevelNumber(level);
        }
        else if (newTile == TileType.Hole)
        {
            if (_player)
            {
                player.Hole();
            } 
            else
            {
                //fill hole
                tiles[_newPosition.y, _newPosition.x] = TileType.Filled;
                return;
            }
        }
        if (!_player) tiles[_newPosition.y, _newPosition.x] = TileType.Block;
    }

    private void UpdateTileSprites()
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                tileSpriteRenderers[i, j].sprite = levelGenerator.GetSprite(tiles[i, j]);
            }
        }
    }

    private void NewLevel(TileType[,] _tiles)
    {
        Destroy(player.gameObject);
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                Destroy(tileSpriteRenderers[i, j].gameObject);
            }
        }
        LoadLevel(_tiles);
    } 

    public Vector2Int WorldPositionToTilePosition(Vector2 _position)
    {
        Vector2 tilePosition = _position - levelGenerator.GetBottomLeftPosition();
        return Vector2Int.RoundToInt(tilePosition);
    }
}
