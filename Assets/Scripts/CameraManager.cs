//Controlls camera position relatively to player
using UnityEngine;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{

    public GameObject player;
    public new Camera camera;
    public GameObject floor;
    public List<Color> Colors = new List<Color>();
    bool[] stages = new bool[3];
    float timer;

    void Start()
    {
        camera.backgroundColor = Colors[0];
    }

    void Update()
    {
        this.camera.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        this.camera.nearClipPlane = -2f;
        CameraPositioning();
        BackgroudColor();
    }

    //Used for poperly translate camera's position based on player's height
    void CameraPositioning()
    {
        //First of all camera checks player height to decide do follow it
        if (player.transform.position.y >= 0.0f)
            this.gameObject.transform.parent = player.transform;
        //If player fall and it's height is less than 0.5 detach camera from player
        else if (player.transform.position.y <= 0.5f)
            this.gameObject.transform.parent = null;
        //Detach floor from camera if it's starts following player
        if (player.transform.position.y >= 0.0f)
            floor.transform.parent = null;
        //It's fix physics issues with too slow calculating cam. pos. and sets floor accurately on cam's bottom
        if (this.gameObject.transform.position.y <= 0.0f)
            this.gameObject.transform.position = new Vector3(0.0f, 0.0f, -10.0f);
    }

    //Changes background color dependent on player's position
    void BackgroudColor()
    {
        if (player.transform.position.y <= 10)
        {
            camera.backgroundColor = Color.Lerp(Colors[0], Colors[1], 5);
            stages[0] = true;
        }
        else if (player.transform.position.y >= 16 )
        {
            camera.backgroundColor = Color.Lerp(Colors[1], Colors[2], 5);
        }
    }
}
