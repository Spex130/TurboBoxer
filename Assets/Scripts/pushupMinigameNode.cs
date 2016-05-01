using UnityEngine;
using System.Collections;
using Assets.Scripts.Boxing;
using UnityEngine.UI;


public class pushupMinigameNode : MonoBehaviour {

    [SerializeField] private Boxer player;
    private Animator anim;
    public Slider slider;
    private float animLength;

    //Pushup variables
    public float pushupSpeed = 20f;
    public float timer = 2;
    public float timerReset;

    //Gameplay middle input threshold.
    public Image leftThres;
    public Image rightThres;
    public float frameThres = 4;

    public int frameCount = 19;//Used to signify the amount of frames tha animation has.
    public float sliderTrackpoint = 0;//This is used to place the progress of the slider.
    private int inverter = 1;//We use this to influence which direction the sliderTrackpoint is moving.

    // Use this for initialization
    void Start () {
        init();
	}

    public void init()
    {
        timerReset = timer;
        //Set the PUSHUP ANIMATION TO PLAY.
        anim = player.GetComponent<Animator>();
        anim.SetBool("Pushup", true);

        //Find the PUSHUP Animation so we can get its length.
        RuntimeAnimatorController ac = anim.runtimeAnimatorController;    //Get Animator controller
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == "Pushup")
            {        //If it has the same name as your clip
                animLength = ac.animationClips[i].length;
            }
        }

        //Set the Framecounts so we can handle this via Frame Numbers instead of stupid arbitrarily tiny floats.
        slider.minValue = -frameCount;
        slider.maxValue = frameCount;

        
    }

    public void gameLoop()
    {
        timer -= Time.deltaTime * pushupSpeed;
        if (timer <= 0)
        {
            timer = timerReset;
            sliderTrackpoint += inverter;
            if (sliderTrackpoint >= frameCount || sliderTrackpoint <= -frameCount)
            {
                inverter = inverter * -1;
            }
        }
        if (sliderTrackpoint <= 2 && sliderTrackpoint >= -2)
        {
            slider.value = sliderTrackpoint;
            anim.SetTime((animLength / frameCount) * Mathf.Abs(18));
        }
        else
        {
            slider.value = sliderTrackpoint;
            anim.SetTime((animLength / frameCount) * Mathf.Abs(sliderTrackpoint));
        }
        frameThresLoop();
        
    }
	
    public void frameThresLoop()
    {
        leftThres.transform.localPosition = new Vector3(-((140 / 20) * frameThres), leftThres.transform.localPosition.y);
        rightThres.transform.localPosition = new Vector3(((140 / 20) * frameThres), rightThres.transform.localPosition.y);
    }

	// Update is called once per frame
	void Update () {
        gameLoop();
	}

    public void inputCheck()
    {
        if (sliderTrackpoint < frameThres && sliderTrackpoint > -frameThres)
        {

        }
    }
}
