using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateScript : MonoBehaviour {

    public int frameRate;

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = frameRate;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("FR : " + 1.0f / Time.deltaTime);
	}
}
