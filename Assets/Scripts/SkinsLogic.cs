using UnityEngine;
using UnityEngine.UI;

public class SkinsLogic : MonoBehaviour {

    public Sprite[] Textures;
    public GameObject SkinButton;
    public Text shoppointstext;
    public Material PlayerMat;

	void Start () {
        shoppointstext.text = "You have " + SkinsManager.Instance.points.ToString() + " coins";
        int texIndex = 0;
	    foreach(Sprite texture in Textures)
        {
            GameObject container = Instantiate(SkinButton) as GameObject;
            container.GetComponent<Image>().sprite = texture;
            container.transform.SetParent(this.transform, false);

            int index = texIndex;

            container.GetComponent<Button>().onClick.AddListener(() => ChangeSkin(index));
            if((SkinsManager.Instance.SkinAvailability & 1 << index) == 1 << index)
            container.transform.GetChild(0).gameObject.SetActive(false);

            texIndex++;
        }
	}

    void ChangeSkin(int index)
    {
        if ((SkinsManager.Instance.SkinAvailability & 1 << index) == 1 << index)
        {
            float x = (index % 4) * 0.25f;
            float y = (index / 4) * 0.25f;

            if (y == 0.0f)
                y = 0.75f;
            else if (y == 0.25f)
                y = 0.5f;
            else if (y == 0.50f)
                y = 0.25f;
            else if (y == 0.75f)
                y = 0.0f;

            PlayerMat.SetTextureOffset("_MainTex", new Vector2(x, y));
            SkinsManager.Instance.CurrentSkinIndex = index;
            SkinsManager.Instance.Save();
        }
        else
        {
            //Buyin mechanic
            int cost = 10000;

            if(SkinsManager.Instance.points >= cost)
            {
                SkinsManager.Instance.points -= cost;
                SkinsManager.Instance.SkinAvailability += 1 << index;
                SkinsManager.Instance.Save();
                this.gameObject.transform.GetChild(index).GetChild(0).gameObject.SetActive(false);
                shoppointstext.text = "You have " + SkinsManager.Instance.points.ToString() + " coins";
                ChangeSkin(index);
            }
        }
        
    }
	
}
