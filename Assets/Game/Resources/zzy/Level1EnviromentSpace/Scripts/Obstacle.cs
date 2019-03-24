using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    public float DestroyTime = 20f;

    //障碍速度，同地面速度
    [HideInInspector]
    public  float MoveSpeed;

    // Use this for initialization
    void Start()
    {

        GameObject.Destroy(gameObject, DestroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        MoveTexture();
    }

    public void MoveTexture()
    {
        
        transform.localPosition -= new Vector3(MoveSpeed, 0, 0);


    }
}
