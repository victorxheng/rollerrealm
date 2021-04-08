using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubeModeMarket : MonoBehaviour {

    public TextMeshProUGUI CubeCount;
    public TextMeshProUGUI CubePerSecond;
    public TextMeshProUGUI CubeFactor;
    public TextMeshProUGUI CubeFrequency;
    public TextMeshProUGUI BallSpeed;
    public TextMeshProUGUI BallMaxSpeed;

    public TextMeshProUGUI mTotalCubes;
    public TextMeshProUGUI mPerSecond;

    public TextMeshProUGUI cpsId0;
    public TextMeshProUGUI cpsId1;
    public TextMeshProUGUI cpsId2;
    public TextMeshProUGUI cpsId3;
    public TextMeshProUGUI cpsId4;
    public TextMeshProUGUI cpsId5;


    public TextMeshProUGUI dcpsId0;
    public TextMeshProUGUI dcpsId1;
    public TextMeshProUGUI dcpsId2;
    public TextMeshProUGUI dcpsId3;
    public TextMeshProUGUI dcpsId4;
    public TextMeshProUGUI dcpsId5;

    public TextMeshProUGUI cFactor;
    public TextMeshProUGUI dFactor;
    public TextMeshProUGUI cFrequency;
    public TextMeshProUGUI dFrequency;
    public TextMeshProUGUI cSpeed;
    public TextMeshProUGUI dSpeed;
    public TextMeshProUGUI cMax;
    public TextMeshProUGUI dMax;

    public TextMeshProUGUI bSpeed;
    public TextMeshProUGUI bMax;

    public MarketIndex i;
    public CubeModeEndless e;
    public CubeModeScript s;

    public int buyCPSid;
    public string buyCPSplayerpref;

    private bool[] cpsShow;
    
    
    // Use this for initialization
    void Start () {

        changeInCPS();
    }
    public void setOtherTexts()
    {
        //change
        setCPStexts();
        CubeFactor.text = "Cube Factor: " + i.CubeFactorArray[PlayerPrefs.GetInt("CubefactorIndex", 0)];
        CubeFrequency.text = "Cube Frequency: " + i.CubeFrequencyArray[PlayerPrefs.GetInt("CubefrequencyIndex", 0)];
        BallSpeed.text = "Acceleration Speed: " + i.SpeedArray[PlayerPrefs.GetInt("SpeedIndex", 0)];
        BallMaxSpeed.text = "Maximum Speed: " + i.MaxArray[PlayerPrefs.GetInt("MaxIndex", 0)];
    }

    public void setCPStexts()
    {
        CubePerSecond.text = i.CPS;
        mPerSecond.text = i.CPS;
    }
    public void BuyCps(string name)
    {
        string PlayerPref = name;
        int id = PlayerPref[PlayerPref.Length - 1]-'0';
        int Cost = i.GetCost(i.baseArray[id], PlayerPrefs.GetInt(PlayerPref, 0));
        int Cubes = PlayerPrefs.GetInt("Cubecount", 0);
        if (Cubes >= Cost){
            PlayerPrefs.SetInt(PlayerPref, PlayerPrefs.GetInt(PlayerPref, 0)+1);
            PlayerPrefs.SetInt("Cubecount", Cubes-Cost);
            changeInCPS();
            i.setTextsForCPS();
            setCPStexts();
        }
        
    }
        
    public void BuyCubeFactor()
    {
        if(PlayerPrefs.GetInt("CubefactorIndex", 0) < 19)
        {
            if (PlayerPrefs.GetInt("Cubecount", 0) >= i.CubeFactorCostArray[PlayerPrefs.GetInt("CubefactorIndex", 0)])
            {
                int cubes = PlayerPrefs.GetInt("Cubecount", 0);
                PlayerPrefs.SetInt("Cubecount", cubes - i.CubeFactorCostArray[PlayerPrefs.GetInt("CubefactorIndex", 0)]);
                int index = PlayerPrefs.GetInt("CubefactorIndex", 0);
                PlayerPrefs.SetInt("CubefactorIndex", index + 1);
                i.setTexts();
                CubeCount.text = "Cubes: " + PlayerPrefs.GetInt("Cubecount", 0);
                CubeFactor.text = "Cube Factor: " + i.CubeFactorArray[PlayerPrefs.GetInt("CubefactorIndex", 0)];
            }
        }
    }
    public void BuyCubeFrequency()
    {
        if (PlayerPrefs.GetInt("CubefrequencyIndex", 0) < 14)
        {
            if (PlayerPrefs.GetInt("Cubecount", 0) >= i.CubeFrequencyCostArray[PlayerPrefs.GetInt("CubefrequencyIndex", 0)])
            {
                int cubes = PlayerPrefs.GetInt("Cubecount", 0);
                PlayerPrefs.SetInt("Cubecount", cubes - i.CubeFrequencyCostArray[PlayerPrefs.GetInt("CubefrequencyIndex", 0)]);
                int index = PlayerPrefs.GetInt("CubefrequencyIndex", 0);
                PlayerPrefs.SetInt("CubefrequencyIndex", index + 1);
                i.setTexts();
                CubeCount.text = "Cubes: " + PlayerPrefs.GetInt("Cubecount", 0);
                CubeFrequency.text = "Cube Frequency: " + i.CubeFrequencyArray[PlayerPrefs.GetInt("CubefrequencyIndex", 0)];
                e.changeFrequency();
            }
        }
    }

    public void BuySpeed()
    {
        if (PlayerPrefs.GetInt("SpeedIndex", 0) < 9)
        {
            if (PlayerPrefs.GetInt("Cubecount", 0) >= i.SpeedCostArray[PlayerPrefs.GetInt("SpeedIndex", 0)])
            {
                int cubes = PlayerPrefs.GetInt("Cubecount", 0);
                PlayerPrefs.SetInt("Cubecount", cubes - i.SpeedCostArray[PlayerPrefs.GetInt("SpeedIndex", 0)]);
                int index = PlayerPrefs.GetInt("SpeedIndex", 0);
                PlayerPrefs.SetInt("SpeedIndex", index + 1);
                i.setTexts();
                CubeCount.text = "Cubes: " + PlayerPrefs.GetInt("Cubecount", 0);
                BallSpeed.text = "Acceleration Speed: " + i.SpeedArray[PlayerPrefs.GetInt("SpeedIndex", 0)];
                s.changeSpeed();
            }
        }
    }
    public void BuyMax()
    {
        if (PlayerPrefs.GetInt("MaxIndex", 0) < 9)
        {
            if (PlayerPrefs.GetInt("Cubecount", 0) >= i.MaxCostArray[PlayerPrefs.GetInt("MaxIndex", 0)])
            {
                int cubes = PlayerPrefs.GetInt("Cubecount", 0);
                PlayerPrefs.SetInt("Cubecount", cubes - i.MaxCostArray[PlayerPrefs.GetInt("MaxIndex", 0)]);
                int index = PlayerPrefs.GetInt("MaxIndex", 0);
                PlayerPrefs.SetInt("MaxIndex", index + 1);
                i.setTexts();
                CubeCount.text = "Cubes: " + PlayerPrefs.GetInt("Cubecount", 0);
                BallMaxSpeed.text = "Maximum Speed: " + i.MaxArray[PlayerPrefs.GetInt("MaxIndex", 0)];
                s.changeMaxSpeed();
            }
        }
    }

    private void changeInCPS()
    {
        cpsShow = new bool[6];
        cpsShow[0] = true;
        if (PlayerPrefs.GetInt("cps0", 0) > 0) cpsShow[1] = true;
        if (PlayerPrefs.GetInt("cps1", 0) > 0) cpsShow[2] = true;
        if (PlayerPrefs.GetInt("cps2", 0) > 0) cpsShow[3] = true;
        if (PlayerPrefs.GetInt("cps3", 0) > 0) cpsShow[4] = true;
        if (PlayerPrefs.GetInt("cps4", 0) > 0) cpsShow[5] = true;
    }
    
    public void ShadeChange(ref TextMeshProUGUI lightObject, ref TextMeshProUGUI darkObject, string text, string type)
    {
        switch (type)
        {
            case "dark":
                lightObject.text = "";
                darkObject.text = text;
                break;
            case "light":
                lightObject.text = text;
                darkObject.text = "";
                break;
        }
    }
    private void Update()
    {      

        mTotalCubes.text = "TOTAL CUBES: " + PlayerPrefs.GetInt("Cubecount", 0);
        CubeCount.text = "Cubes: " + PlayerPrefs.GetInt("Cubecount", 0);

        if (PlayerPrefs.GetInt("CubefactorIndex") < 19)
        {
            if (PlayerPrefs.GetInt("Cubecount", 0) < i.CubeFactorCostArray[PlayerPrefs.GetInt("CubefactorIndex", 0)]) ShadeChange(ref cFactor, ref dFactor, i.Factor, "dark");
            else ShadeChange(ref cFactor, ref dFactor, i.Factor, "light");
        }
        else ShadeChange(ref cFactor, ref dFactor, i.Factor, "dark");
        
        if (PlayerPrefs.GetInt("CubefrequencyIndex") < 14)
        {
            if (PlayerPrefs.GetInt("Cubecount", 0) < i.CubeFrequencyCostArray[PlayerPrefs.GetInt("CubefrequencyIndex", 0)])
            {
                cFrequency.text = "";
                dFrequency.text = i.Frequency;
            }
            else
            {
                dFrequency.text = "";
                cFrequency.text = i.Frequency;
            }
        }
        else
        {
            cFrequency.text = "";
            dFrequency.text = i.Frequency;
        }

        if (PlayerPrefs.GetInt("SpeedIndex") < 9)
        {
            if (PlayerPrefs.GetInt("Cubecount", 0) < i.SpeedCostArray[PlayerPrefs.GetInt("SpeedIndex", 0)])
            {
                cSpeed.text = "";
                dSpeed.text = i.Speed;
            }
            else
            {
                dSpeed.text = "";
                cSpeed.text = i.Speed;
            }
        }
        else
        {
            cSpeed.text = "";
            dSpeed.text = i.Speed;
        }
        if (PlayerPrefs.GetInt("MaxIndex") < 9)
        {
            if (PlayerPrefs.GetInt("Cubecount", 0) < i.MaxCostArray[PlayerPrefs.GetInt("MaxIndex", 0)])
            {
                cMax.text = "";
                dMax.text = i.Max;
            }
            else
            {
                dMax.text = "";
                cMax.text = i.Max;
            }
        }
        else
        {
            cMax.text = "";
            dMax.text = i.Max;
        }

        if (cpsShow[0] == true)
        {
            if (PlayerPrefs.GetInt("Cubecount", 0) < i.GetCost(i.baseArray[0], PlayerPrefs.GetInt("cps0", 0))) ShadeChange(ref cpsId0, ref dcpsId0, i.CPS0, "dark");
            else ShadeChange(ref cpsId0, ref dcpsId0, i.CPS0, "light");
        }
        else
        {
            cpsId0.text = "";
            dcpsId0.text = "";
        }

        if (cpsShow[1] == true)
        {
            if (PlayerPrefs.GetInt("Cubecount", 0) < i.GetCost(i.baseArray[1], PlayerPrefs.GetInt("cps1", 0))) ShadeChange(ref cpsId1, ref dcpsId1, i.CPS1, "dark");
            else ShadeChange(ref cpsId1, ref dcpsId1, i.CPS1, "light");
        }
        else
        {
            cpsId1.text = "";
            dcpsId1.text = "";
        }


        if (cpsShow[2] == true)
        {
            if (PlayerPrefs.GetInt("Cubecount", 0) < i.GetCost(i.baseArray[2], PlayerPrefs.GetInt("cps2", 0))) ShadeChange(ref cpsId2, ref dcpsId2, i.CPS2, "dark");
            else ShadeChange(ref cpsId2, ref dcpsId2, i.CPS2, "light");
        }
        else
        {
            cpsId2.text = "";
            dcpsId2.text = "";
        }

        if (cpsShow[3] == true)
        {
            if (PlayerPrefs.GetInt("Cubecount", 0) < i.GetCost(i.baseArray[3], PlayerPrefs.GetInt("cps3", 0))) ShadeChange(ref cpsId3, ref dcpsId3, i.CPS3, "dark");
            else ShadeChange(ref cpsId3, ref dcpsId3, i.CPS3, "light");
        }
        else
        {
            cpsId3.text = "";
            dcpsId3.text = "";
        }

        if (cpsShow[4] == true)
        {
            if (PlayerPrefs.GetInt("Cubecount", 0) < i.GetCost(i.baseArray[4], PlayerPrefs.GetInt("cps4", 0))) ShadeChange(ref cpsId4, ref dcpsId4, i.CPS4, "dark");
            else ShadeChange(ref cpsId4, ref dcpsId4, i.CPS4, "light");
        }
        else
        {
            cpsId4.text = "";
            dcpsId4.text = "";
        }

        if (cpsShow[5] == true)
        {
            if (PlayerPrefs.GetInt("Cubecount", 0) < i.GetCost(i.baseArray[5], PlayerPrefs.GetInt("cps5", 0))) ShadeChange(ref cpsId5, ref dcpsId5, i.CPS5, "dark");
            else ShadeChange(ref cpsId5, ref dcpsId5, i.CPS5, "light");
        }
        else
        {
            cpsId5.text = "";
            dcpsId5.text = "";
        }
    }

}
