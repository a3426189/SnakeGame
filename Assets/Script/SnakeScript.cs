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
    private List<Transform> SnakeBodyTransformList;
    private int point = 0; 

    private LevelGrid levelGrid;

    public void SnakeGetLevelGrid(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }
    private void Awake()
    {
        SnakeMovePositionList = new List<Vector3>();
        SnakeBodyTransformList = new List<Transform>();
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
                CreateSnakeBodySprites();
            }

            SnakeMovePositionList.Insert(0, transform.position);

            if (SnakeMovePositionList.Count >= snakeSize + 1)
            {
                SnakeMovePositionList.RemoveAt(SnakeMovePositionList.Count - 1);
            }
            
            transform.position += gridPos;

            if (transform.position.x > 10 || transform.position.x < -10 || transform.position.y > 10 || transform.position.y < -10) //不能超出邊界
            {
                transform.position = Temp;
            }

            
            

            for (int i = 0; i < SnakeBodyTransformList.Count; i++)
            {
                SnakeBodyTransformList[i].position = SnakeMovePositionList[i];
            }

            

            //SnakeMoved(transform.position);
        }

    }
    private void CreateSnakeBodySprites()
    {
        GameObject SnakeBodyGameObject = new GameObject("snakebody",typeof(SpriteRenderer));
        SnakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.SnakeBodySprite;
        SnakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -SnakeBodyTransformList.Count;
        SnakeBodyTransformList.Add(SnakeBodyGameObject.transform);
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
        private Vector3 gridposition;
        //private Transform transform;

        public SnakeBodyPart(int bodyIndex)
        {
            GameObject SnakeBodyGameObject = new GameObject("snakebody", typeof(SpriteRenderer));
            SnakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.SnakeBodySprite;
            SnakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            //transform = SnakeBodyGameObject.transform;
        }
        public void setGridPosition(Vector3 position)
        {
            this.gridposition = position;

        }


    }


}
