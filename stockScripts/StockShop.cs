using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockShop : MonoBehaviour {

    public static List<string> prodNames = new List<string>();

    public static int numOfProducts = 0;

    public static Dictionary<string, Item> products = fillStockShop();

    public static Optional<Item> getProd(int id)
    {
        if (id < numOfProducts)
        {
            return Optional<Item>.of(products[ prodNames[id] ]);
        }
        return Optional<Item>.empty();
    }

    private static void addProd(string name, Item product, Dictionary<string, Item> prods)
    {
        prods[name] = product;
        prodNames.Add(name);
        numOfProducts++;
    }

    public static Dictionary<string, Item> fillStockShop()
    {
        Dictionary<string, Item> prods = new Dictionary<string, Item>();

        //Eggs
        Item egg = new Item(Item.Type.FOOD, "egg", 0.6, 1.0);
        egg.descrip = ("6 chicken eggs");
        egg.addTag(Item.Tag.EVERYDAY);
        egg.addTag(Item.Tag.VEGETERIAN);
        egg.addTag(Item.Tag.HEALTHY);
        egg.addTag(Item.Tag.CHEAP);
        addProd("egg", egg, prods);

        //Cake
        Item cake = new Item(Item.Type.FOOD, "cake", 1.5, 5.0);
        cake.descrip = "A packaged victoria sponge with strawberry jam and butter cream! mmm";
        cake.addTag(Item.Tag.CONVINIENT);
        cake.addTag(Item.Tag.SWEET);
        cake.addTag(Item.Tag.VEGETERIAN);
        cake.addTag(Item.Tag.FANCY);
        cake.addTag(Item.Tag.FUN);
        addProd("cake", cake, prods);

        //Banana
        Item banana = new Item(Item.Type.FOOD, "banana", 0.1, 0.3);
        banana.descrip = "A single banana";
        banana.addTag(Item.Tag.HEALTHY);
        banana.addTag(Item.Tag.VEGETERIAN);
        banana.addTag(Item.Tag.EVERYDAY);
        banana.addTag(Item.Tag.CHEAP);
        banana.addTag(Item.Tag.CONVINIENT);
        addProd("banana", banana, prods);

        //Sausage
        Item sausage = new Item(Item.Type.FOOD, "sausage", 0.75, 1.5);
        sausage.descrip = "A meat sausage of dubious origin...";
        sausage.addTag(Item.Tag.CHEAP);
        sausage.addTag(Item.Tag.CONVINIENT);
        sausage.addTag(Item.Tag.UNHEALTHY);
        sausage.addTag(Item.Tag.TACKY);
        addProd("sausage", sausage, prods);

        //Ball
        Item ball = new Item(Item.Type.KIDS, "ball", 0.5, 1.5);
        ball.descrip = "A bouncy ball in lots of fun colours!";
        ball.addTag(Item.Tag.TACKY);
        ball.addTag(Item.Tag.CHEAP);
        ball.addTag(Item.Tag.FUN);
        addProd("ball", ball, prods);

        //Candle
        Item candle = new Item(Item.Type.HOUSEHOLD, "candle", 7.0, 15.0);
        candle.descrip = "A high quality candle with the tropical scents of the Carribean";
        candle.addTag(Item.Tag.FANCY);
        candle.addTag(Item.Tag.EXOTIC);
        addProd("candle", candle, prods);

        //Fizzy Pop
        Item fizzyPop = new Item(Item.Type.HOUSEHOLD, "fizzy pop", 0.1, 0.5);
        candle.descrip = "A fizzing bottle of teeth rotting suggary pop!";
        candle.addTag(Item.Tag.UNHEALTHY);
        candle.addTag(Item.Tag.FUN);
        candle.addTag(Item.Tag.SWEET);
        candle.addTag(Item.Tag.CHEAP);
        candle.addTag(Item.Tag.TACKY);
        addProd("fizzy pop", fizzyPop, prods);

        //Nuts
        Item nuts = new Item(Item.Type.FOOD, "nuts", 0.5, 1.0);
        nuts.descrip = "Careful of choking hazards";
        nuts.addTag(Item.Tag.HEALTHY);
        nuts.addTag(Item.Tag.VEGETERIAN);
        addProd("nuts", nuts, prods);

        //Pizza
        Item pizza = new Item(Item.Type.FOOD, "pizza", 0.7, 1.5);
        pizza.descrip = "A hawaiian pizza";
        pizza.addTag(Item.Tag.CONVINIENT);
        pizza.addTag(Item.Tag.TACKY);
        pizza.addTag(Item.Tag.UNHEALTHY);
        pizza.addTag(Item.Tag.FUN);
        addProd("pizza", pizza, prods);

        //Baseball bat
        Item baseballBat = new Item(Item.Type.SPORTING, "baseball bat", 56, 1.5);
        baseballBat.descrip = "Doesn't fly -- makes balls fly";
        baseballBat.addTag(Item.Tag.FUN);
        baseballBat.addTag(Item.Tag.HEALTHY);
        addProd("baseball bat", baseballBat, prods);


        return prods;
    }

}
