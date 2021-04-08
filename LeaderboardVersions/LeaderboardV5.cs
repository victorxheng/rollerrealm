using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardV5 : MonoBehaviour {

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
    public TextMeshProUGUI YourName;
    public TextMeshProUGUI YourHighscore;
    public TextMeshProUGUI CurrentLevelText;
    public TextMeshProUGUI outputText;

    public GameObject backButton;
    public GameObject updateButton;
    public GameObject submitButton;
    public GameObject refreshButton;
    public GameObject SubmitToLeaderboard;

    private int currentSelect;
    private int temporarySelect;

    public Highscore[] highscoreList;
    public string[] nameList;

    private string SubmitName;
    private string checkName;
    public InputField nameEnter;
    public Text inputfieldText;

    public dreamloLeaderBoard dl;

    private int GetScoreOrRefresh;
    private int repeats;

    public void validateName()
    {
        toggleSubmitFalse();

        checkName = nameEnter.text;
        outputText.text = "Processing...";

        if (checkName.Equals(""))
        {
            outputText.text = "Please Enter a Name";
            toggleSubmitTrue();
        }
        else if (checkName.Length > 18)
        {
            outputText.text = "Too Many Characters";
            toggleSubmitTrue();
        }
        else if (checkName.Equals(PlayerPrefs.GetString("YourName", "No Name")))
        {
            outputText.text = "Please Enter a Different Name";
            toggleSubmitTrue();
        }
        else
        {
            int AsciiCheck = 0;
            for (int i = 0; i < checkName.Length; i++)
            {
                if (checkName[i] < ' ' || (checkName[i] > ' ' && checkName[i] < '0') || (checkName[i] > '9' && checkName[i] < 'A') || checkName[i] > 'z' || (checkName[i] > 'Z' && checkName[i] < '_') || (checkName[i] > '_' && checkName[i] < 'a'))//insert ascii chart
                {
                    AsciiCheck = 1;
                }
            }
            if (AsciiCheck == 1)
            {
                outputText.text = "Contains Invalid Characters";
                toggleSubmitTrue();
            }
            else
            {
                outputText.text = "Loading...";
                GetScoreOrRefresh = 1;
                dl.LoadScores();
            }

        }
    }

    public void leaderboardClick()
    {        
        GetScoreOrRefresh = 0;
        toggleSubmitTrue();
        setTexts();
        outputText.text = "Loading...";
        checkDisplay();
        dl.LoadScores();
    }

    
    public void formatScores()
    {
        repeats = 0;
        List<dreamloLeaderBoard.Score> scoreList02 = dl.ToListHighToLow(-2);
        lvl02Text.text = formatList(scoreList02, 1);

        List<dreamloLeaderBoard.Score> scoreList01 = dl.ToListHighToLow(-1);
        lvl01Text.text = formatList(scoreList01, 1);

        List<dreamloLeaderBoard.Score> scoreList0 = dl.ToListHighToLow(0);
        lvl0Text.text = formatList(scoreList0, 0);

        List<dreamloLeaderBoard.Score> scoreList1 = dl.ToListHighToLow(1);
        lvl1Text.text = formatList(scoreList1,0);

        List<dreamloLeaderBoard.Score> scoreList2 = dl.ToListHighToLow(2);
        lvl2Text.text = formatList(scoreList2,0);

        List<dreamloLeaderBoard.Score> scoreList3 = dl.ToListHighToLow(3);
        lvl3Text.text = formatList(scoreList3,0);

        List<dreamloLeaderBoard.Score> scoreList4 = dl.ToListHighToLow(4);
        lvl4Text.text = formatList(scoreList4, 0);

        List<dreamloLeaderBoard.Score> scoreList5 = dl.ToListHighToLow(5);
        lvl5Text.text = formatList(scoreList5, 0);

        setTexts();
        if(GetScoreOrRefresh == 0)
        {
            checkDisplay();
            outputText.text = "";
        }
        else
        {
            GetScoreOrRefresh = 0;
            if (repeats > 0)
            {
                toggleSubmitTrue();
                outputText.text = "Name Taken";
            }
            else
            {
                outputText.text = "Uploading...";
                dl.DeletePrevious(PlayerPrefs.GetString("YourName", "No Name"));
            }
        }
    }

    public void UploadScores()
    {
        PlayerPrefs.SetString("YourName", checkName);
        dl.AddScore(checkName);
    }
    private int checkListForRepeats(string[] NameList)
    {
        for (int i = 0; i < NameList.Length; i++)
        {
            if (checkName.Equals(NameList[i]))
            {
                return 1;
            }
        }
        return 0;
    }
    private string formatList(List<dreamloLeaderBoard.Score> scoreList, int E)
    {
        int count = 0;
        string ReturnString = "";
        if (scoreList.Count < 1)
        {
            ReturnString = "No Internet Connection";
            return ReturnString;
        }
        foreach (dreamloLeaderBoard.Score currentScore in scoreList)
        {
            count++;
            ReturnString += count + ". ";
            if (E == 0)
            {
                ReturnString += (currentScore.score - 10000000) / -10000 + " - ";
            }
            else
            {
                ReturnString += currentScore.score + " - ";
            }
                ReturnString += Clean(currentScore.playerName) + " ";
                ReturnString += "\n";
            if(GetScoreOrRefresh == 1)
            {
                if (checkName.Equals(Clean(currentScore.playerName)))
                {
                    repeats++;
                }
            }
        }
        print(ReturnString);
        return ReturnString;
    }
    public void updateScore()
    {
        toggleSubmitFalse();
        checkName = nameEnter.text;
        outputText.text = "Updating...";
        dl.AddScore(PlayerPrefs.GetString("YourName", "No Name"));
    }

    public void setTexts()
    {
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
            CurrentLevelText.text = "Leaderboard - Course " + currentSelect;
        }
        YourHighscore.text = "Record";
        YourName.text = "Name: " + PlayerPrefs.GetString("YourName", "No Name");
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
    public void forward()
    {
        currentSelect++;
        PlayerPrefs.SetInt("currentLeaderboard", currentSelect);
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
    public void toggleSubmitFalse()
    {
        SubmitToLeaderboard.SetActive(false);
        backButton.SetActive(false);
    }
    public void toggleSubmitTrue()
    {
        SubmitToLeaderboard.SetActive(true);
        backButton.SetActive(true);
    }
    string Clean(string s)
    {
        s = s.Replace("/", "");
        s = s.Replace("|", "");
        s = s.Replace("+", " ");
        return s;

    }

}
