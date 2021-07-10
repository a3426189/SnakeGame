using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    private Vector3 gridPos;
    [SerializeField]
    private float timeMax;

    private float Move_timecounter = 0f;

    private int snakeSize;
    private List<Vector3> SnakeMovePositionList;
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
        SnakeMovePositionList = new List<Vector3>();
        SnakeBodyPartList = new List<SnakeBodyPart>();
        transform.position = new Vector3(0f, 0f, 0);
        snakeSize = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        gridPos = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && gridPos.y != -1)
        {
            gridPos = new Vector3(0, 1, 0);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && gridPos.y != 1)
        {
            gridPos = new Vector3(0, -1, 0);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && gridPos.x != 1)
        {
            gridPos = new Vector3(-1, 0, 0);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && gridPos.x != -1)
        {
            gridPos = new Vector3(1, 0, 0);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 270.0f);
        }
        //auto moving
        if (Move_timecounter < timeMax)
        {
            Move_timecounter += Time.deltaTime;
            //Debug.Log(Move_timecounter);
        }
        else
        {
            Move_timecounter -= timeMax;
            Vector3 Temp = transform.position;

            bool SnakeAteFood = levelGrid.SnakeTryEatFood(transform.position);
            if (SnakeAteFood)
            {
                snakeSize += 1;
                Debug.Log(snakeSize);
                CreateSnakeBodyPart();
            }

            SnakeMovePositionList.Insert(0, transform.position);

            if (SnakeMovePositionList.Count >= snakeSize + 1)
            {
                SnakeMovePositionList.RemoveAt(SnakeMovePositionList.Count - 1);
            }
            
            transform.position += gridPos;

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
            SnakeBodyPartList[i].setGridPosition(SnakeMovePositionList[i]);
        }
    }

    public List<Vector3> GetSnakePositionList()
    {
        return this.SnakeMovePositionList;
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

        public void setGridPosition(Vector3 position)
        {
            transform.position = position;
        }


    }


}
