using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float tileSize = 1;
    [SerializeField] GameControl gameControl;

    Vector2Int currentPosition;

    private void Start()
    {
        currentPosition = gameControl.WorldPositionToTilePosition(transform.position);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Vector2Int direction = Vector2Int.up;
            if (gameControl.CanMove(currentPosition, direction))
            {
                transform.position += (Vector3)(Vector2)direction * tileSize;
                currentPosition += direction;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int direction = Vector2Int.down;
            if (gameControl.CanMove(currentPosition, direction))
            {
                transform.position += (Vector3)(Vector2)direction * tileSize;
                currentPosition += direction;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int direction = Vector2Int.left;
            if (gameControl.CanMove(currentPosition, direction))
            {
                transform.position += (Vector3)(Vector2)direction * tileSize;
                currentPosition += direction;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int direction = Vector2Int.right;
            if (gameControl.CanMove(currentPosition, direction))
            {
                transform.position += (Vector3)(Vector2)direction * tileSize;
                currentPosition += direction;
            }
        }
    }
}
