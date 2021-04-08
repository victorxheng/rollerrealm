using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketIndex : MonoBehaviour {

    public string CPS;
    public string CPS0;
    public string CPS1;
    public string CPS2;
    public string CPS3;
    public string CPS4;
    public string CPS5;
    public int[] baseArray;
    public int[] cpsArray;

    public string Factor;
    public int[] CubeFactorCostArray;
    public int[] CubeFactorArray;

    public string Frequency;
    public int[] CubeFrequencyArray;
    public int[] CubeFrequencyCostArray;

    public string Speed;
    public int[] SpeedArray;
    public int[] SpeedCostArray;

    public string Max;
    public int[] MaxArray;
    public int[] MaxCostArray;

    public CubeModeMarket c;

    // Use this for initialization
    void Start () {
        baseArray = new int[] { 29, 79,199,399,799,1599};
        cpsArray = new int[] { 1,2,5,10,20,50};

        CubeFactorArray = new int[] { 1, 2, 3, 5, 8, 12, 18, 25, 35, 50, 75, 100, 150, 250, 500, 800, 1000, 1200, 1500, 2000 };
        CubeFactorCostArray = new int[] { 39, 99, 499, 1999, 3999, 9999, 29999, 89999, 149999, 399999, 999999, 4999999, 6899999, 9999999, 12999999, 24999999, 45999999, 99999999, 484632879};

        CubeFrequencyArray = new int[] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15};
        CubeFrequencyCostArray = new int[] {29,99,399,999,4999,9999,59999,99999,199999,399999,689999,999999,2999999,9999999};

        SpeedArray = new int[] {5,6,7,8,9,10,12,14,16,18};
        SpeedCostArray = new int[] { 49, 199, 599, 1999, 5999, 49999, 89999, 299999, 999999};

        MaxArray = new int[] { 20, 21, 22, 23, 24, 25, 26, 28, 30, 32};
        MaxCostArray = new int[] { 89, 299, 799, 2999, 7999, 59999, 99999, 499999, 1999999};
        setTexts();
        c.setOtherTexts();
    }
    public void setTextsForCPS()
    {
        setCPS();
        CPS0 = setCostString(0, PlayerPrefs.GetInt("cps0", 0));
        CPS1 = setCostString(1, PlayerPrefs.GetInt("cps1", 0));
        CPS2 = setCostString(2, PlayerPrefs.GetInt("cps2", 0));
        CPS3 = setCostString(3, PlayerPrefs.GetInt("cps3", 0));
        CPS4 = setCostString(4, PlayerPrefs.GetInt("cps4", 0));
        CPS5 = setCostString(5, PlayerPrefs.GetInt("cps5", 0));
        CPS = "Cubes Per Second: " + PlayerPrefs.GetInt("cps", 0);
    }
    public void setTexts()
    {
        setTextsForCPS();

        int index = PlayerPrefs.GetInt("CubefactorIndex", 0);
        if(index ==19) Factor = "Cube Factor: Maxed" ;
        else Factor = "Cube Factor: " + CubeFactorArray[index + 1] + " (Cost: " + CubeFactorCostArray[index] + ")";


        index = PlayerPrefs.GetInt("CubefrequencyIndex", 0);
        if (index == 14) Frequency = "Cube Frequency: Maxed";
        else Frequency = "Cube Frequency: +1 (Cost: " + CubeFrequencyCostArray[index] + ")";

        index = PlayerPrefs.GetInt("SpeedIndex", 0);
        if (index == 9) Speed = "Speed: Maxed";
        else Speed = "Ball Speed: " + SpeedArray[index + 1] + " (Cost: " + SpeedCostArray[index] + ")";

        index = PlayerPrefs.GetInt("MaxIndex", 0);
        if (index == 9) Max = "Max Speed: Maxed";
        else Max = "Max Speed: " + MaxArray[index + 1] + " (Cost: " + MaxCostArray[index] + ")";
    }
    public void setCPS()
    {
        int t0 = PlayerPrefs.GetInt("cps0") * cpsArray[0];
        int t1 = PlayerPrefs.GetInt("cps1") * cpsArray[1];
        int t2 = PlayerPrefs.GetInt("cps2") * cpsArray[2];
        int t3 = PlayerPrefs.GetInt("cps3") * cpsArray[3];
        int t4 = PlayerPrefs.GetInt("cps4") * cpsArray[4];
        int t5 = PlayerPrefs.GetInt("cps5") * cpsArray[5];
        PlayerPrefs.SetInt("cps",t0+t1+t2+t3+t4+t5);
    }

    private string setCostString(int id, int index)
    {
        return "+" + cpsArray[id] + " Cost: " + GetCost(baseArray[id], index) + " (" + index + ")";
    }
    public int GetCost(int baseCost, int index)
    {
        return (int)(baseCost * Mathf.Pow((float) 1.1 , index));
    }
}
