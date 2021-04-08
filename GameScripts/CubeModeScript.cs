using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CubeModeScript : MonoBehaviour {
    private Transform playerTransform;
    private int volumeStatus;
    private Rigidbody rb;
    public float speed;
    private string material;
    public float maxSpeed;
    
    public TextMeshProUGUI CubeText;
    

    public Renderer Player;
    public Material blueBall;
    public Material redBall;
    public Material pizzaBall;
    public Material americaBall;
    public Material tposeBall;
    public Material supreme;
    public Material worldcup;
    public Material flags;

    public MarketIndex i;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("addCPS", 1.0f, 1.0f);
        speed = i.SpeedArray[PlayerPrefs.GetInt("SpeedIndex", 0)];
        maxSpeed = i.MaxArray[PlayerPrefs.GetInt("MaxIndex", 0)];
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
        
    }
    void addCPS()
    {
        PlayerPrefs.SetInt("Cubecount", PlayerPrefs.GetInt("Cubecount", 0) + PlayerPrefs.GetInt("cps", 0));
    }

    public void changeSpeed()
    {
        speed = i.SpeedArray[PlayerPrefs.GetInt("SpeedIndex", 0)];
    }
    public void changeMaxSpeed()
    {
        maxSpeed = i.MaxArray[PlayerPrefs.GetInt("MaxIndex", 0)];
    }
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                // Get movement of the finger since last frame
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                // Move object across XY plane
                Vector3 movement = new Vector3(touchDeltaPosition.x * speed * (float)0.025, 0.0f, touchDeltaPosition.y * speed * (float)0.025);
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
            rb.AddForce(movement * speed);
            }
        if (playerTransform.position.y < -5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            
            if (volumeStatus == 1)
            {
                FindObjectOfType<AudioManager>().Play("CollectSound");
            }
            PlayerPrefs.SetInt("Cubecount", PlayerPrefs.GetInt("Cubecount", 0) + i.CubeFactorArray[PlayerPrefs.GetInt("CubefactorIndex", 0)]);
        }
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
