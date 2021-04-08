using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CubeModeMenu : MonoBehaviour {

    public TextMeshProUGUI CubesCollected;
    public void StartCubeMode()
    {
        SceneManager.LoadScene(7);
    }
    public void OnCubeMode()
    {
        CubesCollected.text = "COLLECTED: " + PlayerPrefs.GetInt("Cubecount", 0);
    }
}
