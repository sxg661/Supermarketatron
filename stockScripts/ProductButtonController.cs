using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductButtonController : MonoBehaviour {

    int indexStart = 0;

    public void moveToNextPage()
    {
        if (indexStart + 7 < (StockShop.numOfProducts - 1))
        {
            indexStart += 8;
        }
    }

    public void moveToPreviousPage()
    {
        if (indexStart != 0)
        {
            indexStart -= 8;
        }
    }

    public int getMyProdId(int buttonId)
    {
        return indexStart + buttonId;
    }
}

