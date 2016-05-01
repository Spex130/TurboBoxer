using UnityEngine;
using System.Collections;
using Assets.Scripts.Boxing;
using Assets.Scripts.Util;
using UnityEngine.UI;

namespace Assets.Scripts.Boxing
{
    public class pushupMinigameNode : MonoBehaviour
    {
        public Camera cam;
        private sceneNode pmSceneNode;


        public enum pushupGameState { start, play, end}
        public pushupGameState gState = pushupGameState.start;
        private Animator minigameAnim;

        [SerializeField] private Player player;
        private Animator anim;
        public Slider slider;
        private float animLength;

        //Generate HIT/MISS from this point
        public GameObject effectsPoint;
        public textSprite hitSprite;
        public textSprite missSprite;

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

        public bool pushupSuccess = true;

        public float minigameTimer = 7f;
        private float minigameTimerReset;
        public Text timerText;
        public Text proceedText;

        public int score = 0;

        public bool allowEnd = false;

        // Use this for initialization
        void Start()
        {
            pmSceneNode = GetComponent<sceneNode>();
            minigameTimerReset = minigameTimer;
            init();
        }

        public void init()
        {
            cam = GameObject.FindObjectOfType<Camera>();
            player = GameObject.FindObjectOfType<Player>();
            timerReset = timer;
            minigameTimer = minigameTimerReset;
            //Set the PUSHUP ANIMATION TO PLAY.

            minigameAnim = GetComponent<Animator>();
            anim = player.GetComponent<Animator>();
            player.clearAnimations();
            anim.SetBool("Pushup", true);
            player.startPushup();
            score = 0;

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

            //Make sure allowEnd is false
            allowEnd = false;
            //Empty out the proceedText.
            proceedText.text = "";
        }

        public void gameLoop()
        {
            switch (gState)
            {

                case pushupGameState.start:
                    anim.SetTime(0);
                    frameThresLoop();
                    timerText.text = minigameTimer.ToString() + " Sec.";
                    break;

                case pushupGameState.play:
                    if (minigameTimer > 0)
                    {
                        player.startPushup();
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
                            anim.SetTime((animLength / frameCount) * Mathf.Abs(frameCount));
                        }
                        else
                        {
                            slider.value = sliderTrackpoint;
                            anim.SetTime((animLength / frameCount) * Mathf.Abs(sliderTrackpoint));
                        }
                        frameThresLoop();
                        inputCheck();
                        minigameTimer -= Time.deltaTime;
                        timerText.text = Mathf.Round(minigameTimer).ToString() + " Sec.";
                    }
                    else
                   {
                        minigameAnim.SetTrigger("Finish!");
                        switchState(pushupGameState.end);
                    }
                    break;

                case pushupGameState.end:
                    anim.SetTime((animLength / frameCount) * Mathf.Abs(sliderTrackpoint));
                    inputCheck();
                    break;
            }
        }

        public void frameThresLoop()
        {
            leftThres.transform.localPosition = new Vector3(-((140 / 20) * frameThres), leftThres.transform.localPosition.y);
            rightThres.transform.localPosition = new Vector3(((140 / 20) * frameThres), rightThres.transform.localPosition.y);
        }

        // Update is called once per frame
        void Update()
        {
            gameLoop();
            
        }

        public void inputCheck()
        {
            switch (gState)
            {
                case pushupGameState.play:
                if (sliderTrackpoint < frameThres && sliderTrackpoint > -frameThres)
                {
                    if (CustomInput.BoolFreshPress(CustomInput.UserInput.Action))
                    {
                        if (!pushupSuccess)//IF WE HAVE NOT SUCCEEDED THIS ATTEMPT
                        {
                            textSprite hit = GameObject.Instantiate<textSprite>(hitSprite);
                            hit.transform.position = effectsPoint.transform.position;
                            inverter *= -1;//Reverse Direction
                            pushupSpeed *= 1.2f;//speed up
                            pushupSuccess = true;//Prevent spam
                                if (frameThres > 1) { frameThres -= .2f; }

                        }
                        else
                        {
                            textSprite miss = GameObject.Instantiate<textSprite>(missSprite);
                            miss.transform.position = effectsPoint.transform.position;
                        }

                    }
                }
                else
                {
                    pushupSuccess = false;
                    if (CustomInput.BoolFreshPress(CustomInput.UserInput.Action))
                    {
                        textSprite miss = GameObject.Instantiate<textSprite>(missSprite);
                        miss.transform.position = effectsPoint.transform.position;
                        //miss.cam = cam;
                    }
                }
                    break;

                case pushupGameState.end:
                    if (CustomInput.BoolFreshPress(CustomInput.UserInput.Action))
                    {
                        if (allowEnd)
                        {
                            player.init();
                            pmSceneNode.activateNextNode(0);
                        }
                    }
                        break;
            }
        }

        public void switchState(pushupGameState pgState)
        {
            gState = pgState;
        }

        //lets the user click forward.
        public void allowGameEnd()
        {
            proceedText.text = "Press to proceed to the fight!";
            allowEnd = true;
        }
    }
}