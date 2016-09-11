//Initializes player position to the 'spawn box' which's located at the centre-bottom of the camera
using UnityEngine;

public class PlayerStartPos : MonoBehaviour {

    public new Transform transform;

    //Set player pos to the box, at the bottom centre of camera
    void Start () {
        transform.position = gameObject.transform.position;
    }
}
