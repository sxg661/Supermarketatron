using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

    public int id;

    public void setID(int id)
    {
        this.id = id;
    }

	public void quit() {
        Debug.Log("quit");
        Application.Quit();
    }

    public void editShop()
    {
        SceneManager.LoadScene("BuildScene");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void startLevel()
    {
        SceneManager.LoadScene("Level");
    }

    public void shop()
    {
        SceneManager.LoadScene("StockShop");
    }

    public void stockMenu()
    {
        if (!BuildController.displayingStockMenu())
        {
            BuildController.setID(id);
            BuildController.displayStockMenu();
        }
    }

    public void closeStockMenu()
    {
        if (BuildController.displayingStockMenu())
        {
            BuildController.closeStockMenu();
        }
    }

    public void destroyCurrentUnit()
    {
        UnitPlacement.freezeUnits(true);
        BuildController.sell();
    }



}
