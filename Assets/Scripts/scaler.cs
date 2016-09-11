using UnityEngine;
using System.Collections;

public class scaler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    this.transform.localScale = new Vector3(17.75f,0.71f, 1f);
        this.transform.position = new Vector3(0f, -4.75f, 0f);
	}
}
