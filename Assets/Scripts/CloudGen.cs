//Used for procedural clouds generation
//TODO: for optimalization make clouds stacking i mean if cloud isn't occluded by camera push it from lowest pos to high of stack
using UnityEngine;
using System.Collections.Generic;

public class CloudGen : MonoBehaviour {

    public GameObject Player;
    PlayerManager pl = new PlayerManager();
    public GameObject[] CloudsSprites = new GameObject[3];


    // Update is called once per frame
    void Update () {
        if (Player.transform.position.y > -1)
        {
            Generate();
            this.transform.Translate(0.0f, Random.Range(1.5f, 6.0f), 0.0f);
        }
    }

    //Simple instatiating procedural clouds with several random conditions for make it more "unexpectable" lol
    void Generate()
    {
        Instantiate(CloudsSprites[Random.Range(0, 2)], new Vector3(Random.Range(-3.1f, 3.1f), transform.position.y + 3), this.transform.rotation);
    }
}
