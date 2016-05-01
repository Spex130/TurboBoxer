using UnityEngine;
using System.Collections;
using Assets.Scripts.Boxing;
using UnityEngine.UI;

public class pushupScoreDisplay : MonoBehaviour {

    public pushupMinigameNode push;

	// Use this for initialization
	void Start () {
        push = GameObject.FindObjectOfType<pushupMinigameNode>();
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text = "Score: " + push.score.ToString();
	}
}
