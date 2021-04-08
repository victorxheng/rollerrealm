using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ControllerScript : MonoBehaviour {
    private Rigidbody rb;
    private int count;
    private double highscore;
    private int volumeStatus;
    private string material;

    public TextMeshProUGUI countText;
    public float speed;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI rewardText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI recordText;

    public TextMeshProUGUI countTimeText;
    public TextMeshProUGUI HighscoreText;

    public Vector3 position;

    public Renderer Player;
    public Material blueBall;
    public Material redBall;
    public Material pizzaBall;
    public Material americaBall;
    public Material tposeBall;
    public Material supreme;
    public Material worldcup;
    public Material flags;

    private int touchStart;
    private float timePassed;
    private int updateStop;

    private void Start()
    {
        updateStop = 0;
        touchStart = 0;
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

        volumeStatus = PlayerPrefs.GetInt("Volume", 1);
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();

        winText.text = "";
        rewardText.text = "";
        timeText.text = "";
        recordText.text = "";

        highscore = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name, (float)highscore);
        if(highscore == 0)
        {
            highscore = 1000;
        }
        HighscoreText.text = "Record: "+highscore.ToString("0.#");
        countTimeText.text = "Time: 0";
    }
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void FixedUpdate()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            // Move object across XY plane
            Vector3 movement = new Vector3(touchDeltaPosition.x * speed * (float)0.025, 0.0f, touchDeltaPosition.y * speed * (float)0.025);

            rb.AddForce(movement * speed);
            if (touchStart == 0) touchStart++;            
        }
        if (count < 12)
        {
            if(touchStart > 0)
            {
                timePassed += Time.deltaTime;
                countTimeText.text = "Time: " + timePassed.ToString("0.#");
            }
        }
        else
        {
            if(updateStop == 0)
            {
                updateStop++;
                int[] rewardBaseCosts = new int[] { 10, 10, 16, 40, 100 };
                int cCurrent = PlayerPrefs.GetInt("current", 1);
                PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money", GetReward(rewardBaseCosts[cCurrent - 1], PlayerPrefs.GetInt("current" + cCurrent, 0))));
                PlayerPrefs.SetInt("r" + cCurrent, PlayerPrefs.GetInt("r" + cCurrent, 0) + 1);


                winText.text = "COURSE COMPLETE";
                rewardText.text = "REWARD: +$" + PlayerPrefs.GetInt("money", GetReward(rewardBaseCosts[cCurrent - 1], PlayerPrefs.GetInt("current" + cCurrent, 0)));
                timeText.text = "YOUR TIME: " + timePassed.ToString("0.####");
                recordText.text = "RECORD: " + PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name, 1000);
                if (timePassed < highscore)
                {
                    
                        highscore = timePassed;
                        HighscoreText.text = "Record: " + timePassed.ToString("0.#");

                        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, (float)highscore);
                    
                }
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            
            other.gameObject.SetActive(false);
            count = count + 1;
            if (volumeStatus == 1)
            {
                FindObjectOfType<AudioManager>().Play("CollectSound");
            }
            setCountText();
        }
    }
    private void setCountText()
    {
        countText.text = "Count: " + count.ToString() +" / 12";
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

    public int GetReward(int baseCost, int index)
    {
        return (int)(baseCost * Mathf.Pow((float)0.5, index));
    }

}

