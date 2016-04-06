using UnityEngine;

namespace Assets.Scripts.Boxing
{
    class Player : Boxer
    {
        private int points, enemyPoints;

        void Start()
        {
            state = State.middle;
            hit = false;
            points = 0;
            enemyPoints = 0;
            animDone = true;
        }

        void Update()
        {
            if ((actionSpeed += Time.deltaTime)<actionSpeedLimit)
                return;
            if (hit)
            {
                enemyPoints++;
                if (enemyPoints >= 15)
                    Lose();
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
                    if(!blocking)
                        anim.SetBool("BlockHold", true);
                    blocking = true;
                }
                else
                {
                    if(blocking)
                        anim.SetBool("Wait", true);
                    blocking = false;
                }

                if (Util.CustomInput.BoolFreshPress(Util.CustomInput.UserInput.Action))
                {
                    if (Attack())
                        points++;
                    if (points >= 15)
                        Win();
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
            animDone = false;
            Debug.Log("Lost");
        }
    }
}
