using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;

public class LevelGrid 
{
    private Vector2Int foodGridPosition;
    private GameObject foodGameObject;
    private int width;
    private int height;
    private Snake snake;

    public void Setup(Snake snake)
    {
        this.snake = snake;
        SpawnFood();
    }

    public LevelGrid(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    private void SpawnFood()
    {
        do {
            foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPosition().IndexOf(foodGridPosition) != -1);

        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAsset.instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
    }

    public bool TrySnakeEatFood(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == foodGridPosition)
        {
            Object.Destroy(foodGameObject);
            SpawnFood();
            Score.AddScore();
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
    {
        if (gridPosition.x < -width + 1)
        {
            gridPosition.x = width - 1;
        }
        if (gridPosition.x > width - 1)
        {
            gridPosition.x = -width + 1;
        }
        if (gridPosition.y < -height + 1)
        {
            gridPosition.y = height - 1;
        }
        if (gridPosition.y > height - 1)
        {
            gridPosition.y = -height + 1;
        }
        return gridPosition;
    }
}
