//Test of simpliest possible enemy, it's behaviour determins that it should bounce from one camera bound to another
using UnityEngine;
using System.Collections.Generic;

public class SimpleEnemy : MonoBehaviour {

    public  Camera cam;
    public GameObject Player, SimpleEnemyGO;
    List<GameObject> enemies = new List<GameObject>();
    Rigidbody2D body;

    void Start()
    {
        this.transform.Translate(new Vector2(cam.orthographicSize /-2, 0),0); 
        body = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Generate();
        Behaviour();
    }

    //Used for generating simple enemy class
    void Generate()
    {
       enemies.Add((GameObject)Instantiate(SimpleEnemyGO, new Vector3(this.transform.position.x, this.transform.position.y, 0.0f), this.transform.rotation));
    }

    //Determins simple enemy behaviour
    void Behaviour()
    {
        //Sets bounds of enemy movement to camera's x bounds, for more info look to 'start' method
        //transform.position.x = (Mathf.Clamp(transform.position.x, (float)-2.5, (float)2.5));
        
    }
}
