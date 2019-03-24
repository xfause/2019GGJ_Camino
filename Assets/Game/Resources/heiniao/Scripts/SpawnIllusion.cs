using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIllusion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(0,0,0.1f);
        GameObject cons = GameObject.Instantiate<GameObject>(Illusion, transform.position, transform.rotation, null);
        //cons.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.Translate(0, 0, -0.1f);

    }

    public GameObject Illusion;

    [Header("尾巴的左移速度")]
    public float TailsSpeed= 0.05f;
    [Header("主体尾长，可以根据数值不要最细的尾巴,0为尾巴留到最细，1为不留尾巴，不能大于初始尾宽")]
    public float TailsLong=0f;
    [Header("每个影子的宽度缩减")]
    public float ScaleChangePerFrame=0.05f;
    [Header("初始尾宽，可以大于1")]
    public float TailsWidth=0.8f;
    [Header("每个影子的透明度减少量，0为不减，1为1帧直接不透明")]
    public float AlphaDownPerFrame=0.02f;


}
