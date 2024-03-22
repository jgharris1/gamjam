using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class handler : MonoBehaviour
{
    private double cash = 100000000000000000000.0;
    private double votes = 1.0;
    private double cashBonus = 1.0;
    private double voteBonus = 1.0;
    private double voteRate = 0.0;
    private int genlen = 20;
    private double curGoal;
    private int curRank = 0;
    private string[] symbols = new string[20] {"K", "M", "B", "T", "q", "Q", "s", "S", "O", "N", "D", "UD", "DD", "TD", "qD", "QD", "sD", "SD", "OD", "ND"};
    
    // count, baseRate, Costnext, Bonus
    private double[,] Gens = new double[20, 4]
    {
        {0.0, 1.0,   1.0, 1.0},
        {0.0, 20.0,  24.0, 1.0},
        {0.0, 380.0, 340.0, 1.0},
        {0.0, 5550.0, 5000.0, 1.0},
        {0.0, 120000.0, 100000.0, 1.0},
        {0.0, 2700000.0, 2400000.0, 1.0},
        {0.0, 10000000.0, 60000000.0, 1.0},
        {0.0, 190000000.0, 170000000.0, 1.0},
        {0.0, 50.0, 2000000000000000000000000.0, 1.0},
        {0.0, 50.0, 2000000000000000000000000.0, 1.0},
        {0.0, 50.0, 2000000000000000000000000.0, 1.0},
        {0.0, 50.0, 2000000000000000000000000.0, 1.0},
        {0.0, 50.0, 2000000000000000000000000.0, 1.0},
        {0.0, 50.0, 2000000000000000000000000.0, 1.0},
        {0.0, 50.0, 2000000000000000000000000.0, 1.0},
        {0.0, 50.0, 2000000000000000000000000.0, 1.0},
        {0.0, 50.0, 2000000000000000000000000.0, 1.0},
        {0.0, 50.0, 2000000000000000000000000.0, 1.0},
        {0.0, 50.0, 2000000000000000000000000.0, 1.0},
        {0.0, 50.0, 2000000000000000000000000.0, 1.0}
    };
    private int upgrpntr = 0;
    private double halflife= 0.5;
    private string[,] ranks = new string[20, 17] {{"Ruler of the Universe", "1", "Zaraquon the Infinite", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"Head of the ICUP", "1", "Glaxar Veen", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"Head-Representative of Earth for the\nInterspecies Council of Universal Peace", "1", "Terra NovaPrime", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"President of SuperEarth", "1", "Maximus Orvis", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"Assistant to the Head-Representative of Earth\nfor the Interspecies Council of Universal Peace", "1", "Luna Selene", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"Secretary-General of the United Nations", "1", "Alaric Peacewright", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"President", "1", "Franklin Pierce Adams", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"Vice President", "1", "Isabella Constance", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"Governor", "4", "Archibald Fairview", "Beatrice Windhelm", "Cornelius Sunridge", "Daphne Evergreen", "", "", "", "", "", "", "", "", "", "", ""},{"Senator", "12", "Albert Willowbrook", "Bernadette Rosewater", "Clifford Ironwood", "Diana Clearlake", "Edward Stonebridge", "Felicity Greenvalley", "Geoffrey Brightpark", "Helena Swiftstream", "Isaac Thornfield", "Jessica Meadowfair", "Kevin Highgarden", "Lillian Whitestone", "", "", ""},{"Representative", "15", "Adrian Brookside", "Bella Dawnvale", "Charles Pinehill", "Danielle Rivervale", "Ethan Stormwatch", "Fiona Mapleton", "Gabriel Oakwood", "Harper Fairmont", "Ivy Summershade", "Jake Frostglen", "Kira Bloomdale", "Liam Suncrest", "Mia Starfield", "Noah Moonbrook", "Olivia Lightwood"},{"Congressman", "14", "Daisy Greenfield", "Aaron Brightwood", "Bella Greenleaf", "Charlie Riverstone", "Daphne Wildflower", "Elijah Rainwood", "Freya Sunshadow", "George Leafwind", "Hannah Cloudscape", "Ian Hearthstone", "Jasmine Snowdrift", "Kyle Earthsong", "Leah Springwater", "Mason Wilderfield", ""},{"County Official", "10", "Preston Clearwater", "Quinn Meadowlark", "Rachel Stonemeadow", "Steven Skyharbor", "Tara Everbrook", "Ulysses Cloudhaven", "Vanessa Moonfield", "William Starwood", "Xavier Sunleaf", "Yvette Riverbend", "", "", "", "", ""},{"Mayor", "10", "Ashton Bright", "Bianca Valley", "Carter Shine", "Danielle Peak", "Ethan Grove", "Fiona Waters", "Gavin Forest", "Holly Crest", "Ivan Meadow", "Jenna Bloom", "", "", "", "", ""},{"Sheriff", "8", "Wyatt Lawstar", "Ruby Redlaw", "Mason Ironbadge", "Clara Highboots", "Logan Peacekeeper", "Amelia Nightwatch", "Noah Silverpin", "Lily Goldshield", "", "", "", "", "", "", ""},{"Mrs. Mississippi", "1", "Savannah Belle", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"Prom King", "1", "Tyler Highcharm", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"Student Body President", "4", "Jordan Leadvoice", "Casey Brightspear", "Riley Hopestream", "Alex Visionquest", "", "", "", "", "", "", "", "", "", "", ""},{"Student representative", "5", "Jamie Newleaf", "Kimmy Freshstart", "Leo Youngwish", "Maya Dreamweaver", "Nolan Changeseeker", "", "", "", "", "", "", "", "", "", ""},{"Employee of the Week", "9", "Pat Fryflipper", "Morgan Cashsmile", "Jess Sodashaker", "Elliot Burgertoss", "Sam Saladspinner", "Casey McFlurrymix", "Alex Beanbrewer", "Jordan Pizzaboxer", "Taylor Icecreamswirl", "", "", "", "", "", ""}};
    public Text cash_box;
    public Text vote_box;
    private double[,] upgrades = new double[101, 2] 
    {
        {0, 4.0},
        {1, 15.0},
        {2, 15.0},
        {0, 25.0},
        {3, 95.0},
        {2, 200.0},
        {0, 540.0},
        {1, 1050.0},
        {4, 1950.0},
        {0, 9000.0},
        {5, 20500.0},
        {3, 60750.0},
        {0, 150000.0},
        {1, 320000.0},
        {6, 640000.0},
        {4, 1630000.0},
        {0, 3500000.0},
        {7, 6250000.0},
        {5, 21250000.0},
        {6, 42000000.0},
        {0, 65000000.0},
        {1, 70000000.0},
        {8, 75000000.0},
        {7, 90000000.0},
        {0, 1.0},
        {9, 1.0},
        {8, 1.0},
        {2, 1.0},
        {0, 1.0},
        {1, 1.0},
        {10, 1.0},
        {2, 1.0},
        {3, 1.0},
        {0, 1.0},
        {11, 1.0},
        {4, 1.0},
        {5, 1.0},
        {6, 1.0},
        {0, 1.0},
        {1, 1.0},
        {12, 1.0},
        {7, 1.0},
        {8, 1.0},
        {0, 1.0},
        {13, 1.0},
        {9, 1.0},
        {10, 1.0},
        {11, 1.0},
        {0, 1.0},
        {1, 1.0},
        {14, 1.0},
        {12, 1.0},
        {13, 1.0},
        {2, 1.0},
        {0, 1.0},
        {15, 1.0},
        {3, 1.0},
        {4, 1.0},
        {5, 1.0},
        {6, 1.0},
        {0, 1.0},
        {1, 1.0},
        {16, 1.0},
        {7, 1.0},
        {8, 1.0},
        {9, 1.0},
        {0, 1.0},
        {17, 1.0},
        {10, 1.0},
        {11, 1.0},
        {12, 1.0},
        {13, 1.0},
        {0, 1.0},
        {1, 1.0},
        {18, 1.0},
        {14, 1.0},
        {15, 1.0},
        {16, 1.0},
        {17, 1.0},
        {0, 1.0},
        {19, 1.0},
        {2, 1.0},
        {3, 1.0},
        {4, 1.0},
        {5, 1.0},
        {6, 1.0},
        {0, 1.0},
        {1, 1.0},
        {20, 1.0},
        {7, 1.0},
        {8, 1.0},
        {9, 1.0},
        {10, 1.0},
        {0, 1.0},
        {21, 1.0},
        {11, 1.0},
        {12, 1.0},
        {13, 1.0},
        {14, 1.0},
        {15, 1.0},
        {0, 1.0}
    };
    public GameObject upgrPrefab;
    GameObject upgr;
    private float purcool = 0f;
    private float debugcool = 30f;
    private float autobuycool = 0f;
    private float automouse;
    public double buyuntil;
    // Start is called before the first frame update
    void Start()
    {
        loadcomp(0);
        recalcVoteRate();
    }

    // Update is called once per frame
    void Update()
    {
        purcool += Time.deltaTime;
        debugcool += Time.deltaTime;
        autobuycool += Time.deltaTime;
        transform.GetChild(0).GetChild(3).GetComponent<Text>().text = debugcool.ToString("F2");
        update_vote();
        update_cash();
        update_text();
        if (debugcool < 30)
        {
            automouse += Time.deltaTime;
            if (automouse > .15)
            {
                automouse = 0f;
                manual_vote();
            }
        }
        else if (autobuycool < 10)
        {
            automouse += Time.deltaTime;
            if (automouse > .02)
            {
                automouse = 0f;
                setcash();
            }
        }
        else
        {
            votes = 0;
            voteRate = 0;
        }
    }

    private void update_text()
    {
        vote_box.text = "Votes : " + numConv(Math.Ceiling(votes));
        cash_box.text = "Cash : $" + numConv(cash);
    }

    private void update_cash()
    {
        cash += Math.Ceiling(votes) / 100 * cashBonus * Time.deltaTime;
    }

    private void update_vote()
    {
        votes *= Mathf.Pow((float)halflife, Time.deltaTime);
        votes = Math.Max(votes, voteRate + 1);
    }

    public void manual_vote()
    {
        votes += 1 * voteBonus;
    }

    public double add_gen(int id)
    {
        if (cash >= Math.Round(Gens[id, 2], 2))
        {
            cash -= Math.Round(Gens[id, 2], 2);
            Gens[id, 0] += 1;
            Gens[id, 2] *= 1.1;
        }
        else
        {
            return -1;
        }
        recalcVoteRate();
        return Gens[id, 2];
    }

    private void recalcVoteRate()
    {
        voteRate = 0.0;
        for (int i = 0; i < genlen; i++)
        {
            voteRate += Gens[i, 0] * Gens[i, 1] * Gens[i, 3];
        }
        if (voteRate >= curGoal && curRank != 101)
        {
            loadnextcomp();
        }
        if (voteRate >= 1)
        {
            transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>().text = numConv(voteRate + 1) + " votes";
        }
        else
        {
            transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>().text = numConv(voteRate + 1) + " vote";
        }
        recalcStats();
    }

    private void loadnextcomp()
    {
        if (transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetChild(2).GetComponent<Text>().text != transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetComponent<Text>().text)
        {
            if (upgrpntr != 100)
            {
                stageup();
            }
        }
        transform.GetChild(0).GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetComponent<Text>().text = transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text;
        transform.GetChild(0).GetChild(0).GetChild(2).GetChild(2).GetChild(1).GetComponent<Text>().text = transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(1).GetComponent<Text>().text;
        transform.GetChild(0).GetChild(0).GetChild(2).GetChild(2).GetChild(2).GetComponent<Text>().text = transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetChild(2).GetComponent<Text>().text;
        transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetChild(2).GetComponent<Text>().text = transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetComponent<Text>().text;
        curRank++;
        loadcomp(curRank);
        if(voteRate >= curGoal && curRank != 101)
        {
            loadnextcomp();
        }
        if (curRank == 101)
        {
            Destroy(transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).gameObject);
        }
    }

    private void loadcomp(int id)
    {
        int[] point = new int[2] {19, 0};
        int j = 0; 
        for (int i = 19; i >= 0; i--)
        {
            for (int h = int.Parse(ranks[i, 1]); h > 0 ; h--)
            {
                if (j == id)
                {
                    transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = ranks[i, 1+h];
                    transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(1).GetComponent<Text>().text = numConv(Gens[19-i, 1] *(int.Parse(ranks[i, 1]) - h + 3) + 1) + " votes";
                    transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetComponent<Text>().text = ranks[i, 0];
                    curGoal = (Gens[19-i, 1] *(int.Parse(ranks[i, 1]) - h + 3) + 1);
                }
                j++;
            }
        } 
    }
    public string numConv(double num) 
    { 
        int digcnt = 1;
        string sym = "";
        while (num > 1)
        {
            num /= 10;
            digcnt += 1;
        }
        if (digcnt > 3)
        {
            sym = symbols[(digcnt-1)/3-1];
        }
        num *= Math.Pow(10, (digcnt - 1) % 3);
        sym = num.ToString("F2") + " " + sym;
        return sym;
    }

    public double add_upg(int id)
    {
        if (purcool > .2f)
        {
            if (cash >= upgrades[id,1])
            {
                if ((int)upgrades[id, 0] == 0)
                {
                    voteBonus = voteBonus * 5;
                }
                else if ((int)upgrades[id, 0] == 1)
                {
                    halflife = halflife + .05;
                }
                else
                {
                    Gens[(int)upgrades[id, 0] - 2, 3] = Gens[(int)upgrades[id, 0] - 2, 3] * 2;
                }
                resetup();
                purcool = 0f;
                recalcVoteRate();
                return 0.0;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            return -1;
        }
        
    }

    private void stageup()
    {
        int id = 0;
        string cost = "";
        double preeffect = 0.0;
        double posteffect = 0.0;
        do 
        {
            id = (int)upgrades[upgrpntr, 0];
            cost = "$" + numConv(upgrades[upgrpntr,1]);
            if (id == 0)
            {
                
                preeffect = voteBonus;
                posteffect = voteBonus * 5;
            }
            else if (id == 1)
            {
                preeffect = halflife;
                posteffect = halflife - .05;
            }
            else
            {
                preeffect = Gens[id - 2, 3];
                posteffect = Gens[id - 2, 3] * 2;
            }
            upgrpntr += 1;
            upgr = Instantiate(upgrPrefab, transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetChild(0));
            upgr.GetComponent<boost_loader>().setddata(upgrpntr-1, id, cost, preeffect, posteffect);
        }while((int)upgrades[upgrpntr, 0] != 0);
    }

    private void resetup()
    {
        int id = 0;
        int externid = 0;
        string cost = "";
        double preeffect = 0.0;
        double posteffect = 0.0;
        foreach (GameObject upgr2 in GameObject.FindGameObjectsWithTag("upgrade")) 
        {

            externid = upgr2.GetComponent<boost_loader>().externid;
            id = (int)upgrades[externid, 0];
            cost = "$" + numConv(upgrades[externid, 1]);
            if (id == 0)
            {
                
                preeffect = voteBonus;
                posteffect = voteBonus * 5;
            }
            else if (id == 1)
            {
                preeffect = halflife;
                posteffect = halflife - .05;
            }
            else
            {
                preeffect = Gens[id - 2, 3];
                posteffect = Gens[id - 2, 3] * 2;
            }
            upgr2.GetComponent<boost_loader>().setddata(externid, id, cost, preeffect, posteffect);
        }
        foreach (GameObject upgr2 in GameObject.FindGameObjectsWithTag("generator")) 
        {

            for (int i = 0; i < 20; i++)
            {
                upgr2.GetComponent<upgrade_loader>().data[i, 2] = numConv(Gens[i, 1] * Gens[i, 3]);
                upgr2.GetComponent<upgrade_loader>().load();
            }
        }
    }

    private void recalcStats()
    {
        transform.GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetComponent<Text>().text = "Temporary Vote Degredation : " + ((1 - halflife) * 100).ToString("F0") + "% Per Second";
        transform.GetChild(0).GetChild(1).GetChild(2).GetChild(2).GetComponent<Text>().text = "Permanent Votes : " + numConv(voteRate);
        transform.GetChild(0).GetChild(1).GetChild(2).GetChild(4).GetComponent<Text>().text = "Temporary Votes Per Click : " + numConv(voteBonus);
        transform.GetChild(0).GetChild(1).GetChild(2).GetChild(3).GetComponent<Text>().text = "Distance to next Goal : " + numConv(curGoal - voteRate);
        if (curRank == 101)
        {
            transform.GetChild(0).GetChild(1).GetChild(2).GetChild(3).GetComponent<Text>().text = "Distance to next Goal : " + numConv(0.00);
        }
    }

    public void resetcash()
    {
        debugcool = 0f;
        cash = 0.0;
        recalcVoteRate();
    }

    public void resetsetcash()
    {
        autobuycool = 0f;
    }

    public void setcash()
    {
        cash = 100000000000000000.0;
        int pntr = 0;
        double cheapest = 0;

        cheapest = Gens[0, 2];
        for (int i = 0; i < 20; i++)
        {
            if(Gens[i, 2] < cheapest)
            {
                cheapest = Gens[i, 2];
                pntr = i;
            }
        }
        if (cheapest < buyuntil)
        {
            transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(pntr).GetComponent<upgrade_loader>().TaskOnClick();
        }
    }
}
