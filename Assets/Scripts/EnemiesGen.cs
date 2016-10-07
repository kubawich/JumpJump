using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemiesGen : MonoBehaviour {

    public GameObject Player, EnemyBounce;
    public AnimationClip anim;
  
    public List<Object> enemy = new List<Object>();

	
	// Update is called once per frame
	void Start() {

        GenerateNext();
    }

    public IEnumerator Generate()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 1.5f));
            enemy.Add(Instantiate(EnemyBounce, new Vector2(Random.Range(transform.position.x - 1.5f, transform.position.x + 1.5f), this.transform.position.y), this.transform.rotation));
        }
    }

    public void GenerateNext()
    {

        StartCoroutine(Generate());
    }
}

