using UnityEngine;
using System.Collections;
using Assets.Scripts.Boxing;
using UnityEngine.UI;
using Assets.Scripts.Util;

public class fightSceneNode : MonoBehaviour {

    public Camera cam;
    private sceneNode fSceneNode;
    public enum fightGameState { start, play, end }
    public fightGameState fState = fightGameState.start;
    public playerManager pMan;

    [SerializeField] private CPU maleFighter;
    [SerializeField] private CPU femaleFighter;
    [SerializeField] private Player player;

    private CPU initializedEnemy;//This is so we know if WE ARE A BOY OR A GIRL. We can track ourselves.

    private Animator fightAnim;
    public GameObject enemyPos;//Where we put the CPUs.

    public bool allowEnd = false;
    public Text proceedText;
    private Animator anim;

    // Use this for initialization
    void Start () {
        init();
        gameLoop();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gameLoop();
	}

    public void init()
    {
        fSceneNode = GetComponent<sceneNode>();
        proceedText.text = "";
        cam = GameObject.FindObjectOfType<Camera>();
        cam.orthographic = false;
        pMan = GameObject.FindObjectOfType<playerManager>();

        //player related
        player = GameObject.FindObjectOfType<Player>();
        
        player.clearAnimations();
        player.startWaiting();
        player.init();
        player.canMove = false;
        anim = player.GetComponent<Animator>();

        fightAnim = GetComponent<Animator>();


        if (player.isMale)
        {
            maleFighter = GameObject.Instantiate<CPU>(maleFighter);
            maleFighter.init();
            maleFighter.transform.parent = this.transform;
            player.findEnemy();
            maleFighter.transform.position = enemyPos.transform.position;
            maleFighter.transform.rotation = enemyPos.transform.rotation;

            maleFighter.findPlayer();
            maleFighter.canMove = false;
            initializedEnemy = maleFighter;
        }
        else
        {
            femaleFighter = GameObject.Instantiate<CPU>(femaleFighter);
            femaleFighter.init();
            femaleFighter.transform.parent = this.transform;
            player.findEnemy();
            femaleFighter.transform.position = enemyPos.transform.position;
            femaleFighter.transform.rotation = enemyPos.transform.rotation;

            femaleFighter.findPlayer();
            femaleFighter.canMove = false;
            initializedEnemy = maleFighter;
        }

        pMan.round++;

        if(pMan.round >(int)(pMan.maxRounds*25))
        {
            initializedEnemy.dif = CPU.CPUDifficulty.medium;
        }
        if (pMan.round > (int)(pMan.maxRounds*.75))
        {
            initializedEnemy.dif = CPU.CPUDifficulty.hard;
        }

        player.findEnemy();
        anim.SetBool("Wait", true);


    }

    //Where the magic happens. This is the main loop that happens in the main loop.
    public void gameLoop()
    {
        if(player.enemy == null)
        {
            player.findEnemy();
        }
        switch (fState)
        {
            case fightGameState.start:
                break;
            case fightGameState.play:
                if(player.health <=0 || femaleFighter.health <= 0 || maleFighter.health <= 0)
                {
                    switchState(fightGameState.end);
                }
                break;
            case fightGameState.end:
                if (CustomInput.BoolFreshPress(CustomInput.UserInput.Action))
                {
                    if (allowEnd)
                    {
                        if (pMan.round < pMan.maxRounds)
                        {
                            fSceneNode.activateNextNode(0);
                        }
                        else
                        {
                            //YOU WON THE GAME. CONG-RATIONS. *Clap clap clap*
                            fSceneNode.isTrueLoad = true;
                            fSceneNode.activateNextNode(1);
                        }
                    }
                }
                break;
        }
    }

    //This is so we can swap states at all.
    public void switchState(fightGameState fgState)
    {
        fState = fgState;

        switch (fgState)
        {
            case fightGameState.start:
                player.init();
                player.startPushup();
                player.startWaiting();
                break;
            case fightGameState.play:
                player.canMove = true;
                maleFighter.canMove = true;
                femaleFighter.canMove = true;
                player.clearAnimations();
                break;
            case fightGameState.end:
                player.canMove = false;
                maleFighter.canMove = false;
                femaleFighter.canMove = false;
                fightAnim.SetTrigger("Finish!");
                break;
        }
    }

    //lets the user click forward.
    public void allowGameEnd()
    {
        proceedText.text = "Press to proceed!";
        allowEnd = true;
    }

}
