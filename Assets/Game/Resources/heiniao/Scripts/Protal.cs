using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Protal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(0,DropSpeedWhenProtal*probe.EndSeconds*60, 0);
    }

    public CharacterBehavior characterBehavior;
    public Probe probe;
    public bool ProbeTime = false;
    public bool ProtalTime = false;


    float HorizontalSpeed =0;
    float DropSpeedWhenProtal = 0.05f;

    // Update is called once per frame
    void Update()
    {
        HorizontalSpeed = characterBehavior.RunSpeed;
        ProbeTime = probe.ProbeTime;
        ProtalTime = probe.ProtalTime;
        if (ProbeTime) transform.Translate(HorizontalSpeed,-DropSpeedWhenProtal, 0);
        else if(characterBehavior.ControlFlag)transform.Translate(HorizontalSpeed, 0, 0);


        if (characterBehavior.transform.position.x >= transform.position.x)
            EndLevel();
    }

    void EndLevel()
    {
        //Debug.Log("终于结束了");//在这里跳转关卡
        SceneManager.LoadScene("LevelEnd");
    }


}
