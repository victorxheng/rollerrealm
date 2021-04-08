using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarketPlaceScript : MonoBehaviour {
    public TextMeshProUGUI[] PurchaseTexts;
    public TextMeshProUGUI outputText;
    public TextMeshProUGUI CubeText;
    public TextMeshProUGUI DollarText;

    private string layout;
    private string changedLayout;

    private int RedPrice = 200;
    private int BluePrice = 0;
    

    // Use this for initialization
    public void Start () {
        outputText.text = "";

        //PlayerPrefs.SetInt("Cubecount", 10000);//tester
        //PlayerPrefs.SetString("marketplace", "20000");//tester

        CubeText.text = "CUBES: "+PlayerPrefs.GetInt("Cubecount", 0);
        DollarText.text = "$" + PlayerPrefs.GetInt("money", 0);
        buttonDisplay();
	}
    public void PurchaseBlue()
    {

        int index = 0; //button number - 1
        int price = BluePrice; //Price
        ballColorPurchase(index, price);

    }
    public void PurchaseRed()
    {

        int index = 1; //button number - 1
        int price = RedPrice; //Price
        ballColorPurchase(index, price);
        
    }
    public void PurchasePizza()
    {

        int index = 2; //button number - 1
        int price = 999; //Price
        ballColorPurchase(index, price);

    }
    public void PurchaseAmerica()
    {

        int index = 3; //button number - 1
        int price = 999; //Price
        ballColorPurchase(index, price);

    }
    public void PurchaseTpose()
    {

        int index = 4; //button number - 1
        int price = 999; //Price
        ballColorPurchase(index, price);

    }
    public void PurchaseSupreme()
    {
        int index = 5; //button number - 1
        int price = 1999; //Price
        ballColorPurchase(index, price);
    }
    public void PurchaseFifaWorldCup()
    {
        int index = 6; //button number - 1
        int price = 1999; //Price
        ballColorPurchase(index, price);
    }
    public void PurchaseFifaFlags()
    {
        int index = 7; //button number - 1
        int price = 1999; //Price
        ballColorPurchase(index, price);
    }

    private void ballColorPurchase(int index, int price)
    {
        layout = PlayerPrefs.GetString("marketplace", "20000000");
        if (layout.Length.Equals(5)) layout += "0";
        if (layout.Length.Equals(6)) layout += "0";
        if (layout.Length.Equals(7)) layout += "0";
        changedLayout = "";
        print(layout[index]);

            if (layout[index].Equals('0'))
            {            
            int Cubecount = PlayerPrefs.GetInt("Cubecount", 0);
            if (Cubecount >= price)
                {
                    for (int i = 0; i < layout.Length; i++)
                    {
                        if (i == index)
                        {
                            changedLayout += "1";
                        }
                        else
                        {
                            changedLayout += layout[i];
                        }
                    }
                    PlayerPrefs.SetInt("Cubecount", Cubecount - price);
                    PlayerPrefs.SetString("marketplace", changedLayout);

                    CubeText.text = "CUBES: " + PlayerPrefs.GetInt("Cubecount", 0);
                    PurchaseTexts[index].text = "EQUIP";
                outputText.text = "Purchased";
            }

                else
                {
                    outputText.text = "Not Enough Cubes";
                print("not enough");
                }


            }
            else if (layout[index].Equals('1'))
            {
                for (int i = 0; i < layout.Length; i++)
                {
                    if (i == index)
                    {
                        changedLayout += "2";
                    }
                    else if (layout[i].Equals('2'))
                    {
                        changedLayout += "1";
                    }
                    else
                    {
                        changedLayout += layout[i];
                    }
                }

                PlayerPrefs.SetString("marketplace", changedLayout);
                buttonDisplay();
                SetMaterial(index);
        }

        
    }
    private void buttonDisplay()
    {

        layout = PlayerPrefs.GetString("marketplace", "20000000");
        if (layout.Length.Equals(5)) layout += "0";
        if (layout.Length.Equals(6)) layout += "0";
        if (layout.Length.Equals(7)) layout += "0";
        CubeText.text = "CUBES: " + PlayerPrefs.GetInt("Cubecount", 0);
        //0 - Purchase
        //1 - Equip
        //2 - Equipped
        //3 - Purchased
        for (int i = 0; i < PurchaseTexts.Length; i++)
        {
            if (layout[i].Equals('0'))
            {
                PurchaseTexts[i].text = "PURCHASE";
            }
            else if (layout[i].Equals('1'))
            {
                PurchaseTexts[i].text = "EQUIP";
            }
            else if (layout[i].Equals('2'))
            {
                PurchaseTexts[i].text = "EQUIPPED";
            }
            else if (layout[i].Equals('3'))
            {
                PurchaseTexts[i].text = "PURCHASED";
            }
        }
    }
    private void SetMaterial(int index)
    {
        switch (index)
        {
            case 0:
                PlayerPrefs.SetString("material","blue");
                break;
            case 1:
                PlayerPrefs.SetString("material","red");
                break;
            case 2:
                PlayerPrefs.SetString("material", "pizza");
                break;
            case 3:
                PlayerPrefs.SetString("material", "america");
                break;
            case 4:
                PlayerPrefs.SetString("material", "tpose");
                break;
            case 5:
                PlayerPrefs.SetString("material", "supreme");
                break;
            case 6:
                PlayerPrefs.SetString("material", "worldcup");
                break;
            case 7:
                PlayerPrefs.SetString("material", "flags");
                break;
        }
    }
}
