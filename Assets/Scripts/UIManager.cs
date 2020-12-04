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

        [SerializeField] public TMP_Dropdown fightTMP;
        [SerializeField] public TMP_Dropdown fighterTMP;
        [SerializeField] public TextMeshProUGUI betAmountTMP;
        [SerializeField] public TMP_Dropdown punterTMP;

        [SerializeField] public TextMeshProUGUI userLabel;

        string currentFightNameSelection, currentUsernameSelection;

        private void Awake()
        {

            instance = this;


        }
        // Start is called before the first frame update
        void Start()
        {
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

            ClearStart();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ClearStart()
        {
            ClearDropdowns();

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

       
    }
}