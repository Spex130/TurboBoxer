using UnityEngine;

namespace Assets.Scripts.Boxing
{
    class CPU : Boxer
    {
        void Start()
        {
            state = State.middle;
            anim.SetBool("Wait", true);
        }

        void Update()
        {

        }

    }
}
