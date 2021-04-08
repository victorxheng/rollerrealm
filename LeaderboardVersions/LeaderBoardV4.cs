using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LeaderBoardV4 : MonoBehaviour
{
    public Text lvl02Text;
    public Text lvl01Text;
    public Text lvl0Text;
    public Text lvl1Text;
    public Text lvl2Text;
    public Text lvl3Text;
    public Text lvl4Text;
    public Text lvl5Text;
    public GameObject lvl02Object;
    public GameObject lvl01Object;
    public GameObject lvl0Object;
    public GameObject lvl1Object;
    public GameObject lvl2Object;
    public GameObject lvl3Object;
    public GameObject lvl4Object;
    public GameObject lvl5Object;

    public GameObject forwardlvl;
    public GameObject backwardlvl;
    
    public GameObject backButton;
    public GameObject updateButton;
    public GameObject submitButton;
    public GameObject refreshButton;

    public TextMeshProUGUI YourName;
    public TextMeshProUGUI YourHighscore;
    public TextMeshProUGUI CurrentLevelText;
    public TextMeshProUGUI outputText;

    private string highscoreString = "";
    private int currentSelect;
    private int temporarySelect;

    private string[] lvl02Names;
    private string lvl02String;
    private string[] lvl01Names;
    private string lvl01String;
    private string[] lvl0Names;
    private string lvl0String;
    private string[] lvl1Names;
    private string lvl1String;
    private string[] lvl2Names;
    private string lvl2String;
    private string[] lvl3Names;
    private string lvl3String;
    private string[] lvl4Names;
    private string lvl4String;
    private string[] lvl5Names;
    private string lvl5String;

    private string privatelvl02 = "YkvC6-VSpU-xUh1Xx7XDgwsjDs2Ak9HUOewA9t9RQTdg";
    private string publiclvl02 = "5b0986ac191a850bcc34db52";
    private string privatelvl01 = "5OxG7JQCJUW1FqkXt_S6PwG4KMK6PD_EerjaVEq3lzqA";
    private string publiclvl01 = "5b09864a191a850bcc34da38";
    private string privatelvl0 = "m5hmmZz8tUGGdbJTN2wj2QzoALRo9io0u4D1zjjWMqXw";
    private string publiclvl0 = "5af33fb1191a850bcce02c25";
    private string privatelvl1 = "ysTI0ScqTUGyNuhT29L8ZA9DfumuEUREafkWTuiDbe5g";
    private string publiclvl1 = "5ad02acdd6024519e0bc8ad8";
    private string privatelvl2 = "ZUrEtqU4v0uIR28XL21yCgv2gja_aEuEufD3AkUinDFA";
    private string publiclvl2 = "5ad2ce2ed6024519e0c50f24";
    private string privatelvl3 = "wPkyXf_d7kWOEV9Q2UT_ygBc5KxQp3xECH2q23DwNxHg";
    private string publiclvl3 = "5ad2ce4cd6024519e0c50f5d";
    private string privatelvl4 = "vsK_fMwvKkeEaPi4vTq3qgSSnemSertUCO3qCFvs7jUQ";
    private string publiclvl4 = "5ae4f6dd191a840bcca781c0";
    private string privatelvl5 = "FjRZy8ImtkCoxXNxW1BdSQSUhbFN_wDEqRAMp5pzeBoA";
    private string publiclvl5 = "5aedd7d4191a840bccd7ac68";

    private string privateCode = "";
    private string publicCode = "";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoreList;
    public string[] nameList;

    private string SubmitName;
    private string checkName;
    public InputField nameEnter;
    public Text inputfieldText;

    private void Start()
    {
        /*
        PlayerPrefs.SetFloat("GameLevel5", 0);
        PlayerPrefs.SetFloat("GameLevel4", 0);
        PlayerPrefs.SetFloat("GameLevel3", 0);
        PlayerPrefs.SetFloat("GameLevel2", 0);
        PlayerPrefs.SetFloat("minigame", 0);
        */

    }

    public void validateName()
    {
        togglefalse();

        checkName = nameEnter.text;
        outputText.text = "Processing...";

        if (checkName.Equals(""))
        {
            outputText.text = "Please Enter a Name";
            toggletrue();
        }
        else if (checkName.Length > 12)
        {
            outputText.text = "Too Many Characters";
            toggletrue();
        }
        else if (checkName.Equals(PlayerPrefs.GetString("YourName", "No Name")))
        {
            outputText.text = "Please Enter a Different Name";
            toggletrue();
        }
        else
        {

            int AsciiCheck = 0;

            for (int i = 0; i < checkName.Length; i++)
            {
                if (checkName[i].Equals(' '))//insert ascii chart
                {
                    AsciiCheck = 1;
                }
            }

            if(AsciiCheck == 1)
            {
                outputText.text = "Username Cannot Contain Spaces";
                toggletrue();
            }
            else
            {
                AsciiCheck = 0;
                for (int i = 0; i < checkName.Length; i++)
                {
                    if (checkName[i] < '0' || (checkName[i] > '9' && checkName[i] < 'A') || checkName[i] > 'z' || (checkName[i] > 'Z' && checkName[i] < 'a'))//insert ascii chart
                    {
                        AsciiCheck = 1;
                    }
                }
                if (AsciiCheck == 1)
                {
                    outputText.text = "Contains Invalid Characters";
                    toggletrue();
                }
                else
                {
                    StartCoroutine("RefreshLatestData");
                }
            }
            
        }
    }
    
    private void lookForNameRepeats()
    {
        int Repeats = 0;
        for (int i = 0; i < lvl02Names.Length; i++)
        {
            if (checkName.Equals(lvl02Names[i]))
            {
                Repeats++;
            }
        }
        for (int i = 0; i < lvl01Names.Length; i++)
        {
            if (checkName.Equals(lvl01Names[i]))
            {
                Repeats++;
            }
        }
        for (int i = 0; i < lvl0Names.Length; i++)
        {
            if (checkName.Equals(lvl0Names[i]))
            {
                Repeats++;
            }
        }
        for (int i = 0; i < lvl1Names.Length; i++)
        {
            if (checkName.Equals(lvl1Names[i]))
            {
                Repeats++;
            }
        }
        for (int i = 0; i < lvl2Names.Length; i++)
        {
            if (checkName.Equals(lvl2Names[i]))
            {
                Repeats++;
            }
        }
        for (int i = 0; i < lvl3Names.Length; i++)
        {
            if (checkName.Equals(lvl3Names[i]))
            {
                Repeats++;
            }
        }
        for (int i = 0; i < lvl4Names.Length; i++)
        {
            if (checkName.Equals(lvl4Names[i]))
            {
                Repeats++;
            }
        }
        for (int i = 0; i < lvl5Names.Length; i++)
        {
            if (checkName.Equals(lvl5Names[i]))
            {
                Repeats++;
            }
        }
        if (Repeats == 0)
        {
            StartCoroutine("DeleteNames");
        }
        else
        {
            outputText.text = "Name Taken";
            toggletrue();
        }
    }
    

    public void UpdateScores()
    {
        togglefalse();
        StartCoroutine("UpdateScore");
    }
    IEnumerator UpdateScore()
    {
        outputText.text = "Uploading...";
        SubmitName = PlayerPrefs.GetString("YourName", "No Name");

        if (SubmitName.Equals("No Name"))
        {
            outputText.text = "Enter a Name";
            toggletrue();
        }
        else
        {
            privateCode = privatelvl02;
            WWW ww02 = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(SubmitName) + "/" + PlayerPrefs.GetInt("GameLevel6",0));
            yield return ww02;
            privateCode = privatelvl01;
            WWW ww01 = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(SubmitName) + "/" + (PlayerPrefs.GetInt("Cubecount", 0)));
            yield return ww01;
            privateCode = privatelvl0;
            WWW ww0 = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(SubmitName) + "/" + (int)(((float.Parse(PlayerPrefs.GetFloat("minigame", 1000).ToString("0.####"))) + (float.Parse(PlayerPrefs.GetFloat("GameLevel2", 1000).ToString("0.####"))) + (float.Parse(PlayerPrefs.GetFloat("GameLevel3", 1000).ToString("0.####"))) + (float.Parse(PlayerPrefs.GetFloat("GameLevel4", 1000).ToString("0.####"))) + (float.Parse(PlayerPrefs.GetFloat("GameLevel5", 1000).ToString("0.####")))) * -10000));
            yield return ww0;
            privateCode = privatelvl1;
            WWW ww1 = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(SubmitName) + "/" + (int)((float.Parse(PlayerPrefs.GetFloat("minigame", 1000).ToString("0.####"))) * -10000));
            yield return ww1;
            privateCode = privatelvl2;
            WWW ww2 = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(SubmitName) + "/" + (int)((float.Parse(PlayerPrefs.GetFloat("GameLevel2", 1000).ToString("0.####"))) * -10000));
            yield return ww2;
            privateCode = privatelvl3;
            WWW ww3 = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(SubmitName) + "/" + (int)((float.Parse(PlayerPrefs.GetFloat("GameLevel3", 1000).ToString("0.####"))) * -10000));
            yield return ww3;
            privateCode = privatelvl4;
            WWW ww4 = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(SubmitName) + "/" + (int)((float.Parse(PlayerPrefs.GetFloat("GameLevel4", 1000).ToString("0.####"))) * -10000));
            yield return ww4;
            privateCode = privatelvl5;
            WWW ww5 = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(SubmitName) + "/" + (int)((float.Parse(PlayerPrefs.GetFloat("GameLevel5", 1000).ToString("0.####"))) * -10000));
            yield return ww5;
            outputText.text = "Upload Successful";

            toggletrue();
        }
    }


    public void leaderboardClick()
    {
        toggletrue();
        setTexts();
        outputText.text = "Loading...";
        checkDisplay();
        StartCoroutine("DownloadHighscoresFromDatabase");        
    }

    public void checkDisplay()
    {
        setFalse();
        currentSelect = PlayerPrefs.GetInt("currentLeaderboard", -1);
        switch (currentSelect)
        {
            case -2:
                YourHighscore.text = "Highscore: " + PlayerPrefs.GetInt("GameLevel6", 1000);
                lvl02Object.SetActive(true);
                backwardlvl.SetActive(false);
                break;
            case -1:
                YourHighscore.text = "Cubes: " + PlayerPrefs.GetInt("Cubecount", 1000);
                lvl01Object.SetActive(true);
                break;
            case 0:
                YourHighscore.text = "Total: " + (PlayerPrefs.GetFloat("minigame", 1000) + PlayerPrefs.GetFloat("GameLevel2", 1000) + PlayerPrefs.GetFloat("GameLevel3", 1000) + PlayerPrefs.GetFloat("GameLevel4", 1000) + PlayerPrefs.GetFloat("GameLevel5", 1000)).ToString("0.####");
                lvl0Object.SetActive(true);
                break;
            case 1:
                YourHighscore.text = "Record: " + PlayerPrefs.GetFloat("minigame", 1000).ToString("0.####");
                lvl1Object.SetActive(true);
                break;
            case 2:
                YourHighscore.text = "Record: " + PlayerPrefs.GetFloat("GameLevel2", 1000).ToString("0.####");
                lvl2Object.SetActive(true);
                break;
            case 3:
                YourHighscore.text = "Record: " + PlayerPrefs.GetFloat("GameLevel3", 1000).ToString("0.####");
                lvl3Object.SetActive(true);
                break;
            case 4:
                YourHighscore.text = "Record: " + PlayerPrefs.GetFloat("GameLevel4", 1000).ToString("0.####");
                lvl4Object.SetActive(true);
                break;
            case 5:
                YourHighscore.text = "Record: " + PlayerPrefs.GetFloat("GameLevel5", 1000).ToString("0.####");
                lvl5Object.SetActive(true);
                forwardlvl.SetActive(false);
                break;

        }
    }

    private void setFalse()
    {
        lvl02Object.SetActive(false);
        lvl01Object.SetActive(false);
        lvl0Object.SetActive(false);
        lvl1Object.SetActive(false);
        lvl2Object.SetActive(false);
        lvl3Object.SetActive(false);
        lvl4Object.SetActive(false);
        lvl5Object.SetActive(false);

        backwardlvl.SetActive(true);
        forwardlvl.SetActive(true);
    }

    public void setTexts()
    {
        outputText.text = "";
        currentSelect = PlayerPrefs.GetInt("currentLeaderboard", -1);
        if (currentSelect == 0)
        {
            CurrentLevelText.text = "Overall Rankings ";
        }
        else if (currentSelect == -1)
        {
            CurrentLevelText.text = "Total Cubes ";
        }
        else if (currentSelect == -2)
        {
            CurrentLevelText.text = "Infinity Mode ";
        }
        else
        {
            CurrentLevelText.text = "Leaderboard - Level " + currentSelect;
        }
        YourHighscore.text = "Record";
        YourName.text = "Name: "+PlayerPrefs.GetString("YourName", "No Name");
    }


    IEnumerator DeleteNames()
    {
        SubmitName = PlayerPrefs.GetString("YourName", "No Name");
        privateCode = privatelvl02;
        WWW ww02 = new WWW(webURL + privateCode + "/delete/" + WWW.EscapeURL(SubmitName));
        yield return ww02;
        privateCode = privatelvl01;
        WWW ww01 = new WWW(webURL + privateCode + "/delete/" + WWW.EscapeURL(SubmitName));
        yield return ww01;
        privateCode = privatelvl0;
        WWW ww0 = new WWW(webURL + privateCode + "/delete/" + WWW.EscapeURL(SubmitName));
        yield return ww0;
        privateCode = privatelvl1;
        WWW ww1 = new WWW(webURL + privateCode + "/delete/" + WWW.EscapeURL(SubmitName));
        yield return ww1;
        privateCode = privatelvl2;
        WWW ww2 = new WWW(webURL + privateCode + "/delete/" + WWW.EscapeURL(SubmitName));
        yield return ww2;
        privateCode = privatelvl3;
        WWW ww3 = new WWW(webURL + privateCode + "/delete/" + WWW.EscapeURL(SubmitName));
        yield return ww3;
        privateCode = privatelvl4;
        WWW ww4 = new WWW(webURL + privateCode + "/delete/" + WWW.EscapeURL(SubmitName));
        yield return ww4;
        privateCode = privatelvl5;
        WWW ww5 = new WWW(webURL + privateCode + "/delete/" + WWW.EscapeURL(SubmitName));
        yield return ww5;

        PlayerPrefs.SetString("YourName", checkName);
        StartCoroutine("UpdateScore");
    }


    IEnumerator DownloadHighscoresFromDatabase()
    {
        publicCode = publiclvl02;
        WWW ww02 = new WWW(webURL + publicCode + "/pipe/");
        publicCode = publiclvl01;
        WWW ww01 = new WWW(webURL + publicCode + "/pipe/");
        publicCode = publiclvl0;
        WWW ww0 = new WWW(webURL + publicCode + "/pipe/");
        publicCode = publiclvl1;
        WWW ww1 = new WWW(webURL + publicCode + "/pipe/");
        publicCode = publiclvl2;
        WWW ww2 = new WWW(webURL + publicCode + "/pipe/");
        publicCode = publiclvl3;
        WWW ww3 = new WWW(webURL + publicCode + "/pipe/");
        publicCode = publiclvl4;
        WWW ww4 = new WWW(webURL + publicCode + "/pipe/");
        publicCode = publiclvl5;
        WWW ww5 = new WWW(webURL + publicCode + "/pipe/");
        yield return ww02;
        yield return ww01;
        yield return ww0;
        yield return ww1;
        yield return ww2;
        yield return ww3;
        yield return ww4;
        yield return ww5;

        if (string.IsNullOrEmpty(ww02.error))
        {
            FormatHighscore(ww02.text, -2);
            lvl02Text.text = lvl02String;
        }
        else
        {
            outputText.text = "Error Downloading: " + ww02.error;
        }
        if (string.IsNullOrEmpty(ww01.error))
        {
            FormatHighscore(ww01.text, -1);
            lvl01Text.text = lvl01String;
        }
        else
        {
            outputText.text = "Error Downloading: " + ww01.error;
        }
        if (string.IsNullOrEmpty(ww0.error))
        {
            FormatHighscore(ww0.text, 0);
            lvl0Text.text = lvl0String;
        }
        else
        {
            outputText.text = "Error Downloading: " + ww0.error;
        }

        if (string.IsNullOrEmpty(ww1.error))
        {
            FormatHighscore(ww1.text, 1);
            lvl1Text.text = lvl1String;
        }
        else
        {
            outputText.text = "Error Downloading: " + ww1.error;
        }

        if (string.IsNullOrEmpty(ww2.error))
        {
            FormatHighscore(ww2.text, 2);
            lvl2Text.text = lvl2String;
        }
        else
        {
            outputText.text = "Error Downloading: " + ww2.error;
        }

        if (string.IsNullOrEmpty(ww3.error))
        {
            FormatHighscore(ww3.text, 3);
            lvl3Text.text = lvl3String;
        }
        else
        {
            outputText.text = "Error Downloading: " + ww3.error;
        }

        if (string.IsNullOrEmpty(ww4.error))
        {
            FormatHighscore(ww4.text, 4);
            lvl4Text.text = lvl4String;
        }
        else
        {
            outputText.text = "Error Downloading: " + ww4.error;
        }
        if (string.IsNullOrEmpty(ww5.error))
        {
            FormatHighscore(ww5.text, 5);
            lvl5Text.text = lvl5String;
        }
        else
        {
            outputText.text = "Error Downloading: " + ww5.error;
        }
        checkDisplay();
        outputText.text = "";
    }
    void FormatHighscore(string textStream, int lvl)
    {
        print(textStream);
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoreList = new Highscore[entries.Length];
        nameList = new string[entries.Length];

        highscoreString = "";
        print(entries.Length);
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo;
            string username;
            float score;
            if(lvl == -2 || lvl == -1)
            {
                entryInfo = entries[i].Split(new char[] { '|' });
                username = entryInfo[0];
                score = (float.Parse(entryInfo[1]));
            }
            else
            {
                entryInfo = entries[entries.Length - 1 - i].Split(new char[] { '|' });
                username = entryInfo[0];
                score = (float.Parse(entryInfo[1]));
                score = score / -10000;
            }

            nameList[i] = username;

            highscoreList[i] = new Highscore(username, score);
            highscoreString += (i + 1) + ". " + highscoreList[i].score + " - " + highscoreList[i].username + "\n";
        }

        switch (lvl)
        {
            case -2:
                lvl02Names = nameList;
                lvl02String = highscoreString;
                break;
            case -1:
                lvl01Names = nameList;
                lvl01String = highscoreString;
                break;
            case 0:
                lvl0Names = nameList;
                lvl0String = highscoreString;
                break;
            case 1:
                lvl1Names = nameList;
                lvl1String = highscoreString;
                break;
            case 2:
                lvl2Names = nameList;
                lvl2String = highscoreString;
                break;
            case 3:
                lvl3Names = nameList;
                lvl3String = highscoreString;
                break;
            case 4:
                lvl4Names = nameList;
                lvl4String = highscoreString;
                break;
            case 5:
                lvl5Names = nameList;
                lvl5String = highscoreString;
                break;
        }

    }

    public void forward()
    {
        currentSelect++;
        PlayerPrefs.SetInt("currentLeaderboard",currentSelect);
        setTexts();
        checkDisplay();
    }
    public void backward()
    {
        currentSelect--;
        PlayerPrefs.SetInt("currentLeaderboard", currentSelect);
        setTexts();
        checkDisplay();
    }
    IEnumerator RefreshLatestData()
    {
        publicCode = publiclvl02;
        WWW ww02 = new WWW(webURL + publicCode + "/pipe/");
        publicCode = publiclvl01;
        WWW ww01 = new WWW(webURL + publicCode + "/pipe/");
        publicCode = publiclvl0;
        WWW ww0 = new WWW(webURL + publicCode + "/pipe/");
        publicCode = publiclvl1;
        WWW ww1 = new WWW(webURL + publicCode + "/pipe/");
        publicCode = publiclvl2;
        WWW ww2 = new WWW(webURL + publicCode + "/pipe/");
        publicCode = publiclvl3;
        WWW ww3 = new WWW(webURL + publicCode + "/pipe/");
        publicCode = publiclvl4;
        WWW ww4 = new WWW(webURL + publicCode + "/pipe/");
        publicCode = publiclvl5;
        WWW ww5 = new WWW(webURL + publicCode + "/pipe/");
        yield return ww02;
        yield return ww01;
        yield return ww0;
        yield return ww1;
        yield return ww2;
        yield return ww3;
        yield return ww4;
        yield return ww5;

        if (string.IsNullOrEmpty(ww02.error))
        {
            FormatRefresh(ww02.text, -2);
            lvl02Text.text = lvl02String;
        }
        else
        {
            outputText.text = "Error Downloading: " + ww02.error;
        }
        if (string.IsNullOrEmpty(ww01.error))
        {
            FormatRefresh(ww01.text, -1);
            lvl0Text.text = lvl01String;
        }
        else
        {
            outputText.text = "Error Downloading: " + ww01.error;
        }
        if (string.IsNullOrEmpty(ww0.error))
        {
            FormatRefresh(ww0.text, 0);
            lvl0Text.text = lvl0String;
        }
        else
        {
            outputText.text = "Error Downloading: " + ww0.error;
        }
        if (string.IsNullOrEmpty(ww1.error))
        {
            FormatRefresh(ww1.text, 1);
            lvl1Text.text = lvl1String;
        }
        else
        {
            outputText.text = "Error Downloading: " + ww1.error;
        }

        if (string.IsNullOrEmpty(ww2.error))
        {
            FormatRefresh(ww2.text, 2);
            lvl2Text.text = lvl2String;
        }
        else
        {
            outputText.text = "Error Downloading: " + ww2.error;
        }

        if (string.IsNullOrEmpty(ww3.error))
        {
            FormatRefresh(ww3.text, 3);
            lvl3Text.text = lvl3String;
        }
        else
        {
            outputText.text = "Error Downloading: " + ww3.error;
        }

        if (string.IsNullOrEmpty(ww4.error))
        {
            FormatRefresh(ww4.text, 4);
            lvl4Text.text = lvl4String;
        }
        else
        {
            outputText.text = "Error Downloading: " + ww4.error;
        }
        if (string.IsNullOrEmpty(ww5.error))
        {
            FormatRefresh(ww5.text, 5);
            lvl5Text.text = lvl5String;
        }
        else
        {
            outputText.text = "Error Downloading: " + ww5.error;
        }
        lookForNameRepeats();
    }
    void FormatRefresh(string textStream, int lvl)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        nameList = new string[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            if(lvl ==-2 || lvl == -1)
            {
                string[] entryInfo = entries[i].Split(new char[] { '|' });
                string username = entryInfo[0];
                nameList[i] = username;
            }
            else
            {//entries.Length - 1 -
                string[] entryInfo = entries[i].Split(new char[] { '|' });
                string username = entryInfo[0];
                nameList[i] = username;
            }
        }
        switch (lvl)
        {
            case -2:
                lvl02Names = nameList;
                break;
            case -1:
                lvl01Names = nameList;
                break;
            case 0:
                lvl0Names = nameList;
                break;
            case 1:
                lvl1Names = nameList;
                break;
            case 2:
                lvl2Names = nameList;
                break;
            case 3:
                lvl3Names = nameList;
                break;
            case 4:
                lvl4Names = nameList;
                break;
            case 5:
                lvl5Names = nameList;
                break;
        }
    }

    public void toggletrue()
    {
        backButton.SetActive(true);
        updateButton.SetActive(true);
        submitButton.SetActive(true);
        forwardlvl.SetActive(true);
        backwardlvl.SetActive(true);
        refreshButton.SetActive(true);
    }
    public void togglefalse()
    {
        backButton.SetActive(false);
        updateButton.SetActive(false);
        submitButton.SetActive(false);
        forwardlvl.SetActive(false);
        backwardlvl.SetActive(false);
        refreshButton.SetActive(false);
    }

}
