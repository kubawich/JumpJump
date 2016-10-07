using UnityEngine;
using UnityEngine.UI;

public class SkinsManager : MonoBehaviour {

    private static SkinsManager instance;
    public static SkinsManager Instance { get { return instance; } }

    public int points, SkinAvailability, CurrentSkinIndex;

    void Awake()
    {
        this.GetComponentInChildren<Text>().text ="You have " + PlayerPrefs.GetInt("All points").ToString() + " coins";
        instance = this;
        if (PlayerPrefs.HasKey("CurrentSkin"))
        {
            CurrentSkinIndex = PlayerPrefs.GetInt("CurrentSkin");
            points = PlayerPrefs.GetInt("All points");
            SkinAvailability = PlayerPrefs.GetInt("SkinAvailability");
        }
        else
        {
            PlayerPrefs.SetInt("CurrentSkin", 0);
            PlayerPrefs.SetInt("SkinAvailability", 1);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("CurrentSkin", 0);
        PlayerPrefs.SetInt("SkinAvailability", 1);
    }

}
