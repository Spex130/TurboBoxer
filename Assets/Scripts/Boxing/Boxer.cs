using UnityEngine;

namespace Assets.Scripts.Boxing
{
    abstract class Boxer : MonoBehaviour
    {
        public Boxer enemy;
        public Animator anim;
        public bool animDone;
        public bool canMove = true;//This controls whether or not the player can interact.
        public float actionSpeedLimit = 1;
        protected float actionSpeed = 1;

        protected enum State { middle, left, right, dead, pushup };
        [SerializeField]protected State state;
        protected bool hit;
        protected bool blocking;

        public int health = 15;

        protected void reinit()
        {
            anim.SetBool("Wait", true);
            clearAnim();
            health = 15;
        }

        protected void clearAnim()
        {
            anim.SetBool("DodgeLeft", false);
            anim.SetBool("DodgeRight", false);
            anim.SetBool("BlockHit", false);
            anim.SetBool("Hit", false);
            anim.SetBool("BlockHold", false);
            anim.SetBool("LeftPunchHi", false);
            anim.SetBool("RightPunchHi", false);
            anim.SetBool("Wait", false);
            hit = false;
            animDone = true;
        }

        protected bool Attack()
        {
            if (canMove)
            {
                if (state == State.left)
                {
                    anim.SetBool("LeftPunchHi", true);
                    actionSpeed = 0;
                }
                else if (state == State.middle)
                {
                    if (Random.Range(1, 10) % 2 == 0) //IF random number is even.
                    {
                        anim.SetBool("LeftPunchLow", true);
                        actionSpeed = 0;
                    }
                    else
                    {
                        anim.SetBool("RightPunchLow", true);
                        actionSpeed = 0;
                    }
                }
                else {
                    anim.SetBool("RightPunchHi", true);
                    actionSpeed = 0;
                }
                if (enemy.blocking && state == State.middle)
                    return false;
                if (state == enemy.state)
                {
                    enemy.Hit();
                    return true;
                }
                enemy.Dodge(state);
            }
            return false;
        }

        protected void Hit()
        {
            if (canMove)
            {
                hit = true;
                anim.SetBool("Hit", true);
                actionSpeed = 0;
                health--;
                if (health <= 0) { state = State.dead; canMove = false; }
            }
        }

        protected void Block()
        {
            if (canMove)
            {
                anim.SetBool("BlockHit", true);
                actionSpeed = 0;
            }
        }

        protected void Dodge(State state)
        {
            if (canMove)
            {
                if (state == State.left)
                    anim.SetBool("DodgeLeft", true);
                else
                    anim.SetBool("DodgeRight", true);
                actionSpeed = 0;
            }
        }
    }
}
