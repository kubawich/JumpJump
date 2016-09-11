//Additional UI's management
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public PlayerManager player = new PlayerManager();
    public Text CurrentPointsText = null;
    public Button StartButton, ShopButton, SkinsButton, BackArrow = null;
    public GameObject ShopHandler;

	void Start () {
        player.GetComponent<PlayerManager>();
        CurrentPointsText.canvasRenderer.gameObject.SetActive(false);
        ShopHandler.gameObject.SetActive(false);
    }
	
	void Update () {
        CurrentPointsText.text = player.CurrentPoints.ToString();
        if (player.Hits == 2)
        {
            StartButton.gameObject.SetActive(true);
            ShopButton.gameObject.SetActive(true);
        }
        if (StartButton.IsActive() && SkinsButton.IsActive()) SkinsButton.gameObject.SetActive(false);
        else SkinsButton.gameObject.SetActive(true);
	}
}
