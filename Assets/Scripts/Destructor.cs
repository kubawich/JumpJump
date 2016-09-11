using UnityEngine;
using System.Collections;

public class Destructor : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Cloud" || col.tag == "Enemy")
            Destroy(col.gameObject);
    }
}
