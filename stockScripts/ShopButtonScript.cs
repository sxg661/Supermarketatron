using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonScript : MonoBehaviour {

    public int myButtonId;

    private int myProdId;
    private Item myProduct;

    public Button buttonComponent;
    public Text nameLabel;
    public Image iconImage;
    public Text priceText;

    public GameObject controller;

    private ProductButtonController buttonController;
    private ProductImages images;

    private void reConfigureSelf()
    {

        myProdId = buttonController.getMyProdId(myButtonId);
        Debug.Log(myProdId);
        Optional<Item> newItem = StockShop.getProd(myProdId);


        if (newItem.isPresent())
        {
            myProduct = newItem.get();
            nameLabel.text = myProduct.name;
            iconImage.sprite = images.getImage(myProduct.name).sprite;
            priceText.text = string.Format("£{0:0.00}", decimal.Parse(myProduct.wholesalePrice.ToString() )  );
        }
        else
        {
            myProduct = null;
            nameLabel.text = "";
            iconImage.sprite = images.defaultImage.sprite;
            priceText.text = "";
        }
    }



    // Use this for initialization
    void Start () {


        buttonComponent.onClick.AddListener(HandleClick);
        buttonController = controller.GetComponent<ProductButtonController>();
        images = controller.GetComponent<ProductImages>();
        myProdId = -1;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(myProdId + " " + buttonController.getMyProdId(myButtonId));
		if(buttonController.getMyProdId(myButtonId) != myProdId)
        {
            reConfigureSelf();
        }
	}

    public void HandleClick()
    {
        
    }
}
