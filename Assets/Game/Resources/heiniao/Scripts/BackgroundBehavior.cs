using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehavior : MonoBehaviour
{


    public float BackgroundHeight = 0f;
    public float DealtaHeight = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundHeight += DealtaHeight;
    }

}
