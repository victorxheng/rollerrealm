using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class ControllerScriptLvl6 : MonoBehaviour
{
    private Transform playerTransform;
    private int volumeStatus;
    private Rigidbody rb;
    private int count;
    public float speed;
    private string material;

    public TextMeshProUGUI countText;
    public TextMeshProUGUI HighscoreText;
    public TextMeshProUGUI CubeText;

    public TextMeshProUGUI UseReviveText;
    public Button UseReviveButton;
    public TextMeshProUGUI CubeTotal;
    public GameObject GameOver;
    public GameObject ReviveObject;

    private int revivesUsed;
    private int highscore;
    private int UpdateStop;

    public Renderer Player;
    public Material blueBall;
    public Material redBall;
    public Material pizzaBall;
    public Material americaBall;
    public Material tposeBall;
    public Material supreme;
    public Material worldcup;
    public Material flags;

    

    // Use this for initialization
    void Start () {
        UpdateStop = 0;
        GameOver.SetActive(false);
        material = PlayerPrefs.GetString("material", "blue");
        switch (material)
        {
            case "blue":
                Player.material = blueBall;
                break;
            case "red":
                Player.material = redBall;
                break;
            case "pizza":
                Player.material = pizzaBall;
                break;
            case "america":
                Player.material = americaBall;
                break;
            case "tpose":
                Player.material = tposeBall;
                break;
            case "supreme":
                Player.material = supreme;
                break;
            case "worldcup":
                Player.material = worldcup;
                break;
            case "flags":
                Player.material = flags;
                break;
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        volumeStatus = PlayerPrefs.GetInt("Volume", 1);
        rb = GetComponent<Rigidbody>();
        count = 0;
        countText.text = "Cubes: "+ count;
        highscore = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name, highscore);
        
        HighscoreText.text = "Highscore: " + highscore.ToString("0.#");
        int cubecount = PlayerPrefs.GetInt("Cubecount", 0);
        CubeText.text = "Total Collected: " + (cubecount);

        revivesUsed = 0;
    }
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    
    


   

// Update is called once per frame
void Update () {
        if(playerTransform.position.y < -20)
        {
            rb.velocity = new Vector3(0,0,0);
            float posZ = playerTransform.position.z;

            if(UpdateStop == 0)
            {
                UpdateStop++;
                PlayerPrefs.SetFloat("PosZ", posZ);
                CubeTotal.text = "Cubes: " + count;
                UseReviveText.text = "Revive (Watch Ad)";

                if (revivesUsed == 0)
                {
                    ReviveObject.SetActive(true);
                }
                else
                {
                    ReviveObject.SetActive(false);
                }
                GameOver.SetActive(true);
            }
            
        }
        else
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                // Get movement of the finger since last frame
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                // Move object across XY plane
                Vector3 movement = new Vector3(touchDeltaPosition.x * speed * (float)0.025, 0.0f, touchDeltaPosition.y * speed * (float)0.025);

                rb.AddForce(movement * speed);
            }
        }
        
    }

    public void Revive()
    {        
            revivesUsed++;
            float PosZ = PlayerPrefs.GetFloat("PosZ", 0) - (PlayerPrefs.GetFloat("PosZ", 0) % 200);
            rb.velocity = new Vector3(0, 0, 0);
            rb.ResetInertiaTensor();
            if (PosZ != 0)
            {
                PosZ = PosZ - 150.0f;
            }
            Vector3 reviveStart = new Vector3(0, 5, PosZ);
            playerTransform.position = reviveStart;
            GameOver.SetActive(false);        
            UpdateStop = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            if (count > highscore)
            {
                highscore = count;
                HighscoreText.text = "Highscore: " + count;

                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, highscore);
            }

            if (volumeStatus == 1)
            {
                FindObjectOfType<AudioManager>().Play("CollectSound");
            }
            setCountText();
            int cubecount = PlayerPrefs.GetInt("Cubecount", 0);
            PlayerPrefs.SetInt("Cubecount", cubecount + 1);
            CubeText.text = "Total Collected: "+(cubecount+1);
        }
    }
    private void setCountText()
    {
        countText.text = "Cubes: " + count.ToString();
    }
    public void volumeToggle()
    {
        volumeStatus = PlayerPrefs.GetInt("Volume", 1);
        if (volumeStatus == 1)
        {
            volumeStatus = 0;
        }
        else
        {
            volumeStatus = 1;
        }
    }
}
