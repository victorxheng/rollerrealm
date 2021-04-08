using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketPlaceDisplay : MonoBehaviour {

    public GameObject color1;
    public GameObject color2;
    public GameObject color3;
    public GameObject color4;
    public GameObject color5;
    public GameObject color6;
    public GameObject color7;
    public GameObject color8;

    public GameObject upArrow;
    public GameObject downArrow;

    private int current;

    public void onMarketplaceClick()
    {
        current = PlayerPrefs.GetInt("MarketCurrent", 1);
        checkDisplay(current);
    }
    public void up()
    {
        current--;
        PlayerPrefs.SetInt("MarketCurrent", current);
        checkDisplay(current);
    }

    public void down()
    {
        current++;
        PlayerPrefs.SetInt("MarketCurrent", current);
        checkDisplay(current);
    }
    private void checkDisplay(int current)
    {
        setFalse();
        switch (current)
        {
            case 1:
                color1.SetActive(true);
                upArrow.SetActive(false);
                break;
            case 2:
                color2.SetActive(true);
                break;
            case 3:
                color3.SetActive(true);
                break;
            case 4:
                color4.SetActive(true);
                break;
            case 5:
                color5.SetActive(true);
                break;
            case 6:
                color6.SetActive(true);
                break;
            case 7:
                color7.SetActive(true);
                break;
            case 8:
                color8.SetActive(true);
                downArrow.SetActive(false);
                break;
        }
    }
    private void setFalse()
    {
        color1.SetActive(false);
        color2.SetActive(false);
        color3.SetActive(false);
        color4.SetActive(false);
        color5.SetActive(false);
        color6.SetActive(false);
        color7.SetActive(false);
        color8.SetActive(false);

        upArrow.SetActive(true);
        downArrow.SetActive(true);
    }
}
