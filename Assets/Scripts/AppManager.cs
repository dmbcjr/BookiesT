using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System.Linq;

public class AppManager : MonoBehaviour
{
    private List<FightData> fights = new List<FightData>();
    private List<UserData> users = new List<UserData>();
   

    private List<string> fightList = new List<string>();
    private List<string> userList = new List<string>();
    private List<string> oppoList = new List<string>();

    [SerializeField] private TMP_Dropdown fightDropdown;
    [SerializeField] private TMP_Dropdown oppoDropdown;
    [SerializeField] private TMP_Dropdown punterDropdown;
    [SerializeField] private TextMeshProUGUI userLabel;

    int countLink = 0;
    string currentFightNameSelection;
    string currentUsernameSelection;

    //public TMP_Text
    private void Start()
    {
        ClearDropdowns();

        ReadFightData();
        LoadFightData();
        LoadOppoData();

        ReadUserData();
        LoadUserData();
        GetUserCredit();
    }

    

    public void NextLevelButton(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    private void ClearDropdowns()
    {

        fightDropdown.ClearOptions();
        oppoDropdown.ClearOptions();
        punterDropdown.ClearOptions();

    }
    private void ReadFightData()
    {
        //read in csv of all fight data
        TextAsset fightData = Resources.Load<TextAsset>("fightdata");
        string[] data = fightData.text.Split(new char[] { '\n' });
        
        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

            if (row[1] != "")
            {
                FightData f = new FightData();

                int.TryParse(row[0], out f.fightID);
                f.fightDescription = row[1];
                f.fighterA = row[2];
                f.fighterB = row[3];

                fights.Add(f);
            }
        }
        //Debug.Log(fights.Count());
        //
        
    }
    private void ReadUserData()
    {
      
        //read in csv of all fight data
        TextAsset userData = Resources.Load<TextAsset>("punterdata");
        string[] data = userData.text.Split(new char[] { '\n' });

        
        for (int i = 1; i < data.Length - 1; i++)
        {
            
            string[] row = data[i].Split(new char[] { ',' });
            
            if (row[1] != "")
            {
                
                UserData u = new UserData();
                
                int.TryParse(row[0], out u.userID);
                u.userName = row[1];
                int.TryParse(row[2], out u.userCredit);
                int.TryParse(row[3], out u.betsMade);
               
                users.Add(u);
                
            }
            

        }
        //Debug.Log(users.Count());

        //
        
    }

    private void LoadFightData()
    {
        
        foreach (FightData f in fights)
        {
            //Debug.Log(f.fightDescription + "\nA:" + f.fighterA + "  B:" + f.fighterB);
            fightList.Add(f.fightDescription);

        }
        fightDropdown.AddOptions(fightList);
       
    }
    private void LoadUserData()
    {

        foreach (UserData u in users)
        {
            //Debug.Log(f.fightDescription + "\nA:" + f.fighterA + "  B:" + f.fighterB);
            userList.Add(u.userName);

        }
        punterDropdown.AddOptions(userList);

    }

    private void LoadOppoData()
    {
        oppoDropdown.ClearOptions();
        oppoList.Clear();
        currentFightNameSelection = fightDropdown.options[fightDropdown.value].text;
        //Debug.Log(currentFightNameSelection);

        var match = fights.FirstOrDefault(f => f.fightDescription == currentFightNameSelection);

        oppoList.Add(match.fighterA);
        oppoList.Add(match.fighterB);


        oppoDropdown.AddOptions(oppoList);
    }

    private void GetUserCredit()
    {
        currentUsernameSelection = punterDropdown.options[punterDropdown.value].text;

        var match = users.FirstOrDefault(u => u.userName == currentUsernameSelection);

        userLabel.text = match.userCredit.ToString();


    }
   
    public void DropdownValueChanged(TMP_Dropdown change)
    {
       
        LoadOppoData();
    }

    public void UserDropdownValueChanged(TMP_Dropdown change)
    {

        GetUserCredit();
    }


}





