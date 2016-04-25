using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.Boxing;

namespace Assets.Scripts.Boxing
{
    public class playerSetupScript : MonoBehaviour
    {

        public Dropdown genderDrop;
        public Dropdown skinDrop;
        public Dropdown clothDrop;
        public Dropdown bootDrop;
        public Dropdown gloveDrop;

        [SerializeField] private Boxer player;
        public playerColorSelector playercolor;

        [SerializeField]private Boxer[] genderList;

        // Use this for initialization
        void Start()
        {
            populateDropdowns();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void populateDropdowns()
        {
            //Skin setup
            skinDrop.ClearOptions();
            List<string> options = new List<string>();
            for (int i = 0; i < playercolor.bodyMats.Length; i++)
            {
                options.Add(playercolor.bodyMats[i].name);
            }
            skinDrop.AddOptions(options);

            //Clothes Setup
            clothDrop.ClearOptions();
            options.Clear(); ;
            for (int i = 0; i < playercolor.clotheMats.Length; i++)
            {
                options.Add(playercolor.clotheMats[i].name);
            }
            clothDrop.AddOptions(options);

            //Boots setup
            bootDrop.ClearOptions();
            options.Clear();
            for (int i = 0; i < playercolor.bootMats.Length; i++)
            {
                options.Add(playercolor.bootMats[i].name);
            }
            bootDrop.AddOptions(options);

            //Glove Setup
            gloveDrop.ClearOptions();
            options.Clear();
            for (int i = 0; i < playercolor.gloveMats.Length; i++)
            {
                options.Add(playercolor.gloveMats[i].name);
            }
            gloveDrop.AddOptions(options);

        }

        public void genderSwap(int index)
        {
            if(index == 0)
            {
                genderList[1].gameObject.SetActive(false);
                genderList[0].gameObject.SetActive(true);
                player = genderList[index];
                playercolor = genderList[index].GetComponent<playerColorSelector>();
            }

            else
            {
                genderList[0].gameObject.SetActive(false);
                genderList[1].gameObject.SetActive(true);
                player = genderList[index];
                playercolor = genderList[index].GetComponent<playerColorSelector>();
            }
        }

        public void applyChoices()
        {
            genderSwap(genderDrop.value);
            setMaterial(playercolor.bodys, playercolor.bodyMats, skinDrop);
            setMaterial(playercolor.clothes, playercolor.clotheMats, clothDrop);
            setMaterial(playercolor.gloves, playercolor.gloveMats, gloveDrop);
            setMaterial(playercolor.boots, playercolor.bootMats, bootDrop);
        }

        private void setMaterial(GameObject[] part, Material[] mats, Dropdown drop)
        {
            foreach (GameObject g in part)
                g.GetComponent<Renderer>().material = mats[drop.value];
        }
    }
}