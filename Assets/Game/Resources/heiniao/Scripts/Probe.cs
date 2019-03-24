using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameFrames = GameSeconds * 60;
        EndFrames = EndSeconds * 60;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) Application.Quit();//退出游戏
        counter++;
        if (counter == GameFrames) { obstacleController.Flag = false; ProbeTime = true; }
        if (counter == GameFrames + EndFrames) { characterBehavior.ControlFlag = false; ProtalTime = true; ProbeTime = false; }

    }

    public ObstacleController obstacleController;//用于禁止生成障碍物
    public CharacterBehavior characterBehavior;//用于禁止输入
    [HideInInspector]
    public bool ProbeTime = false;
    [HideInInspector]
    public bool ProtalTime = false;

    [Header("多少秒后禁止生成障碍物")]
    public int GameSeconds = 25;
    int GameFrames = 1500;

    [Header("探针多少秒后禁止输入")]
    public int EndSeconds = 10;
    int EndFrames = 600;

    int counter = 0;//每帧++




}
