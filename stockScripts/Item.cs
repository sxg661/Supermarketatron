using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    public enum Tag { FANCY, TACKY, CHEAP, EVERYDAY, VEGETERIAN,
        HEALTHY, SWEET, EXOTIC, CONVINIENT, UNHEALTHY, FUN }
    public enum Type { FOOD, BEVERAGE, HOUSEHOLD, KIDS, SPORTING, ELECTRONIC, CLOTHING}

    public List<Tag> tags = new List<Tag>();
    public Type itemType;
    public string name;
    public string descrip = "";
    public double wholesalePrice;
    public double recRetailPrice;

    public Item(Type type, string name, double wholesalePrice, double recRetailPrice)
    {
        this.itemType = type;
        this.name = name;
        this.wholesalePrice = wholesalePrice;
        this.recRetailPrice = recRetailPrice;
    }

    public void addTag(Tag tag)
    {
        tags.Add(tag);
    }

}


