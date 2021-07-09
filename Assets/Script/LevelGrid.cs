using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    private Vector3 foodGridPosition;
    GameObject AppleGO;
    private int width, height;
    private SnakeScript snakeScript;
    public LevelGrid(int width,int height)
    {
        this.width = width;
        this.height = height;
        
    }
    public void GetSnakeScript(SnakeScript snakeScript)
    {
        this.snakeScript = snakeScript;
        SpawnFood();
    }
    private void SpawnFood()
    {
        AppleGO = new GameObject();
        Vector3 snakePosition = snakeScript.GetPosition();

        do { foodGridPosition = new Vector3(Random.Range(-width, width), Random.Range(-height, height), 0); }//can't be same position with the snake
        while (foodGridPosition == snakePosition);

        AppleGO.AddComponent<SpriteRenderer>();
        AppleGO.GetComponent<SpriteRenderer>().sprite = GameAssets.i.FoodAppleSprite;

        AppleGO.transform.position = foodGridPosition;
        //Instantiate(AppleGO, foodGridPosition, Quaternion.identity);
    }
    public void snakeMoved(Vector3 vector3)
    {
        //Debug.Log(vector3);
        //Debug.Log(AppleGO.transform.position);
        if (vector3 == AppleGO.transform.position)
        {
            GameObject.Destroy(AppleGO);
            SpawnFood();
        }
    }
}
