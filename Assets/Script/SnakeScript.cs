using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    private Vector3 gridPos;
    [SerializeField]
    private float timeMax;

    private float Move_timecounter = 0f;

    private LevelGrid levelGrid;
    public void SnakeGetLevelGrid(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }
    private void Awake()
    {
        transform.position = new Vector3(0f, 0f, 0);
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
            transform.position += gridPos;

            if (transform.position.x > 10 || transform.position.x < -10 || transform.position.y > 10 || transform.position.y < -10)
            {
                transform.position = Temp;
            }
            levelGrid.snakeMoved(transform.position);
            //SnakeMoved(transform.position);
        }

    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
