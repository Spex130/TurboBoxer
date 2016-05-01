using UnityEngine;
using System.Collections;

public class textSprite : MonoBehaviour {

    public Camera cam;
    public Transform mySprite;
    public SpriteRenderer mySpriteRenderer;
    public float transparencyValue = 1f;
    public Vector3 spriteSize = new Vector3(1, 1, 1);

    public float moveSpeed = .1f;
    public float invisDrainSpeed = .1f;

    // Use this for initialization
    void Start () {
        cam = GameObject.FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(cam.transform);
        transparencyValue -= invisDrainSpeed;
        transform.Translate(0, moveSpeed, 0);

        mySprite.localScale = spriteSize;
        mySpriteRenderer.color = new Color(1f, 1f, 1f, transparencyValue);

        if(transparencyValue <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
	}
}
