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
        {1.0, 50.0, 200.0, 1.0},
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
