using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBlack : MonoBehaviour
{
    [HideInInspector]
    public CanvasGroup cGroup;
    [HideInInspector]
    public bool inProgress = true;

    public float fadeSpeed = 5;//control by loadScene script
    public float InternalWaitTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        cGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Fade(Action finalCallBack, params Action[] callback)
    {
        inProgress = true;
        while (cGroup.alpha < 0.95f)//From transport to black
        {
            cGroup.alpha = Mathf.Lerp(cGroup.alpha, 1, Time.deltaTime * fadeSpeed);
            yield return null;
        }
        //show UI form black to transport
        if (callback != null)//Do some thing when the panel is black
        {
            foreach (Action action in callback)
            {
                action.Invoke();//Do you want to 
            }
        }
        //finalCallBack.Invoke();
        inProgress = false;
        cGroup.alpha = 1;
        yield return new WaitForSeconds(InternalWaitTime);//wait seconds when black

        while (cGroup.alpha > 0.05f)//From black to transport
        {
            cGroup.alpha = Mathf.Lerp(cGroup.alpha, 0, Time.deltaTime * fadeSpeed);
            yield return null;
        }
        cGroup.alpha = 0;

        Destroy(transform.parent.gameObject);
        

        //inProgress = false;
    }
}
