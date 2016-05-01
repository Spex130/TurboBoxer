using UnityEngine;

namespace Assets.Scripts.Boxing
{
    class CPU : Boxer
    {

        public enum CPUDifficulty { easy, medium, hard};

        public CPUDifficulty dif = CPUDifficulty.easy;

        public float attackTimer = 4f;
        private float attackTimerReset = 4f;
        public float attackSpeed = .1f;

        public float stateSwitchTimer = 0;
        private float stateTimerReset = 0;

        public int tiltRep;//Private/Protected is annoying and confusing, so this is so we can actually access those variables.

        void Start()
        {
            state = State.middle;
            reinit();
            setDifficulty();
        }

        void Update()
        {
            gamePlayloop();
        }

        public void setDifficulty()
        {
            if (dif == CPUDifficulty.easy)
            {
                attackTimer = 10f;
                attackTimerReset = attackTimer;

                stateSwitchTimer = 2.5f;
                stateTimerReset = 2.5f;
            }
            else if (dif == CPUDifficulty.medium)
            {
                attackTimer = 8f;
                attackTimerReset = attackTimer;

                stateSwitchTimer = 2f;
                stateTimerReset = 2f;
            }
            else if (dif == CPUDifficulty.hard)
            {
                attackTimer = 6f;
                attackTimerReset = attackTimer;

                stateSwitchTimer = 1f;
                stateTimerReset = 1f;
            }
        }

        public void gamePlayloop()
        {
            if (canMove)
            {
                if (state != State.dead)
                {
                    stateSwitchTimer -= Time.deltaTime;
                    if (stateSwitchTimer <= 0) { stateSwitchTimer = stateTimerReset; state = (State)Random.Range(0, 2); }

                    attackTimer -= attackSpeed;
                    if (attackTimer <= 0) { attackTimer = attackTimerReset; Attack(); }

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
        public void findPlayer()
        {
            enemy = GameObject.FindObjectOfType<Player>();
        }

        public void init()
        {
            reinit();
        }
    }
}
