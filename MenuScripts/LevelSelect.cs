using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelect : MonoBehaviour {

    private int currentLevel;

    public TextMeshProUGUI LevelName;
    public TextMeshProUGUI CourseRecord;
    public TextMeshProUGUI Reward;

    public GameObject backArrow;
    public GameObject forwardArrow;
    public GameObject Level1;
    public GameObject Level2;
    public GameObject Level3;
    public GameObject Level4;
    public GameObject Level5;


    public void PlayGame()
    {
            SceneManager.LoadScene(currentLevel);
    }
    // Use this for initialization
    void Start () {
        currentLevel =   PlayerPrefs.GetInt("current", 1);
        checkDisplay();
    }
    public void PlayActive()
    {
        currentLevel = PlayerPrefs.GetInt("current", 1);
        if(currentLevel == 0)
        {
            currentLevel = 1;
            PlayerPrefs.SetInt("current", 1);
        }
        checkDisplay();
    }
	
	public void ForwardArrow()
    {
        currentLevel++;
        PlayerPrefs.SetInt("current", currentLevel);
        checkDisplay();
    }
    public void BackwardArrow()
    {
        currentLevel--;
        PlayerPrefs.SetInt("current", currentLevel);
        checkDisplay();
    }
    private void checkDisplay()
    {
        currentLevel = PlayerPrefs.GetInt("current", 1);
        if (currentLevel == 1)
        {
            LevelName.text = "COURSE: 1";
            float highscore = PlayerPrefs.GetFloat("minigame", (float)1000);
            CourseRecord.text = "RECORD: " + highscore.ToString("0.#");
            Reward.text = "REWARD: $" + GetReward(10, PlayerPrefs.GetInt("r1",0));

            backArrow.SetActive(false);
            forwardArrow.SetActive(true);

            Level1.SetActive(true);
            Level2.SetActive(false);
            Level3.SetActive(false);
            Level4.SetActive(false);
            Level5.SetActive(false);
        }
        if (currentLevel == 2)
        {
            LevelName.text = "COURSE: 2";
            float highscore = PlayerPrefs.GetFloat("GameLevel2", (float)1000);
            CourseRecord.text = "RECORD: " + highscore.ToString("0.#");
            Reward.text = "REWARD: $" + GetReward(10, PlayerPrefs.GetInt("r2", 0));

            backArrow.SetActive(true);
            forwardArrow.SetActive(true);

            Level1.SetActive(false);
            Level2.SetActive(true);
            Level3.SetActive(false);
            Level4.SetActive(false);
            Level5.SetActive(false);
        }
        if (currentLevel == 3)
        {
            LevelName.text = "COURSE: 3";
            float highscore = PlayerPrefs.GetFloat("GameLevel3", (float)1000);
            CourseRecord.text = "RECORD: " + highscore.ToString("0.#");
            Reward.text = "REWARD: $" + GetReward(16, PlayerPrefs.GetInt("r3", 0));

            backArrow.SetActive(true);
            forwardArrow.SetActive(true);

            Level1.SetActive(false);
            Level2.SetActive(false);
            Level3.SetActive(true);
            Level4.SetActive(false);
            Level5.SetActive(false);
        }
        if (currentLevel == 4)
        {
            LevelName.text = "COURSE: 4";
            float highscore = PlayerPrefs.GetFloat("GameLevel4", (float)1000);
            CourseRecord.text = "RECORD: " + highscore.ToString("0.#");
            Reward.text = "REWARD: $" + GetReward(40, PlayerPrefs.GetInt("r4", 0));

            backArrow.SetActive(true);
            forwardArrow.SetActive(true);

            Level1.SetActive(false);
            Level2.SetActive(false);
            Level3.SetActive(false);
            Level4.SetActive(true);
            Level5.SetActive(false);
        }
        if (currentLevel == 5)
        {
            LevelName.text = "COURSE: 5";
            float highscore = PlayerPrefs.GetFloat("GameLevel5", (float)1000);
            CourseRecord.text = "RECORD: " + highscore.ToString("0.#");
            Reward.text = "REWARD: $" + GetReward(100, PlayerPrefs.GetInt("r5", 0));

            backArrow.SetActive(true);
            forwardArrow.SetActive(false);

            Level1.SetActive(false);
            Level2.SetActive(false);
            Level3.SetActive(false);
            Level4.SetActive(false);
            Level5.SetActive(true);
        }

    }
    public int GetReward(int baseCost, int index)
    {
        return (int)(baseCost * Mathf.Pow((float)0.5, index));
    }

}
