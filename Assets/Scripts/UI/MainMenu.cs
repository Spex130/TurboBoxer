using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject mainParent;
        [SerializeField]
        private GameObject mainSelected;
        [SerializeField]
        private GameObject creditsParent;
        [SerializeField]
        private GameObject creditsSelected;

        private GameObject currentSelected;
        private bool inMain;
        private bool inCredits;

        void Start()
        {
            inMain = true;
			inCredits = false;
            EventSystem.current.SetSelectedGameObject(mainSelected);
        }

        void Update()
        {
			if (inMain) {
				if (Input.GetKey (KeyCode.Escape))
                {
                    if (inCredits)
                        GoToMain();
                    else
					    Application.Quit ();
                }
				if (inMain && EventSystem.current.currentSelectedGameObject == null) {
					if (inCredits)
						EventSystem.current.SetSelectedGameObject (creditsSelected);
					else
						EventSystem.current.SetSelectedGameObject (mainSelected);
				}

				currentSelected = EventSystem.current.currentSelectedGameObject;

				if (Util.CustomInput.BoolFreshPressDeleteOnRead (Util.CustomInput.UserInput.Up))
					Navigator.Navigate (Util.CustomInput.UserInput.Up, currentSelected);
				if (Util.CustomInput.BoolFreshPressDeleteOnRead (Util.CustomInput.UserInput.Down))
					Navigator.Navigate (Util.CustomInput.UserInput.Down, currentSelected);
				if (Util.CustomInput.BoolFreshPressDeleteOnRead (Util.CustomInput.UserInput.Accept))
					Navigator.CallSubmit ();
				if (inCredits && Util.CustomInput.BoolFreshPressDeleteOnRead (Util.CustomInput.UserInput.Cancel))
					GoToMain ();
			} 
        }
        
        public void GoToMain()
        {
            inMain = true;
			inCredits = false;
            mainParent.SetActive(true);
            creditsParent.SetActive(false);
            EventSystem.current.SetSelectedGameObject(mainSelected);
        }

        public void GoToCredits()
        {
            inMain = true;
            inCredits = true;
            mainParent.SetActive(false);
            creditsParent.SetActive(true);
            EventSystem.current.SetSelectedGameObject(creditsSelected);
        }

        public void Play()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("NodeLoadScene");
        }
    }
}