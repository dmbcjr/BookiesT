using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace BookiesT
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        [SerializeField] Button btnPlaceBet;
        [SerializeField] Button btnMainMenu;
        [SerializeField] Button btnBetPanel;

        [SerializeField] public TMP_Dropdown fightTMP;
        [SerializeField] public TMP_Dropdown fighterTMP;
        [SerializeField] public TMP_InputField betAmountTMP;
        [SerializeField] public TMP_Dropdown punterTMP;

        [SerializeField] public GameObject betPanel;
        [SerializeField] public TextMeshProUGUI fundsText;
        [SerializeField] public TextMeshProUGUI betValidText;



        int betAmount, fundAmount;
        bool openBetPanel;
        //string currentFightNameSelection, currentUsernameSelection;

        private void Awake()
        {

            instance = this;


        }
        // Start is called before the first frame update
        void Start()
        {

            ClearStart();
            DataManager.Instance.ClearStart();

            UnityEngine.Assertions.Assert.IsNotNull(btnPlaceBet);
            btnPlaceBet.onClick.AddListener(delegate
            {
                SaveData.instance.MakeBetData();
            });

            UnityEngine.Assertions.Assert.IsNotNull(btnMainMenu);
            btnMainMenu.onClick.AddListener(LoadMainMenu);

            UnityEngine.Assertions.Assert.IsNotNull(punterTMP);
            punterTMP.onValueChanged.AddListener(delegate
            {
                DataManager.Instance.GetUserCredit();
                
            });


            UnityEngine.Assertions.Assert.IsNotNull(fightTMP);
            fightTMP.onValueChanged.AddListener(delegate
            {
                DataManager.Instance.LoadFighterData();
            });

            UnityEngine.Assertions.Assert.IsNotNull(btnBetPanel);
            btnBetPanel.onClick.AddListener(CloseBetValidMenu);



        }

        public void ClearStart()
        {
            ClearDropdowns();

        }

        public void OpenBetValidMenu()
        {
            betPanel.SetActive(true);
        }
        public void CloseBetValidMenu()
        {
            DataManager.Instance.ClearStart();
            betPanel.SetActive(false);
        }
        private void ClearDropdowns()
        {
            fightTMP.ClearOptions();
            fighterTMP.ClearOptions();
            punterTMP.ClearOptions();

        }
        private void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
        public bool CheckPunterFunds()
        {
            betAmount = string.IsNullOrEmpty(betAmountTMP.text) ? 0 : int.Parse(betAmountTMP.text);
            fundAmount = string.IsNullOrEmpty(fundsText.text) ? 0 : int.Parse(fundsText.text);

            if (betAmount > fundAmount)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}