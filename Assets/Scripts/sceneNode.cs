using UnityEngine;
using System.Collections;
using Assets.Scripts.Boxing;

public class sceneNode : MonoBehaviour {

    Camera cam;

    public bool isStartingNode = false;
    public GameObject playerPos;
    public GameObject camPos;

    public enum camProjection { ortho, persp}
    public camProjection camPro;

    //public GameObject

    public sceneNode[] nextNodeArray;

	// Use this for initialization
	void Start () {
	
	}

    public void initGame()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void activateNextNode(int id)
    {
        sceneNode temp = GameObject.Instantiate<sceneNode>(nextNodeArray[id]);
        temp.transform.position = new Vector3(0, 0, 0);
        Player player = GameObject.FindObjectOfType<Player>();
        player.transform.position = temp.playerPos.transform.position;
        player.transform.rotation = temp.playerPos.transform.rotation;

        cam = GameObject.FindObjectOfType<Camera>();
        cam.transform.position = temp.camPos.transform.position;
        cam.transform.rotation = temp.camPos.transform.rotation;

        if(camPro == camProjection.ortho)
        {
            cam.orthographic = false;
        }
        else
        {
            cam.orthographic = true;
        }

        //cam.ResetProjectionMatrix();
        Destroy(this.gameObject);
    }
}
