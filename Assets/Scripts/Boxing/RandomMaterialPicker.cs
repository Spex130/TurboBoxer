using UnityEngine;

namespace Assets.Scripts.Boxing
{
    class RandomMaterialPicker : MonoBehaviour
    {
        public Material[] bodyMats;
        public Material[] clotheMats;
        public Material[] gloveMats;
        public Material[] bootMats;

        public GameObject[] bodys;
        public GameObject[] clothes;
        public GameObject[] gloves;
        public GameObject[] boots;

        void Start()
        {
            setToRand(bodys, bodyMats);
            setToRand(clothes, clotheMats);
            setToRand(gloves, gloveMats);
            setToRand(boots, bootMats);
        }

        private void setToRand(GameObject[] part, Material[] mats)
        {
            int rand = Random.Range(0, mats.Length);
            foreach (GameObject g in part)
                g.GetComponent<Renderer>().material = mats[rand];
        }
    }
}
