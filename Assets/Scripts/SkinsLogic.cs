using UnityEngine;
using UnityEngine.UI;

public class SkinsLogic : MonoBehaviour {

    public Sprite[] Textures;
    public GameObject SkinButton;
    public Material PlayerMat;

	void Start () {
        int texIndex = 0;
	    foreach(Sprite texture in Textures)
        {
            GameObject container = Instantiate(SkinButton) as GameObject;
            container.GetComponent<Image>().sprite = texture;
            container.transform.SetParent(this.transform, false);

            int index = texIndex;

            container.GetComponent<Button>().onClick.AddListener(() => ChangeSkin(index));

            texIndex++;
        }
	}

    void ChangeSkin(int index)
    {
        float x = (index % 4)  * 0.25f;
        float y = (index / 4) * 0.25f;

        if (y == 0.0f)
            y = 0.75f;
        else if (y == 0.25f)
            y = 0.5f;
        else if (y == 0.50f)
            y = 0.25f;
        else if (y == 0.75f)
            y = 0.0f;

        PlayerMat.SetTextureOffset("_MainTex", new Vector2(x,y));
        SkinsManager.Instance.CurrentSkinIndex = index;
        SkinsManager.Instance.Save();
    }
	
}
