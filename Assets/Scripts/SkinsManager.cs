using UnityEngine;

public class SkinsManager : MonoBehaviour {

    private static SkinsManager instance;
    public static SkinsManager Instance { get { return instance; } }

    public int points, CurrentSkinIndex = 0;
    public int SkinAvailability = 1;

    void Start()
    {
        Object.DontDestroyOnLoad(gameObject);
        instance = this;
        if (PlayerPrefs.HasKey("CurrentSkin"))
        {
            CurrentSkinIndex = PlayerPrefs.GetInt("CurrentSkin");
            points = PlayerPrefs.GetInt("All points");
            SkinAvailability = PlayerPrefs.GetInt("SkinAvailability");
        }
        else
        {
            Save();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("CurrentSkin", CurrentSkinIndex);
        PlayerPrefs.SetInt("SkinAvailability", SkinAvailability);
        PlayerPrefs.SetInt("All points", points);
    }

}
