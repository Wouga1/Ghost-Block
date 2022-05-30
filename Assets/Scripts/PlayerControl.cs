using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float tileSize = 1;
    private GameControl gameControl;
    private SpriteRenderer spriteRenderer;
    private Sprite ghostSprite;

    private Vector2Int currentPosition;
    private bool dead = false;

    public void Setup(GameControl _gameControl, Sprite _ghostSprite, Vector2Int _currentPosition, float _tileSize)
    {
        gameControl = _gameControl;
        ghostSprite = _ghostSprite;
        currentPosition = _currentPosition;
        tileSize = _tileSize;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2Int direction;
        if (Input.GetKeyDown(KeyCode.UpArrow)) direction = Vector2Int.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow)) direction = Vector2Int.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) direction = Vector2Int.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow)) direction = Vector2Int.right;
        else return;

        if (gameControl.CanMove(currentPosition, direction, !dead))
        {
            transform.position += (Vector3)(Vector2)direction * tileSize;
            currentPosition += direction;
            gameControl.MoveTo(currentPosition, direction);
        }
    }

    public void Hole()
    {
        if (dead) return;

        dead = true;
        spriteRenderer.sprite = ghostSprite;
    }
}
