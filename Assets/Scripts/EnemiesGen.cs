using UnityEngine;
using System.Collections.Generic;

public class EnemiesGen : MonoBehaviour {

    public GameObject Player, EnemyBounce;
    public AnimationClip anim;
  
    public List<Object> enemy = new List<Object>();

	// Use this for initialization
	void Start () {
	    //anim.GetComponent<AnimationClip>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Player.transform.position.y > 2)
        {
            Generate();
            this.transform.Translate(0.0f, Random.Range(4.0f, 9.0f), 0.0f);
        }
    }

    void Generate()
    {
        enemy.Add(Instantiate(EnemyBounce, new Vector2(this.transform.position.x, this.transform.position.y), this.transform.rotation));
    }
}

