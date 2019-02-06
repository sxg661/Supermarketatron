using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour {

    public int numCustomers = 0;

    private int capacity = TileGrid.getGridSize() / 12;

    public GameObject customerPrefab;

    //approximate time between customer spawns
    private int probOfCust = 3;

    private float timeSinceLast = 0f;
    private float timePerSpawn = 1f;

    public void removeCustomer()
    {
        numCustomers--;
    }

    public Vector3 entrance;

	// Use this for initialization
	void Start () {
        int[] ent = TileGrid.getDoorLoc();
        entrance = new Vector3(Tile.getPos(ent[0],1), Tile.getPos(ent[1],1), 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        timeSinceLast += Time.deltaTime;

        if(timeSinceLast < timePerSpawn)
        {
            return;
        }

        timeSinceLast = 0;

        if (Random.Range(0, probOfCust) != 0)
        {
            return;
        }

		if(numCustomers < capacity)
        {
            GameObject newCust = Instantiate(customerPrefab, entrance, Quaternion.identity);
            CasualCustomer custControl = newCust.GetComponent<CasualCustomer>();
            custControl.spawner = this;
            numCustomers++;
        }
	}
}
