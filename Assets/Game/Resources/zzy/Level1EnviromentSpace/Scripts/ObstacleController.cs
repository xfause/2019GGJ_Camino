using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleController : MonoBehaviour
{
    [Header("障碍物物体列表")]
    public List<GameObject> ObstacleList;

    [Header("障碍物物体列表概率")]
    public List<float> ObstaclePerList;

    [Header("生成物体的随机缩放：最小值")]
    public float randScale_Min = 0.9001f;

    [Header("生成物体的随机缩放：最大值")]
    public float randScale_Max = 1.5f;

    [Header("障碍物体GameObject")]
    private GameObject ObstacleObj;

    [Header("单次生成总数")]
    public int RandomNum = 5;

    public List<GameObject> tempList;


    [Header("生成地面的概率")]
    public float LandPercent = 0.5f;

    [Header("Y贴地面的高度范围")]
    [Header("Y地面的最小值：")]
    public float YLandMin = 3.85f;

    [Header("Y地面的最大值：")]
    public float YLandMax = 4.28f;

    [Header("Y浮空的高度范围")]

    [Header("Y浮空的最小值：")]
    public float YAirMin = 6.66f;

    [Header("Y浮空的最大值：")]
    public float YAirMax = 8.23f;

    [Header("预制件的大小")]
    [Header("浮空件的长度")]
    public float XValue = 4.5f;
    [Header("浮空件的宽度")]
    public float YValue = 2.5f;

    [Header("****************************************************")]

    //X轴的最小随机位置
    public float XPositionMin = 0f;
    //X轴的最大随机位置
    public float XpositionMax = 25.4f;

    //地面场景
    public EnviromentController LandEnviroment;
    //********************************************************************************
    //最小Y
    private float MinY;
    //最大Y
    private float MaxY;

    public float RandomY = 1f;
    public float RandomX = 1f;
    // Use this for initialization
    void Start()
    {
        InsAllObstacle();

    }
    public bool Flag = true;
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonUp(0))
        //{
        //    InsAllObstacle();
        //}

        if (countTime < NextTime)
        {
            countTime += Time.deltaTime;

        }
        else
        {
            XPositionMin = 0f;
            countTime = 0;
            InsAllObstacle();
        }
    }

    public float NextTime = 2f;

    private float countTime = 0;


    public void InsAllObstacle()
    {
        for (int i = 0; i < RandomNum; i++)
        {
            ObstacleObj = RandObstacleList();
            ObstacleObj.transform.localScale = new Vector3(1, 1, 1)
                * Random.Range(randScale_Min, randScale_Max);
            if (Flag)
                tempList.Add(InsOneRandomObstacle());
        }
    }

    public GameObject RandObstacleList()
    {
        float PerCount = 0;
        GameObject TempObj = null;
        for (int i = 0; i < ObstaclePerList.Count; i++)
        {
            PerCount += ObstaclePerList[i];
        }


        float tempObstaclePer = Random.Range(0.0001f, PerCount);

        if (ObstaclePerList.Count == 0) return null;//防止空值

        float tempSum = ObstaclePerList[0];

        for (int i = 0; i < ObstaclePerList.Count; i++)
        {
            if (tempObstaclePer < tempSum)//临时时候
            {
                TempObj = ObstacleList[i];
                break;
            }

            if (i + 1 < ObstaclePerList.Count)
            {

                tempSum += ObstaclePerList[i + 1];
            }
            else
            {
                TempObj = ObstacleList[ObstaclePerList.Count - 1];
                break;
            }
        }

        return TempObj;
    }

    //public string landOrAir  ;
    public GameObject InsOneRandomObstacle()
    {
        GameObject InsObstacle = Instantiate<GameObject>(ObstacleObj, transform);
        RandHeight();
        RandomPosition(XPositionMin);
        InsObstacle.transform.localPosition = new Vector3(RandomX, RandomY, 0);
        InsObstacle.GetComponent<Obstacle>().MoveSpeed = LandEnviroment.BackgroundSpeed;
        return InsObstacle;
    }

    /// <summary>
    /// 随机位置X
    /// </summary>
    public void RandomPosition(float nextXPosition)
    {
        RandomX = Random.Range(nextXPosition + 0.0001f, ((XpositionMax - XPositionMin) / RandomNum) + nextXPosition);
        XPositionMin = Random.Range(RandomX + XValue + 3f, RandomX + XValue * 2f + 3f);
    }

    /// <summary>
    /// 随机位置Y
    /// </summary>
    public void RandHeight()
    {
        float tempPercent = Random.Range(0.0001f, 1);
        if (tempPercent <= LandPercent) //出现地面的概率
        {
            MinY = YLandMin;
            MaxY = YLandMax;
            //landOrAir = "Land";
        }
        else
        {
            MinY = YAirMin;
            MaxY = YAirMax;
            //landOrAir = "Air";
        }


        RandomY = Random.Range(MinY, MaxY);




        //RandomXPosition = 
    }


}
