using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfinityModeMenuScript : MonoBehaviour {
    public TextMeshProUGUI BlockCount;
    public TextMeshProUGUI HighscoreText;

    public void onClick()
    {
        HighscoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("GameLevel6", 0);
        BlockCount.text = "CUBES COLLECTED: " + PlayerPrefs.GetInt("Cubecount", 0);
    }
    public void startGame()
    {
        SceneManager.LoadScene(6);
    }
}
