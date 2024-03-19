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
    private string[,] data = new string[20, 4]
    {
        {"Give Extra\nSauce",                 "0", "1",  "$1.00"},
        {"Local\nVolunteer\nWork",            "0", "5",  "$10.00"},
        {"Button\nCampaign",                  "0", "15", "$45.00"},
        {"Party\nAt My\nHouse",               "0", "50", "$150.00"},
        {"Personal\nTrainer",                 "0", "50", "$150.00"},
        {"Clean\nUp The\nStreets",            "0", "50", "$150.00"},
        {"Parks and\nRecreation\nFunding",    "0", "50", "$150.00"},
        {"Hospital\nExpansion",               "0", "50", "$150.00"},
        {"Billboards",                        "0", "50", "$150.00"},
        {"Primetime\nCommercial",             "0", "50", "$150.00"},
        {"Live\nInterview",                   "0", "50", "$150.00"},
        {"Tax Breaks",                        "0", "50", "$150.00"},
        {"Party\nConventions",                "0", "50", "$150.00"},
        {"Live\nDebate",                      "0", "50", "$150.00"},
        {"Governmental\nRestructure",         "0", "50", "$150.00"},
        {"Interstellar\nGladatorial\nCombat", "0", "50", "$150.00"},
        {"Democracy\nProtection\nAgents",     "0", "50", "$150.00"},
        {"Spice\nTrade\nDeal",                "0", "50", "$150.00"},
        {"Infestation\nPrevention",           "0", "50", "$150.00"},
        {"\"Peaceful\"\nInterventions",       "0", "50", "$150.00"}
    };
    void Start()
    {
        parentscript = GameObject.Find("uihandler").GetComponent<handler>();
        m_YourFirstButton.onClick.AddListener(TaskOnClick);
        id = transform.GetSiblingIndex();
        load();
    }
    
    private void TaskOnClick()
    {
        taskret = parentscript.add_gen(id);
        if (taskret != -1)
        {
            data[id, 1] = (int.Parse(data[id, 1]) + 1).ToString();
            data[id, 3] = taskret.ToString("F2");
            load();
        }
    }

    private void load(string newprod = "", string newcost = "")
    {
        img.sprite = Resources.Load<Sprite>("images/camp_" + id.ToString());
        texts[0].text = data[id, 0];
        texts[1].text = "Owned : " + data[id, 1];
        texts[2].text = "Produces : " + data[id, 2];
        texts[3].text = "Cost : " + data[id, 3];
    }
}
