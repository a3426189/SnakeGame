using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SnakeScript snake;
    public LevelGrid levelGrid;
    void Start()
    {
        levelGrid = new LevelGrid(10,10);
        snake.SnakeGetLevelGrid(levelGrid);
        levelGrid.GetSnakeScript(snake);
        Debug.Log("start!");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
