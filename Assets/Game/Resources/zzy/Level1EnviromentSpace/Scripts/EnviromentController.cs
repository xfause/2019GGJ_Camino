using UnityEngine;
using System.Collections;


public class EnviromentController : MonoBehaviour
{
    [Header("图速度:")]
    public float BackgroundSpeed = 0.4f;

    [Header("X位置")]
    public float moveVectorX = -23.27f;

    public bool IsReStart = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveTexture();
    }

    public void MoveTexture()
    {
        IsReStart = false;
        transform.localPosition -= new Vector3(BackgroundSpeed, 0, 0);
        if (transform.localPosition.x <= moveVectorX)
        {
            transform.localPosition = new Vector3(transform.localPosition.x - moveVectorX, 
                transform.localPosition.y, transform.localPosition.z);
            IsReStart = true;
        }
        
    }

}
