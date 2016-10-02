using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScreenPointsInfo : MonoBehaviour {

    public Text EarnedPoints, AllPoints;
    public PlayerManager player;

	// Use this for initialization
	void Update () {
        EarnedPoints.text = "Earned points : " + player.CurrentPointsText.text.ToString();
        AllPoints.text = "All points : " + PlayerPrefs.GetInt("All points");

    }
	
}
