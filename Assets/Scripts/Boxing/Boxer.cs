using UnityEngine;

namespace Assets.Scripts.Boxing
{
    abstract class Boxer : MonoBehaviour
    {
        public Boxer enemy;
        public Animator anim;
        public bool animDone;

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
            if (state == State.left || state == State.middle)
                anim.SetBool("LeftPunchHi", true);
            else
                anim.SetBool("RightPunchHi", true);
            if (state == enemy.state)
            {
                if (enemy.blocking && state == State.middle)
                    return false;
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
            animDone = false;
        }

        protected void Block()
        {
            anim.SetBool("BlockHit", true);
            animDone = false;
        }

        protected void Dodge(State state)
        {
            if (state == State.left)
                anim.SetBool("DodgeLeft", true);
            else
                anim.SetBool("DodgeRight", true);
            animDone = false;
        }
    }
}
