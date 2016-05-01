using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts.Boxing
{
    public class CPUStatusView : MonoBehaviour
    {

        public Slider tiltSlider;
        public Slider attackSlider;
        public Slider stateSlider;
        public Slider healthSlider;

        [SerializeField]private CPU enemy;

        private bool initNode = false;

        // Use this for initialization
        void Start()
        {
            //init();
        }

        // Update is called once per frame
        void Update()
        {
            if (!initNode)
            {
                init();
                initNode = true;
            }
            tiltSlider.value = enemy.tiltRep;
            attackSlider.value = enemy.attackTimer;
            stateSlider.value = enemy.stateSwitchTimer;
            healthSlider.value = enemy.health;
        }

        public void init()
        {
            enemy = GameObject.FindObjectOfType<CPU>();
            attackSlider.maxValue = enemy.attackTimer;
            stateSlider.maxValue = enemy.stateSwitchTimer;
            healthSlider.maxValue = enemy.health;
        }
    }
}
