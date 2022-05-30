using UnityEngine;

public enum TileType
{
    Floor,
    Wall,
    Hole,
    Block,
    Start,
    End,
    Filled
}

public class LevelGenerator : MonoBehaviour
{
    [Header("Size")]
    [SerializeField] private Vector2 midPosition = new Vector2(0, 0);
    [SerializeField] private float screenWidth = 32;
    [SerializeField] private float screenHeight = 20;
    [SerializeField] private float tileSize = 1;

    [Header("Prefabs")]
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject playerPrefab;

    [Header("Sprites")]
    [SerializeField] private Sprite floorSprite;
    [SerializeField] private Sprite wallSprite;
    [SerializeField] private Sprite holeSprite;
    [SerializeField] private Sprite blockSprite;
    [SerializeField] private Sprite startSprite;
    [SerializeField] private Sprite endSprite;
    [SerializeField] private Sprite filledSprite;

    [SerializeField] private Sprite playerGhostSprite;

    public Vector2 GetBottomLeftPosition()
    {
        return new Vector2(midPosition.x - screenWidth / 2 + tileSize / 2, midPosition.y - screenHeight / 2 + tileSize / 2);
    }
        
    public SpriteRenderer[,] GenerateRoom(TileType[,] _tiles, GameControl _gameControl)
    {
        if (_tiles.GetLength(0) != screenHeight/tileSize || _tiles.GetLength(1) != screenWidth/tileSize)
        {
            Debug.Log("TileType[,] passed into GenerateRoom was not the correct size!");
            return null;
        }

        SpriteRenderer[,] tileSpriteRenderers = new SpriteRenderer[(int)(screenHeight/tileSize), (int)(screenWidth/tileSize)];

        for (int i = 0; i < screenHeight/tileSize; i++)
        {
            for (int j = 0; j < screenWidth/tileSize; j++)
            {
                Vector2 tilePosition = GetBottomLeftPosition() + new Vector2(j * tileSize, i * tileSize);
                tileSpriteRenderers[i, j] = Instantiate(tilePrefab, tilePosition, Quaternion.identity).GetComponent<SpriteRenderer>();
                if (_tiles[i, j] == TileType.Start)
                {
                    PlayerControl player = Instantiate(playerPrefab, tilePosition, Quaternion.identity).GetComponent<PlayerControl>();
                    player.Setup(_gameControl, playerGhostSprite, new Vector2Int(j, i), tileSize);
                    _gameControl.player = player;
                }
            }
        }
        return tileSpriteRenderers;
    }

    public Sprite GetSprite(TileType _type)
    {
        switch (_type)
        {
            case TileType.Floor:
                return floorSprite;
            case TileType.Wall:
                return wallSprite;
            case TileType.Hole:
                return holeSprite;
            case TileType.Block:
                return blockSprite;
            case TileType.Start:
                return startSprite;
            case TileType.End:
                return endSprite;
            case TileType.Filled:
                return filledSprite;
            default:
                return floorSprite;
        }
    }
}
