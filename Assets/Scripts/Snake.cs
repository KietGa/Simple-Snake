using CodeMonkey;
using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    public AudioSource[] bgSound;
    public AudioSource eatSound;
    public AudioSource dieSound;
    private enum Direction
    {
        Left, Right, Up, Down
    }

    private enum State
    {
        Alive, Dead
    }

    private State state; 
    private Vector2Int gridPosition;
    private Direction gridMoveDirection;
    private float gridMoveTimer;
    [SerializeField] private float gridMoveTimerMax;
    [SerializeField] private Text speedText;
    private float moveSpeed;
    private LevelGrid levelGrid;
    private int snakeBodySize;
    private List<SnakeMovePosition> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartList;
    private int soundList;

    public void Setup(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }
    private void Awake()
    {
        gridPosition = new Vector2Int(0,0);
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = Direction.Right;

        snakeMovePositionList = new List<SnakeMovePosition>();
        snakeBodySize = 0;

        snakeBodyPartList = new List<SnakeBodyPart>();
        state = State.Alive;

        soundList = Random.Range(0, 5);
        bgSound[soundList].Play();

        moveSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = moveSpeed.ToString();
        switch(state)
        {
            case State.Alive:
                HandleInput();
                HandleGridMovement();
                break;
            case State.Dead:
                break;
        }
    }

    public void ChangeMusic()
    {
        bgSound[soundList].Stop();
        soundList = Random.Range(0, 5);
        bgSound[soundList].Play();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gridMoveDirection != Direction.Up)
            {
                gridMoveDirection = Direction.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gridMoveDirection != Direction.Down)
            {
                gridMoveDirection = Direction.Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gridMoveDirection != Direction.Right)
            {
                gridMoveDirection = Direction.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gridMoveDirection != Direction.Left)
            {
                gridMoveDirection = Direction.Right;
            }
        }
    }

    private void HandleGridMovement()
    {
        gridMoveTimer += Time.deltaTime;
        float countdown = gridMoveTimerMax / moveSpeed;
        if (gridMoveTimer >= countdown)
        {
            gridMoveTimer -= countdown;
            SnakeMovePosition previousSnakeMovePosition = null;
            if (snakeMovePositionList.Count > 0) 
            {
                previousSnakeMovePosition = snakeMovePositionList[0];
            }
            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, gridMoveDirection);
            snakeMovePositionList.Insert(0, snakeMovePosition);
            Vector2Int gridMoveDirectionVector;
            switch (gridMoveDirection)
            {
                default:
                case Direction.Up: gridMoveDirectionVector = new Vector2Int(0,1); break;
                case Direction.Down: gridMoveDirectionVector = new Vector2Int(0, -1); break;
                case Direction.Left: gridMoveDirectionVector = new Vector2Int(-1, 0); break;
                case Direction.Right: gridMoveDirectionVector = new Vector2Int(1, 0); break;
            }
            gridPosition += gridMoveDirectionVector;

            gridPosition = levelGrid.ValidateGridPosition(gridPosition);

            bool snakeAteFood = levelGrid.TrySnakeEatFood(gridPosition);

            if (snakeAteFood)
            {
                snakeBodySize++;
                moveSpeed += 0.02f;
                CreateSnakeBodyPart();
                eatSound.Play();
            }

            if (snakeMovePositionList.Count >= snakeBodySize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }

            UpdateSnakeBodyPart();

            foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList) 
            {
                Vector2Int snakeBodyPartGridPosition = snakeBodyPart.GetGridPosition();
                if (gridPosition == snakeBodyPartGridPosition)
                {
                    dieSound.Play();
                    state = State.Dead;
                    bgSound[soundList].Stop(); 
                    GameHandler.GameOver();
                } 
            }

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector) - 90);

            UpdateSnakeBodyPart();
        }
    }
    
    private void CreateSnakeBodyPart()
    {
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));
    }

    private void UpdateSnakeBodyPart()
    {
        for (int i = 0; i < snakeMovePositionList.Count; i++)
        {
            snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
        }
    }

    private float GetAngleFromVector(Vector2Int dir) 
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return n;
    }

    public List<Vector2Int> GetFullSnakeGridPosition()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList)
        {
            gridPositionList.Add(snakeMovePosition.GetGridPosition());
        }
        return gridPositionList;
    }

    private class SnakeBodyPart
    {
        private SnakeMovePosition snakeMovePosition;
        private Transform transform;
        public SnakeBodyPart(int bodyIndex) 
        {
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAsset.instance.snakeBodySprite;
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            transform = snakeBodyGameObject.transform;
        }

        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition)
        {
            this.snakeMovePosition = snakeMovePosition;
            transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);
            float angle;
            switch (snakeMovePosition.GetDirection())
            {
                default:
                case Direction.Up:
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 0; break;
                        case Direction.Left:
                            angle = 0 + 45; break;
                        case Direction.Right:
                            angle = 0 - 45; break;
                    }
                    break;
                case Direction.Down:
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 180; break;
                        case Direction.Left:
                            angle = 180 + 45; break;
                        case Direction.Right:
                            angle = 180 - 45; break;
                    }
                    break;
                case Direction.Left:
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = -90; break;
                        case Direction.Down:
                            angle = -45; break;
                        case Direction.Up:
                            angle = 45; break;
                    }
                    break;
                case Direction.Right: 
                    switch(snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 90; break;
                        case Direction.Down:
                            angle = 45; break;
                        case Direction.Up:
                            angle = -45; break;
                    }
                    break;
            }
            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        public Vector2Int GetGridPosition()
        {
            return snakeMovePosition.GetGridPosition();
        }
    }

    private class SnakeMovePosition
    {
        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int gridPosition;
        private Direction direction; 
         
        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition, Direction direction)
        {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.gridPosition = gridPosition;
            this.direction = direction;
        }

        public Vector2Int GetGridPosition()
        {
            return gridPosition;
        }

        public Direction GetDirection()
        {
            return direction;
        }

        public Direction GetPreviousDirection()
        {
            if (previousSnakeMovePosition == null)
            {
                return Direction.Right;
            }
            else
            {
                return previousSnakeMovePosition.direction;
            }
        }
    }
}
