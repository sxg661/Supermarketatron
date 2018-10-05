using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProductImages : MonoBehaviour
{
    private Dictionary<string, Image> images = new Dictionary<string, Image>();

    public Image defaultImage;

    public Image cake;
    public Image ball;
    public Image sausage;
    public Image banana;
    public Image egg;
    public Image fizzypop;
    public Image candle;
    public Image pizza;
    public Image nuts;

    // Use this for initialization
    void Start()
    {
        images["cake"] = cake;
        images["ball"] = ball;
        images["sausage"] = sausage;
        images["banana"] = banana;
        images["egg"] = egg;
        images["fizzy pop"] = fizzypop;
        images["candle"] = candle;
        images["pizza"] = pizza;
        images["nuts"] = nuts;
    }

    public Image getImage(string productName)
    {
        if (images.ContainsKey(productName))
        {
            return images[productName];
        }
        return defaultImage;
    }
}
