//Used to manage all player main stuff, like jumping, points etc.
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class PlayerManager : MonoBehaviour
{
    //Actuall player dependent variables
    public bool DiedByFall;
    new Rigidbody2D rigidbody;
    public float JumpHeight, currentY;
    public int Hits, CurrentPoints ,Points;
    //Variables for UI and other graphics magic
    public ParticleSystem PointsParticle;
    public Material particleMaterial = null;
    public Texture[] pointsParticleTexture = new Texture[10];
    public Text CurrentPointsText , AllPoints;
    public Button StartButton, ShopButton;
    public Image Logo;
    public Camera cam;
    public GameObject[] cloudstokill;
    public GameObject CloudsGenerator;

    //Initializes all variables
    void Awake()
    {
        gameObject.SetActive(false);
    }

    //Initialize after first rebirth
    void Start()
    {
        //General
        this.gameObject.SetActive(true);
        DiedByFall = false;
        //Graphics stuff
        QualitySettings.vSyncCount = 0;
        //Saving stuff
        AllPoints.text = PlayerPrefs.GetInt("All points").ToString();
        rigidbody = this.GetComponent<Rigidbody2D>();
        CurrentPoints = 0;
        Hits = 0;
        //UI stuff
        StartButton.gameObject.SetActive(false);
        ShopButton.gameObject.SetActive(false);
    }

    //Initialize after each another rebirth
    void NextPlay()
    {
        cam.backgroundColor = Random.ColorHSV();
        StartButton.gameObject.SetActive(true);
        ShopButton.gameObject.SetActive(true);
        Logo.gameObject.SetActive(true);
        CurrentPoints = 0;
        Hits = 0;
        AllPoints.text = PlayerPrefs.GetInt("All points").ToString();
        if(!StartButton.IsActive() || !ShopButton.IsActive()) this.gameObject.SetActive(true);
    }
		
    void Update()
    {
        Jump();
        IsFalling();
        StartCoroutine(DeadByFall());
    }

    //Jump logic
    public void Jump()
    {
        //Jump logic for touch input
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                rigidbody.velocity = new Vector2(0, JumpHeight);
                CurrentPoints++;
                EmitParticle();
                currentY = this.transform.position.y;
            }
        }
        //Jump logic for keyboard input || Only for dev tests
        if (Input.GetButtonDown("Jump"))
        {              
            rigidbody.velocity = new Vector2(0, JumpHeight);
            CurrentPoints++;
            EmitParticle();
            currentY = this.transform.position.y;
        }
    }

    //Used for killing player, and sum current game points to all points
    void OnCollisionEnter2D(Collision2D col)
    {
        DeadByCollision(col);
    }

    //Implements death after collision
    public void DeadByCollision(Collision2D col)
    {
        Hits++;
        if (Hits == 2 || this.gameObject.transform.position.y <= -4.5f || col.gameObject.tag == "Enemy")
        {
            //Gamepley stuff
            this.gameObject.SetActive(false);
            cam.gameObject.transform.parent = null;
            CloudsDestroyer();
            //Saving stuff
            Points = CurrentPoints + PlayerPrefs.GetInt("All points");
            PlayerPrefs.SetInt("All points", Points);
            //UI stuff
            CurrentPointsText.canvasRenderer.gameObject.SetActive(false);
            NextPlay();
        }
    }

    //Emmits points paricle each tap
    void EmitParticle()
    {
        PointsParticle.Emit(1);
    }

    //Ads manager
    void ShowAd()
    {
        if (Advertisement.IsReady() )
        {
            Advertisement.Show();
        }
        else Start();
    }

    public bool IsFalling()
    {
        if (transform.position.y < currentY && transform.position.y > 0)
        {
            Debug.Log("falling");
            return true;
        } else return false;
    }

    //Implements death after falling for too long
    public IEnumerator DeadByFall()
    {
        if (IsFalling()) {
            yield return new WaitForSeconds(1.5f);
            CloudsDestroyer();
            Debug.Log("Died by fall");
            cam.gameObject.transform.parent = null;
            cam.transform.position = new Vector2(0f, 0f);
            this.gameObject.SetActive(false);
            gameObject.transform.position = new Vector3(0.0f,-4.3f,0.0f);
            CloudsGenerator.transform.position = new Vector2(0f, 2f);
            Points = CurrentPoints + PlayerPrefs.GetInt("All points");
            PlayerPrefs.SetInt("All points", Points);
            DiedByFall = true;
            ShowAd();
            NextPlay();
        }
    }

    //Clear clouds index after death
    public void CloudsDestroyer()
    {
        cloudstokill = GameObject.FindGameObjectsWithTag("Cloud");
        for(int i=0; i< cloudstokill.Length; i++)
        {
            Destroy(cloudstokill[i]);
        }
        CloudsGenerator.transform.position = new Vector3(0.0f, 1.0f);
    }
}
