﻿//Used to manage all player main stuff, like jumping, points etc.
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[System.Runtime.InteropServices.Guid("E6C25675-EE97-40FA-A46B-5353BC474A0C")]
public class PlayerManager : MonoBehaviour
{
    //Actuall player dependent variables
    public bool DiedByFall, IsGameOver;
    new Rigidbody2D rigidbody;
    public float JumpHeight, currentY;
    public int Hits, CurrentPoints, Points, PointsForGameOverScreen;
    //Variables for UI and other graphics magic
    public ParticleSystem PointsParticle;
    public Material particleMaterial = null;
    public Texture[] pointsParticleTexture = new Texture[10];
    public Text CurrentPointsText;
    public Button StartButton, ShopButton;
    public Image Logo;
    public Camera cam;
    public GameObject[] cloudstokill, enemiestokill;
    public GameObject CloudsGenerator, EnemiesGenerator, GameOverScreen, StartUIHandler, ShopUIHandler;
    public EnemiesGen enemies;
    public SkinsManager sm;

    //Initializes all variables
    void Awake()
    {
        gameObject.SetActive(false);
        Points = PlayerPrefs.GetInt("All points");
    }

    //Initialize after first rebirth
    void Start()
    {
        //General
        IsGameOver = false;
        this.gameObject.SetActive(true);
        DiedByFall = false;
        //Graphics stuff
        QualitySettings.vSyncCount = 0;
        //Saving stuff
        rigidbody = this.GetComponent<Rigidbody2D>();
        Hits = 0;
        StartDemandingCroutines();
        //UI stuff
        StartUIHandler.transform.position = new Vector2(8, 0);
        ShopUIHandler.transform.position = new Vector2(8, 0);
        if (!PlayerPrefs.HasKey("All points"))
            PlayerPrefs.SetInt("All points", 0);
    }

    //Initialize after each another rebirth
    public void NextPlay()
    {
        IsGameOver = false;
        GameOverScreen.SetActive(false);
        cam.backgroundColor = Random.ColorHSV();
        StartButton.gameObject.SetActive(true);
        ShopButton.gameObject.SetActive(true);
        Logo.gameObject.SetActive(true);
        Hits = 0;
        enemies.StartCoroutine(enemies.Generate());
        if (!StartButton.IsActive() || !ShopButton.IsActive()) this.gameObject.SetActive(true);
    }

    void Update()
    {
        Jump();
        IsFalling();
        StartCoroutine(DeadByFall());
    }

    //Jump logic
    public bool Jump()
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
                return true;
            }
        }
        //Jump logic for keyboard input || Only for dev tests
        if (Input.GetButtonDown("Jump"))
        {
            rigidbody.velocity = new Vector2(0, JumpHeight);
            CurrentPoints++;
            EmitParticle();
            currentY = this.transform.position.y;
            return true;
        }
        return false;
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
            IsGameOver = true;
            PointsForGameOverScreen = CurrentPoints;
            gameObject.transform.position = new Vector3(0.0f, -4.3f, 0.0f);
            cam.gameObject.transform.parent = null;
            cam.gameObject.SetActive(true);
            gameObject.SetActive(false);
            enemies.StopAllCoroutines();
            CloudsGenerator.transform.position = new Vector2(0f, 2f);
            EnemiesGenerator.transform.position = new Vector2(0f, 2f);
            cam.transform.position = new Vector2(0f, 0f);
            Hits = 0;
            CloudsDestroyer();
            EnemiesDestroyer();
            //Saving stuff
            if (PlayerPrefs.HasKey("All points"))
                Points = CurrentPoints + PlayerPrefs.GetInt("All points");
            else Debug.Log("error, no key");
            PlayerPrefs.SetInt("All points", Points);
            sm.points = Points;
            CurrentPoints = 0;
            //UI stuff
            CurrentPointsText.canvasRenderer.gameObject.SetActive(false);
            if (IsGameOver)
                GameOverScreen.SetActive(true);
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
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
            Debug.Log("Ad's playin'");
        }
        else Start();
    }

    //Checks if player's fallin'
    public bool IsFalling()
    {
        if (transform.position.y < currentY && transform.position.y > 0)
        {
            Debug.Log("falling");
            return true;
        }
        else return false;
    }

    //Implements death after falling for too long
    public IEnumerator DeadByFall()
    {
        if (IsFalling() && !Jump())
        {
            yield return new WaitForSeconds(1.5f);
            CloudsDestroyer();
            EnemiesDestroyer();
            IsGameOver = true;
            PointsForGameOverScreen = CurrentPoints;
            cam.gameObject.transform.parent = null;
            cam.transform.position = new Vector2(0f, 0f);
            this.gameObject.SetActive(false);
            gameObject.transform.position = new Vector3(0.0f, -4.3f, 0.0f);
            enemies.StopAllCoroutines();
            CurrentPointsText.canvasRenderer.gameObject.SetActive(false);
            CloudsGenerator.transform.position = new Vector2(0f, 2f);
            EnemiesGenerator.transform.position = new Vector2(0f, 2f);
            if (PlayerPrefs.HasKey("All points"))
                Points = CurrentPoints + PlayerPrefs.GetInt("All points");
            else Debug.Log("Error, no key");
            PlayerPrefs.SetInt("All points", Points);
            sm.points = Points;
            CurrentPoints = 0;
            Hits = 0;
            DiedByFall = true;

            int range = Random.Range(0, 2);
            if (range == 1)
                ShowAd();

            if (IsGameOver) GameOverScreen.SetActive(true);
        }
    }

    //Clear clouds index after death
    public void CloudsDestroyer()
    {
        cloudstokill = GameObject.FindGameObjectsWithTag("Cloud");
        for (int i = 0; i < cloudstokill.Length; i++)
        {
            Destroy(cloudstokill[i]);
        }
        CloudsGenerator.transform.position = new Vector3(0.0f, 1.0f);
    }

    public void EnemiesDestroyer()
    {
        enemiestokill = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemiestokill.Length; i++)
        {
            Destroy(enemiestokill[i]);
        }
        EnemiesGenerator.transform.position = new Vector3(0.0f, 1.0f);
    }

    public void SetStartUI(int x)
    {
        StartUIHandler.transform.position = new Vector2(x, 0);
        ShopUIHandler.transform.position = new Vector2(x, 0);
    }

    public void StartDemandingCroutines()
    {
        enemies.StartCoroutine(enemies.Generate());
    }
}
