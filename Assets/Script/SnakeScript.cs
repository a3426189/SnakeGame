using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    enum Direction
    {
        Up,
        Down,
        Right,
        Left,
    }
    private Vector3 MoveDirectionVector3;

    [SerializeField]
    private float timeMax;
    private float Move_timecounter = 0f;
    
    private int snakeSize;
    private List<SnakeMovePosition> SnakeMovePositionList;
    private Direction direction;
    private List<SnakeBodyPart> SnakeBodyPartList;

    //private List<SnakeBodyPart> SnakeBodyPartList;
    private int point = 0; 

    private LevelGrid levelGrid;

    public void SnakeGetLevelGrid(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }
    private void Awake()
    {
        SnakeMovePositionList = new List<SnakeMovePosition>();
        SnakeBodyPartList = new List<SnakeBodyPart>();
        transform.position = new Vector3(0f, 0f, 0);
        snakeSize = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        MoveDirectionVector3 = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        HandleGirdMovement();
        //auto moving
        if (Move_timecounter < timeMax)
        {
            Move_timecounter += Time.deltaTime;
            //Debug.Log(Move_timecounter);
        }
        else
        {
            HandleGirdMovement();
            Move_timecounter -= timeMax;
            Vector3 Temp = transform.position;

            bool SnakeAteFood = levelGrid.SnakeTryEatFood(transform.position);
            if (SnakeAteFood)
            {
                snakeSize += 1;
                Debug.Log(snakeSize);
                CreateSnakeBodyPart();
            }
            SnakeMovePosition PreviousSnakeMovePosition = null;
            if (SnakeMovePositionList.Count > 0)
            {
                PreviousSnakeMovePosition = SnakeMovePositionList[0];
            }
            SnakeMovePositionList.Insert(0, new SnakeMovePosition(PreviousSnakeMovePosition, transform.position,direction));

            if (SnakeMovePositionList.Count >= snakeSize + 1)
            {
                SnakeMovePositionList.RemoveAt(SnakeMovePositionList.Count - 1);
            }
            
            transform.position += MoveDirectionVector3;

            if (CheckSnakeMoveCorrectly(this.transform))
            {
                transform.position = Temp;
            }

            UpdateSnakeBodyPart();
            //SnakeMoved(transform.position);
        }

    }/*
    private void CreateSnakeBodySprites()
    {
        GameObject SnakeBodyGameObject = new GameObject("snakebody",typeof(SpriteRenderer));
        SnakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.SnakeBodySprite;
        SnakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -SnakeBodyTransformList.Count;
        SnakeBodyTransformList.Add(SnakeBodyGameObject.transform);
    }*/
    private void HandleDirection()
    {
        switch (direction)
        {
            case (Direction.Up):
                MoveDirectionVector3 = new Vector3(0, 1, 0);
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                break;
            case (Direction.Down):
                MoveDirectionVector3 = new Vector3(0, -1, 0);
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                break;
            case (Direction.Left):
                MoveDirectionVector3 = new Vector3(-1, 0, 0);
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                break;
            case (Direction.Right):
                MoveDirectionVector3 = new Vector3(1, 0, 0);
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 270.0f);
                break;
        }
    }
    private void HandleGirdMovement()
    {
        if (Input.GetKey(KeyCode.UpArrow) && direction != Direction.Down)
        {
            direction = Direction.Up;
            HandleDirection();
            return;
        }
        if (Input.GetKey(KeyCode.DownArrow) && direction != Direction.Up)
        {
            direction = Direction.Down;
            HandleDirection();
            return;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && direction != Direction.Right)
        {
            direction = Direction.Left;
            HandleDirection();
            return;
        }
        if (Input.GetKey(KeyCode.RightArrow) && direction != Direction.Left)
        {
            direction = Direction.Right;
            HandleDirection();
            return;
        }
    }
    private bool CheckSnakeMoveCorrectly(Transform transform)
    {
        if (transform.position.x > 10 || transform.position.x < -10 || transform.position.y > 10 || transform.position.y < -10)
        {
            return true;
        }
        return false;
    }
    private void CreateSnakeBodyPart()
    {
        SnakeBodyPartList.Add(new SnakeBodyPart(SnakeBodyPartList.Count));
    }
    private void UpdateSnakeBodyPart()
    {
        for (int i = 0; i < SnakeBodyPartList.Count; i++)
        {
            SnakeBodyPartList[i].SetSnakeMovePosition(SnakeMovePositionList[i]);
        }
    }

    public List<Vector3> GetSnakePositionList()
    {
        List<Vector3> List = new List<Vector3>();
        for (int i = 0; i < SnakeMovePositionList.Count; i++)
        {
            List.Add(SnakeMovePositionList[i].GetGridPosition());
        }
        return List;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private class SnakeBodyPart {
        private Transform transform;

        public SnakeBodyPart(int bodyIndex)
        {
            GameObject SnakeBodyGameObject = new GameObject("snakebody", typeof(SpriteRenderer));
            SnakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.SnakeBodySprite;
            SnakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            transform = SnakeBodyGameObject.transform;
        }
        
        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition)
        {
            transform.position = snakeMovePosition.GetGridPosition();
            switch (snakeMovePosition.GetDirection())
            {
                case (Direction.Up):
                    switch (snakeMovePosition.GetPreviousSnakeDirection())
                    {
                        case (Direction.Left):
                            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 45.0f);
                            break;
                        case (Direction.Right):
                            transform.rotation = Quaternion.Euler(0.0f, 0.0f, -45.0f);
                            break;
                        default:
                            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                            break;
                    }
                    break;
                case (Direction.Down):
                    switch (snakeMovePosition.GetPreviousSnakeDirection())
                    {
                        case (Direction.Left):
                            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 135.0f);
                            break;
                        case (Direction.Right):
                            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 225.0f);
                            break;
                        default:
                            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                            break;
                    }
                    break;
                case (Direction.Left):
                    switch (snakeMovePosition.GetPreviousSnakeDirection())
                    {
                        case (Direction.Up):
                            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 45.0f);
                            break;
                        case (Direction.Down):
                            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 135.0f);
                            break;
                        default:
                            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                            break;
                    }
                    
                    break;
                case (Direction.Right):
                    switch (snakeMovePosition.GetPreviousSnakeDirection())
                    {
                        case (Direction.Up):
                            transform.rotation = Quaternion.Euler(0.0f, 0.0f, -45.0f);
                            break;
                        case (Direction.Down):
                            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 235.0f);
                            break;
                        default:
                            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 270.0f);
                            break;
                    }
                    
                    break;
            }
            
        }
    }
    private class SnakeMovePosition {
        private SnakeMovePosition PreviousSnakeMovePosition;
        private Vector3 GridPosition;
        private Direction direction;

        public SnakeMovePosition (SnakeMovePosition PreviousSnakeMovePosition,Vector3 GridPosition,Direction direction)
        {
            this.PreviousSnakeMovePosition = PreviousSnakeMovePosition;
            this.GridPosition = GridPosition;
            this.direction = direction;
        }
        public Vector3 GetGridPosition()
        {
            return this.GridPosition;
        }
        public Direction GetDirection()
        {
            return this.direction;
        }
        public Direction GetPreviousSnakeDirection()
        {
            if (this.PreviousSnakeMovePosition == null)
            {
                return Direction.Right;
            }
            else {
                return this.PreviousSnakeMovePosition.direction;
            }
        }
    }

}
