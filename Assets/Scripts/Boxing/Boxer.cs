using UnityEngine;

namespace Assets.Scripts.Boxing
{
    abstract class Boxer : MonoBehaviour
    {
        public Boxer enemy;
        public Animator anim;
        public bool animDone;
        public float actionSpeedLimit = 1;
        protected float actionSpeed = 1;

        protected enum State { middle, left, right };
        protected State state;
        protected bool hit;
        protected bool blocking;

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
            animDone = true;
        }

        protected bool Attack()
        {
            if (state == State.left) {
                anim.SetBool("LeftPunchHi", true);
                actionSpeed = 0;
            }
            else if (state == State.middle) {
                if (Random.Range(1, 10)%2 == 0) //IF random number is even.
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
            return false;
        }

        protected void Hit()
        {
            hit = true;
            anim.SetBool("Hit", true);
            actionSpeed = 0;
        }

        protected void Block()
        {
            anim.SetBool("BlockHit", true);
            actionSpeed = 0;
        }

        protected void Dodge(State state)
        {
            if (state == State.left)
                anim.SetBool("DodgeLeft", true);
            else
                anim.SetBool("DodgeRight", true);
            actionSpeed = 0;
        }
    }
}
