using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class LeaderBoardScript : MonoBehaviour {

    public Text lvl1Text;
    public Text lvl2Text;
    public Text lvl3Text;
    public Text lvl4Text;
    public GameObject lvl1Object;
    public GameObject lvl2Object;
    public GameObject lvl3Object;
    public GameObject lvl4Object;

    public GameObject forwardlvl;
    public GameObject backwardlvl;

    public TextMeshProUGUI YourName;
    public TextMeshProUGUI YourHighscore;
    public TextMeshProUGUI CurrentLevelText;
    public TextMeshProUGUI outputText;

    private string highscoreString = "";
    private int currentSelect;
    private int temporarySelect;

    private string privatelvl1 = "ysTI0ScqTUGyNuhT29L8ZA9DfumuEUREafkWTuiDbe5g";
    private string publiclvl1 = "5ad02acdd6024519e0bc8ad8";
    private string privatelvl2 = "ZUrEtqU4v0uIR28XL21yCgv2gja_aEuEufD3AkUinDFA";
    private string publiclvl2 = "5ad2ce2ed6024519e0c50f24";
    private string privatelvl3 = "wPkyXf_d7kWOEV9Q2UT_ygBc5KxQp3xECH2q23DwNxHg";
    private string publiclvl3 = "5ad2ce4cd6024519e0c50f5d";
    private string privatelvl4 = "vsK_fMwvKkeEaPi4vTq3qgSSnemSertUCO3qCFvs7jUQ";
    private string publiclvl4 = "5ae4f6dd191a840bcca781c0";

    private string privateCode = "";
    private string publicCode = "";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoreList;
    public string[] nameList;

    private string SubmitName;
    private string checkName;
    public InputField nameEnter;
    public Text inputfieldText;
    private int NameCheckRepeats;
    private int NameCheckRepeatsBoolean = 0;

    public void Awake()
    {
        //PlayerPrefs.SetInt("current", 1);

        PlayerPrefs.SetInt("current", 1);
        currentSelect = PlayerPrefs.GetInt("current", 1);
        outputText.text = "";
        if (inputfieldText.text != "No Name")
        {
            inputfieldText.text = PlayerPrefs.GetString("YourName", "No Name");
        }
        /*
        currentSelect = 1;
        checkDisplay();
        AddNewHighscore("Default", 100);

        currentSelect = 2;
        checkDisplay();
        AddNewHighscore("Default", 100);

        currentSelect = 3;
        checkDisplay();
        AddNewHighscore("Default", 100);
        */
    }
    public void AddNewHighscore(string username, int score)
    {
        StartCoroutine(UploadNewHighscore(username, score));
    }
    public void DownloadHighscore()
    {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }
    IEnumerator DeleteHighscore()
    {
        WWW www = new WWW(webURL + privateCode + "/delete/" + WWW.EscapeURL(SubmitName));
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
            currentSelect++;
            finishDeletion();
        }
        else
        {
            print("Error uploading: " + www.error);
            outputText.text = "Error uploading: " + www.error;
        }
    }

    IEnumerator UploadNewHighscore(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
            outputText.text = "Upload Successful";
            currentSelectSet();
        }
        else
        {
            print("Error uploading: " + www.error);
            outputText.text = "Error uploading: " + www.error;
        }
    }
    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscore(www.text);
        }
        else
        {
            print("Error Downloading: " + www.error);
            highscoreString = "Error Downloading Highscores";
        }
    }
    void FormatHighscore(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoreList = new Highscore[entries.Length];
        nameList = new string[entries.Length];

        highscoreString = "";

            for (int i = 0; i < entries.Length; i++)
            {
                string[] entryInfo = entries[entries.Length - 1 - i].Split(new char[] { '|' });
                string username = entryInfo[0];
                float score = (float.Parse(entryInfo[1]));
                score = score / -10000;

                nameList[i] = username;

                highscoreList[i] = new Highscore(username, score);
                highscoreString += (i + 1) + ". " + highscoreList[i].score + " - " + highscoreList[i].username + "\n";
                print(score + " - " + username + "\n");

                switch (currentSelect)
                {
                    case 1:
                        lvl1Text.text = highscoreString;
                    break;
                    case 2:
                        lvl2Text.text = highscoreString;
                    break;
                    case 3:
                        lvl3Text.text = highscoreString;
                    break;
                    case 4:
                        lvl4Text.text = highscoreString;
                    break;
            }
            

            }
        if (NameCheckRepeatsBoolean == 1)
        {
            NameCheckRepeatsBoolean = 0;
            currentSelect++;
            UploadScores();
        }

    }

    public void showLeaderboard()
    {
        checkDisplay();
        setNameHighscoreText();
        DownloadHighscore();
        //DownloadHighscoresFromDatabase();
    }
    public void leaderboardPress()
    {
        currentSelect = PlayerPrefs.GetInt("current", 1);
        outputText.text = "";
        //inputfieldText.text = PlayerPrefs.GetString("YourName", "No Name");
        
        showLeaderboard();
    }

    public void refreshBoard()
    {
        currentSelect = PlayerPrefs.GetInt("current", 1);
        showLeaderboard();
        outputText.text = "Refreshed";
    }
    public void updateHighscore()
    {
            temporarySelect = currentSelect;
            currentSelect = 1;
            currentSelectSet();
    }

    public void currentSelectSet()
    {
        switch (currentSelect)
        {
            case 1:
                privateCode = privatelvl1;
                publicCode = publiclvl1;
                AddNewHighscore(PlayerPrefs.GetString("YourName", "No Name"), (int)((float.Parse(PlayerPrefs.GetFloat("minigame", 1000).ToString("0.####"))) * -10000));
                currentSelect++;
                break;
            case 2:
                privateCode = privatelvl2;
                publicCode = publiclvl2;
                AddNewHighscore(PlayerPrefs.GetString("YourName", "No Name"), (int)((float.Parse(PlayerPrefs.GetFloat("GameLevel2", 1000).ToString("0.####"))) * -10000));
                currentSelect++;
                break;
            case 3:
                privateCode = privatelvl3;
                publicCode = publiclvl3;
                AddNewHighscore(PlayerPrefs.GetString("YourName", "No Name"), (int)((float.Parse(PlayerPrefs.GetFloat("GameLevel3", 1000).ToString("0.####"))) * -10000));
                break;
            case 4:
                privateCode = privatelvl4;
                publicCode = publiclvl4;
                AddNewHighscore(PlayerPrefs.GetString("YourName", "No Name"), (int)((float.Parse(PlayerPrefs.GetFloat("GameLevel4", 1000).ToString("0.####"))) * -10000));
                break;
            case 5:
                outputText.text = "Upload Successful";
                currentSelect = temporarySelect;
                showLeaderboard();
                break;
        }
    }
    

    public void validateName()
    {
        SubmitName = PlayerPrefs.GetString("YourName", "No Name");
        checkName = nameEnter.text;
        outputText.text = "Processing...";
        if(checkName.Equals(""))
        {
            outputText.text = "Please Enter a Name";
        }
        else if (checkName.Length> 12)
        {
            outputText.text = "Too Many Characters";
        }
        else if(checkName.Equals(PlayerPrefs.GetString("YourName", "No Name")))
        {
            outputText.text = "Please Enter a Different Name";
        }
        else
        {
            int AsciiCheck = 0;
            for (int i = 0; i < checkName.Length; i++)
            {
                if(checkName[i] < '0'||(checkName[i]>'9' &&checkName[i]<'A')|| checkName[i] > 'z' || (checkName[i] > 'Z' && checkName[i] < 'a'))//insert ascii chart
                {
                    AsciiCheck = 1;
                }
            }
            if (AsciiCheck == 1)
            {
                outputText.text = "Contains Invalid Characters";
            }
            else
            {
                temporarySelect = currentSelect;
                currentSelect = 1;
                NameCheckRepeats = 0;
                UploadScores();
            }
        }
    }

    public void UploadScores()
    {
        switch (currentSelect)
        {
            case 1:
                privateCode = privatelvl1;
                publicCode = publiclvl1;
                NameCheckRepeatsBoolean = 1;
                DownloadHighscore();
                break;
            case 2:
                NameCheckRepeats = checkForRepeats();
                privateCode = privatelvl2;
                publicCode = publiclvl2;
                NameCheckRepeatsBoolean = 1;
                DownloadHighscore();
                break;
            case 3:
                NameCheckRepeats = checkForRepeats();
                privateCode = privatelvl3;
                publicCode = publiclvl3;
                NameCheckRepeatsBoolean = 1;
                DownloadHighscore();
                break;
            case 4:
                NameCheckRepeats = checkForRepeats();
                privateCode = privatelvl4;
                publicCode = publiclvl4;
                NameCheckRepeatsBoolean = 1;
                DownloadHighscore();
                break;
            case 5:
                NameCheckRepeats = checkForRepeats();
                //showLeaderboard();
                EnterNameContinued();
                break;
        }
    }

    public void EnterNameContinued()
    {
        if (NameCheckRepeats == 1)
        {
            outputText.text = "Name taken";
        }
        else
        {
            currentSelect = 1;
            finishDeletion();
        }
    }

    public int checkForRepeats()
    {
        int nameCheckRepeats = 0;
        for (int i = 0; i < nameList.Length; i++)
        {
            if (nameList[i].Equals(checkName))
            {
                nameCheckRepeats = 1;
            }
        }
        if(nameCheckRepeats == 1)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }


    public void finishDeletion()
    {
        switch (currentSelect)
        {
            case 1:
                privateCode = privatelvl1;
                publicCode = publiclvl1;
                StartCoroutine("DeleteHighscore");
                break;
            case 2:
                privateCode = privatelvl2;
                publicCode = publiclvl2;
                StartCoroutine("DeleteHighscore");
                break;
            case 3:
                privateCode = privatelvl3;
                publicCode = publiclvl3;
                StartCoroutine("DeleteHighscore");
                break;
            case 4:
                privateCode = privatelvl4;
                publicCode = publiclvl4;
                StartCoroutine("DeleteHighscore");
                break;
            case 5:
                SubmitName = checkName;
                PlayerPrefs.SetString("YourName", SubmitName);
                currentSelect = temporarySelect;
                setNameHighscoreText();
                updateHighscore();
                break;
        }
    }


    private void setNameHighscoreText()
    {
        YourName.text = "Name: "+PlayerPrefs.GetString("YourName", "No Name");
        CurrentLevelText.text = "Leaderboard - Level "+ currentSelect;
    }

    public void forwardGo()
    {
        currentSelect++;
        PlayerPrefs.SetInt("current", currentSelect);
        showLeaderboard();
    }
    public void backwardGo()
    {
        currentSelect--;
        PlayerPrefs.SetInt("current", currentSelect);
        showLeaderboard();
    }
    
    public void checkDisplay()
    {
        setFalse();
        PlayerPrefs.GetInt("current", currentSelect);
        switch (currentSelect)
        {
            
            case 1:
                YourHighscore.text = "Record: " + PlayerPrefs.GetFloat("minigame", 1000).ToString("0.####");
                privateCode = privatelvl1;
                publicCode = publiclvl1;
                lvl1Object.SetActive(true);
                backwardlvl.SetActive(false);
                break;
            case 2:
                YourHighscore.text = "Record: " + PlayerPrefs.GetFloat("GameLevel2", 1000).ToString("0.####");
                privateCode = privatelvl2;
                publicCode = publiclvl2;
                lvl2Object.SetActive(true);
                break;
            case 3:
                YourHighscore.text = "Record: " + PlayerPrefs.GetFloat("GameLevel3", 1000).ToString("0.####");
                privateCode = privatelvl3;
                publicCode = publiclvl3;
                lvl3Object.SetActive(true);
                break;
            case 4:
                YourHighscore.text = "Record: " + PlayerPrefs.GetFloat("GameLevel4", 1000).ToString("0.####");
                privateCode = privatelvl4;
                publicCode = publiclvl4;
                lvl4Object.SetActive(true);
                forwardlvl.SetActive(false);
                break;

        }
    }

    private void setFalse()
    {
        lvl1Object.SetActive(false);
        lvl2Object.SetActive(false);
        lvl3Object.SetActive(false);
        lvl4Object.SetActive(false);

        backwardlvl.SetActive(true);
        forwardlvl.SetActive(true);
    }

    


}
public struct Highscore
{
    public string username;
    public float score;
    //public string date;
    public Highscore(string _username, float _score /*, string date*/)
    {
        username = _username;
        score = _score;
        //date = _date;
    }
}