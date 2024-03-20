using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class handler : MonoBehaviour
{
    private double cash = 0.0;
    private double votes = 1.0;
    private double cashBonus = 1.0;
    private double voteBonus = 1.0;
    private double voteRate = 0.0;
    // count, baseRate, Costnext, Bonus
    private int genlen = 20;
    private double[,] Gens = new double[20, 4]
    {
        {0.0, 1.0, 1.0, 1.0},
        {0.0, 5.0, 10.0, 1.0},
        {0.0, 15.0, 45.0, 1.0},
        {0.0, 50.0, 200.0, 1.0},
        {0.0, 50.0, 200.0, 1.0},
        {0.0, 50.0, 200.0, 1.0},
        {0.0, 50.0, 200.0, 1.0},
        {0.0, 50.0, 200.0, 1.0},
        {0.0, 50.0, 200.0, 1.0},
        {0.0, 50.0, 200.0, 1.0},
        {0.0, 50.0, 200.0, 1.0},
        {0.0, 50.0, 200.0, 1.0},
        {0.0, 50.0, 200.0, 1.0},
        {0.0, 50.0, 200.0, 1.0},
        {0.0, 50.0, 200.0, 1.0},
        {0.0, 50.0, 200.0, 1.0},
        {0.0, 50.0, 200.0, 1.0},
        {0.0, 50.0, 200.0, 1.0},
        {0.0, 50.0, 200.0, 1.0},
        {0.0, 50000000.0, 200.0, 1.0}
    };

    private string[,] ranks = new string[20, 17] {{"Ruler of the Universe", "1", "Zaraquon the Infinite", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"Head of the ICUP", "1", "Glaxar Veen", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"Head-Representative of Earth for the Interspecies Council of Universal Peace", "1", "Terra NovaPrime", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"President of SuperEarth", "1", "Maximus Orvis", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"Assistant to the Head-Representative of Earth for the Interspecies Council of Universal Peace", "1", "Luna Selene", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"Secretary-General of the United Nations", "1", "Alaric Peacewright", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"President", "1", "Franklin Pierce Adams", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"Vice President", "1", "Isabella Constance", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"Governor", "4", "Archibald Fairview", "Beatrice Windhelm", "Cornelius Sunridge", "Daphne Evergreen", "", "", "", "", "", "", "", "", "", "", ""},{"Senator", "12", "Albert Willowbrook", "Bernadette Rosewater", "Clifford Ironwood", "Diana Clearlake", "Edward Stonebridge", "Felicity Greenvalley", "Geoffrey Brightpark", "Helena Swiftstream", "Isaac Thornfield", "Jessica Meadowfair", "Kevin Highgarden", "Lillian Whitestone", "", "", ""},{"Representative", "15", "Adrian Brookside", "Bella Dawnvale", "Charles Pinehill", "Danielle Rivervale", "Ethan Stormwatch", "Fiona Mapleton", "Gabriel Oakwood", "Harper Fairmont", "Ivy Summershade", "Jake Frostglen", "Kira Bloomdale", "Liam Suncrest", "Mia Starfield", "Noah Moonbrook", "Olivia Lightwood"},{"Congressman", "14", "Daisy Greenfield", "Aaron Brightwood", "Bella Greenleaf", "Charlie Riverstone", "Daphne Wildflower", "Elijah Rainwood", "Freya Sunshadow", "George Leafwind", "Hannah Cloudscape", "Ian Hearthstone", "Jasmine Snowdrift", "Kyle Earthsong", "Leah Springwater", "Mason Wilderfield", ""},{"County Official", "10", "Preston Clearwater", "Quinn Meadowlark", "Rachel Stonemeadow", "Steven Skyharbor", "Tara Everbrook", "Ulysses Cloudhaven", "Vanessa Moonfield", "William Starwood", "Xavier Sunleaf", "Yvette Riverbend", "", "", "", "", ""},{"Mayor", "10", "Ashton Bright", "Bianca Valley", "Carter Shine", "Danielle Peak", "Ethan Grove", "Fiona Waters", "Gavin Forest", "Holly Crest", "Ivan Meadow", "Jenna Bloom", "", "", "", "", ""},{"Sheriff", "8", "Wyatt Lawstar", "Ruby Redlaw", "Mason Ironbadge", "Clara Highboots", "Logan Peacekeeper", "Amelia Nightwatch", "Noah Silverpin", "Lily Goldshield", "", "", "", "", "", "", ""},{"Mrs. Mississippi", "1", "Savannah Belle", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"Prom King", "1", "Tyler Highcharm", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},{"Student Body President", "4", "Jordan Leadvoice", "Casey Brightspear", "Riley Hopestream", "Alex Visionquest", "", "", "", "", "", "", "", "", "", "", ""},{"Student representative", "5", "Jamie Newleaf", "Kimmy Freshstart", "Leo Youngwish", "Maya Dreamweaver", "Nolan Changeseeker", "", "", "", "", "", "", "", "", "", ""},{"Employee of the Week", "9", "Pat Fryflipper", "Morgan Cashsmile", "Jess Sodashaker", "Elliot Burgertoss", "Sam Saladspinner", "Casey McFlurrymix", "Alex Beanbrewer", "Jordan Pizzaboxer", "Taylor Icecreamswirl", "", "", "", "", "", ""}};

    public Text cash_box;
    public Text vote_box;
    // Start is called before the first frame update
    void Start()
    {
        recalcVoteRate();
    }

    // Update is called once per frame
    void Update()
    {
        update_vote();
        update_cash();
        update_text();
    }

    private void update_text()
    {
        vote_box.text = "Votes : " + Math.Ceiling(votes).ToString("F0");
        cash_box.text = "Cash : $" + cash.ToString("F2");
    }

    private void update_cash()
    {
        cash += Math.Ceiling(votes) / 100 * cashBonus * Time.deltaTime;
    }

    private void update_vote()
    {
        votes *= Mathf.Pow(.50f, Time.deltaTime);
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
    }


}
