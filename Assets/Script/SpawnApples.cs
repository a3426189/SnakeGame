using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnApples : MonoBehaviour
{
    public GameObject Apple;
    [SerializeField]
    private float SpawnTimeMax;
    [SerializeField]
    private int MinRandValue;
    [SerializeField]
    private int MaxRandValue;

    private float SpawnTime;

    private int RandVector3_X, RandVector3_Y;
    private Vector3 RandVector3;

    public List<Vector3> list = new List<Vector3>();
    GameObject ApplePrefabs;
    public int points = 0;
    //private SpriteRenderer FoodApple;
    void Start()
    {
        
        RandVector3_X = Random.Range(MinRandValue, MaxRandValue);
        RandVector3_Y = Random.Range(MinRandValue, MaxRandValue);
        RandVector3 = new Vector3(RandVector3_X, RandVector3_Y, 0);
        ApplePrefabs = Instantiate(Apple, RandVector3, Quaternion.identity);
    }

    void Update()
    {
         
        if (ApplePrefabs.gameObject == null)
        {
            points += 1;
            Debug.Log(points);
            RandVector3_X = Random.Range(MinRandValue, MaxRandValue);
            RandVector3_Y = Random.Range(MinRandValue, MaxRandValue);
            RandVector3 = new Vector3(RandVector3_X, RandVector3_Y, 0);
            ApplePrefabs = Instantiate(Apple, RandVector3, Quaternion.identity);
        }

        
    }
    /*void RandomIntWithOutRepeat()
    {
        while (list.Contains(RandVector3))
        {
            RandVector3_X = Random.Range(MinRandValue, MaxRandValue);
            RandVector3_Y = Random.Range(MinRandValue, MaxRandValue);
            RandVector3 = new Vector3(RandVector3_X, RandVector3_Y, 0);
        }
        if (!list.Contains(RandVector3))
        {
            list.Add(RandVector3);
        }
        //Vector3 RandVector3 = new Vector3(RandVector3_X, RandVector3_Y, 0);
    }*/
    
    
}
