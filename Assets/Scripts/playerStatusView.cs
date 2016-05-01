using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.Boxing;

public class playerStatusView : MonoBehaviour {

    public Slider tiltSlider;
    //public Slider attackSlider;
    //public Slider stateSlider;
    public Slider healthSlider;

    [SerializeField]
    private Player player;

    private bool initNode = false;

    // Use this for initialization
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!initNode)
        {
            init();
            initNode = true;
        }
        tiltSlider.value = player.tiltRep;
        //attackSlider.value = player.attackTimer;
        //stateSlider.value = player.stateSwitchTimer;
        healthSlider.value = player.health;
    }

    public void init()
    {
        player = GameObject.FindObjectOfType<Player>();
        healthSlider.maxValue = player.health;
    }
}
