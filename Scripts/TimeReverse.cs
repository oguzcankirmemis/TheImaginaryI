using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeReverse : MonoBehaviour
{
    private Stack<TRObject> objectsOnStack = new Stack<TRObject>();

    private ITR otherScript;
    void Start()
    {
        otherScript = (ITR)gameObject.GetComponent(typeof(ITR));
    }
    void FixedUpdate()
    {
        if (Input.GetButton("TimeControl"))
        {
            if (objectsOnStack.Count > 0)
                otherScript.LoadTRObject(objectsOnStack.Pop());
        }
        else
            otherScript.SaveTRObject();
    }

    public void PushTRObject(TRObject trobject)
    {
        objectsOnStack.Push(trobject);
    }
}
