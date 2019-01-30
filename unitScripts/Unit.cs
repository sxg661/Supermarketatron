using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public static int[] SHELFDIMENSIONS = new int[] { 2, 2 };
    public static int[] SHELFFLOORDIMENSIONS = new int[] { 2, 1 };
    public static int[] COUNTERDIMENSIONS = new int[] { 4, 2 };

    private GameObject myPrefab;
    private GameObject stockPrefab;
    private UnitInfo.unitType myType;
    private int[] myfloorSpace;
    private int[] mySize;
    private Vector3 myPosition;
    private Vector3 stockButtonPosition;
    private GameObject instantiatedObject;
    private GameObject stockButton;


    public UnitBehaviour unitMovement;
    public UnitAnimScript animationController;
    public ButtonController buttonController;


    public void giveType(UnitInfo.unitType type, UnitPrefabs prefabs, bool staticUnit)
    {
        myType = type;
        switch (myType)
        {
            case (UnitInfo.unitType.SHELF):
                //Debug.Log(type.ToString());
                if (staticUnit)
                {
                    myPrefab = prefabs.staticShelfPrefab;
                }
                else {
                    myPrefab = prefabs.shelfPrefab;
                }
                stockPrefab = prefabs.stockPrefab;
                myfloorSpace = SHELFFLOORDIMENSIONS;
                mySize = SHELFDIMENSIONS;
                break;
            case (UnitInfo.unitType.COUNTER):
                myPrefab = prefabs.counterPrefab;
                stockPrefab = prefabs.stockPrefab;
                myfloorSpace = COUNTERDIMENSIONS;
                mySize = COUNTERDIMENSIONS;
                break;
        }
    }

    public void initStaticComponents(int id)
    {
        unitMovement = myPrefab.GetComponent<UnitStatic>();
        initComponents(id);
    }

    public void initMovingComponents(int id)
    {
        unitMovement = myPrefab.GetComponent<UnitMovement>();
        initComponents(id);
    }


    private void initComponents(int id)
    {
        animationController = myPrefab.GetComponent<UnitAnimScript>();
        buttonController = stockPrefab.GetComponent<ButtonController>();
 
        unitMovement.setId(id);
        unitMovement.myType = myType;
        unitMovement.setFloorLength(myfloorSpace[0], myfloorSpace[1]);
        unitMovement.setSize(mySize[0], mySize[1]);
        buttonController.setID(id);
    }

    public void givePosition(int blx, int bly, bool forward)
    {
        unitMovement.setTile(blx, bly);
        myPosition = new Vector3(Tile.getPos(blx, mySize[0]), Tile.getPos(bly, mySize[1]), 0);
        stockButtonPosition = new Vector3(-100, -100, -100);
        unitMovement.forward = forward;

    }


    public void render(bool pending)
    {
        unitMovement.setPending(pending);

        if(animationController != null)
        {
            animationController.pending = pending;

        }
        stockButton = Instantiate(stockPrefab, stockButtonPosition, Quaternion.identity);

        unitMovement.giveStockButton(stockButton);

        instantiatedObject = Instantiate(myPrefab, myPosition, Quaternion.identity);
    }

    public void destroyMe()
    {
        Destroy(instantiatedObject);
    }

    public bool isPending()
    {
        return unitMovement.pending;
    }

    public void removeFromGrid()
    {
        unitMovement.removeFromGrid();
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
