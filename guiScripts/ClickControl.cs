using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickControl : MonoBehaviour {

    bool hover = false;

    public UnityEvent onClick;


    // Update is called once per frame
    void Update()
    {
        //Debug.Log("action is now : " + onClick);
        if (Input.GetMouseButtonDown(0) && hover)
        {
            if(onClick != null)
            {
                onClick.Invoke();
            }
        }
    }

    private void OnMouseOver()
    {
        hover = true;
    }

    private void OnMouseExit()
    {
        hover = false;
    }
}
