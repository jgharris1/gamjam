using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgrade_loader : MonoBehaviour
{
    public Button m_YourFirstButton;
    private int id;
    private handler parentscript;
    public Image img;
    public Text[] texts = new Text[4];
    private double taskret;
    //name                                    amount, production, cost
    public string[,] data = new string[20, 4]
    {
        {"Give Extra\nSauce",                 "0", "1.00 ",   "$1.00"},
        {"Local\nVolunteer\nWork",            "0", "20.00 ",  "$24.00"},
        {"Button\nCampaign",                  "0", "380.00 ", "$340.00"},
        {"Party\nAt My\nHouse",               "0", "5.50 K", "$5.00 K"},
        {"Personal\nTrainer",                 "0", "0.12 M", "$100.00 K"},
        {"Clean\nUp The\nStreets",            "0", "2.70 M", "$2.40 M"},
        {"Parks and\nRecreation\nFunding",    "0", "10.00 M", "$60.00 M"},
        {"Hospital\nExpansion",               "0", "0.19 B", "$0.17 B"},
        {"Billboards",                        "0", "4.78 B", "$4.30 B"},
        {"Primetime\nCommercial",             "0", "0.11 T", "$0.10 T"},
        {"Live\nInterview",                   "0", "2.75 T", "$2.45 T"},
        {"Robotics\nResearch",                "0", "60.00 T", "$54.00 T"},
        {"Party\nConventions",                "0", "1.46 Qua", "1.31 Qua"},
        {"Live\nDebate",                      "0", "47.50 Qua", "$42.40 Qua"},
        {"Governmental\nRestructure",         "0", "0.12 Qui", "$0.11 Qui"},
        {"Interstellar\nGladatorial\nCombat", "0", "3.07 Qui ", "$2.76 Qui"},
        {"Democracy\nProtection\nAgents",     "0", "78.00 Qui", "$68.06 Qui"},
        {"Spice\nTrade\nDeal",                "0", "2.07 Sec", "$1.86 Sec"},
        {"Infestation\nPrevention",           "0", "54.00 Sec", "$47.09 Sec"},
        {"\"Peaceful\"\nInterventions",       "0", "1.33 Sep", "$1.19 Sep"}
    };
    void Start()
    {
        parentscript = GameObject.Find("uihandler").GetComponent<handler>();
        m_YourFirstButton.onClick.AddListener(TaskOnClick);
        id = transform.GetSiblingIndex();
        load();
    }
    
    public void TaskOnClick()
    {
        taskret = parentscript.add_gen(id);
        if (taskret != -1)
        {
            data[id, 1] = (int.Parse(data[id, 1]) + 1).ToString();
            data[id, 3] = parentscript.numConv(taskret);
            load();
        }
    }

    public void load(string newprod = "", string newcost = "")
    {
        img.sprite = Resources.Load<Sprite>("images/camp_" + id.ToString());
        texts[0].text = data[id, 0];
        texts[1].text = "Owned : " + data[id, 1];
        texts[2].text = "Produces : " + data[id, 2];
        texts[3].text = "Cost : " + data[id, 3];
    }
}
