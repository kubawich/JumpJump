using UnityEngine;
using UnityEngine.UI;

public class SkinsLogic : MonoBehaviour {

    public Sprite[] Textures;
    public GameObject SkinButton;

	void Start () {
	    foreach(Sprite texture in Textures)
        {
            GameObject container = Instantiate(SkinButton) as GameObject;
            container.GetComponent<Image>().sprite = texture;
            container.transform.SetParent(this.transform, false);
        }
	}
	
}
