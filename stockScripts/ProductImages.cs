using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProductImages : MonoBehaviour
{

    public Image defaultImage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Image getImage(string productName)
    {
        return defaultImage;
    }
}
