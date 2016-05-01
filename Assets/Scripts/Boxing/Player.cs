using UnityEngine;

namespace Assets.Scripts.Boxing
{
    class Player : Boxer
    {
        private int points, enemyPoints;//This is your score.
        public int tiltRep;//This allows other things to find whether or not you are tilting in a specific direction.
        public bool isMale = true;// ARE YOU A BOY OR A GIRL??? /Oak

        public bool shouldWait = false;

        void Start()
        {
            init();
        }


        void FixedUpdate()
        {
            if (state == State.pushup)
            {

            }
            else {
                if (shouldWait)
                {
                    clearAnim();
                    anim.SetBool("Wait", true);
                    shouldWait = false;
                }
                if ((actionSpeed += Time.deltaTime) < actionSpeedLimit)
                    return;
                if (hit)
                {
                    if (health <= 0)
                    {
                        Lose();
                    }
                }
                else
                {
                    if (Util.CustomInput.BoolHeld(Util.CustomInput.UserInput.Left))
                        state = State.left;
                    else if (Util.CustomInput.BoolHeld(Util.CustomInput.UserInput.Right))
                        state = State.right;
                    else
                        state = State.middle;
                    if (Util.CustomInput.BoolHeld(Util.CustomInput.UserInput.Block))
                    {
                        if (!blocking)
                            anim.SetBool("BlockHold", true);
                        blocking = true;
                    }
                    else
                    {
                        if (blocking)
                            anim.SetBool("Wait", true);
                        blocking = false;
                    }

                    if (Util.CustomInput.BoolFreshPress(Util.CustomInput.UserInput.Action))
                    {
                        print("punch");
                        if (Attack())
                            points++;
                        if (enemy.health <= 0)
                            Win();
                    }
                    switch (state)
                    {
                        case State.left:
                            tiltRep = 0;
                            break;
                        case State.middle:
                            tiltRep = 1;
                            break;
                        case State.right:
                            tiltRep = 2;
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        void Win()
        {
            Debug.Log("Won");
            enemy.anim.SetBool("Knockout", true);
            enemy.animDone = false;
        }

        void Lose()
        {
            anim.SetBool("Knockout", true);
            canMove = false;
            animDone = false;
            Debug.Log("Lost");
        }

        public void startPushup()
        {
            state = State.pushup;
            canMove = false;
        }

        public void startWaiting()//Sometimes player classes are stupid and don't listen. This will make them listen.
        {
            print("wait!");
            clearAnim();
            state = State.middle;
            anim.SetBool("Wait", true);
            //shouldWait = true;
        }

        public void clearAnimations()
        {
            clearAnim();
        }

        public void findEnemy()
        {
            enemy = GameObject.FindObjectOfType<CPU>();
        }

        public void init()
        {
            
            state = State.middle;
            clearAnim();
            anim.SetBool("Wait", true);
            hit = false;
            points = 0;
            enemyPoints = 0;
            animDone = true;
            health = 15;
        }

    }
}
