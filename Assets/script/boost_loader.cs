using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boost_loader : MonoBehaviour
{
    public Button m_YourFirstButton;
    private int id;
    public int externid;
    private handler parentscript;
    private Text[] texts = new Text[2];
    private Image img;
    private double taskret;
    private bool avail = true;
    
    public void setddata(int idnum, int idnum2, string cost, double effectpre, double effectpost)
    {
        texts[0] = transform.GetChild(0).GetComponent<Text>();
        texts[1] = transform.GetChild(2).GetComponent<Text>();
        img = transform.GetChild(1).GetComponent<Image>();
        parentscript = GameObject.Find("uihandler").GetComponent<handler>();
        m_YourFirstButton.onClick.AddListener(TaskOnClick);
        externid = idnum;
        id = idnum2;
        img.sprite = Resources.Load<Sprite>("images/camp_" + (id-2).ToString());
        texts[0].text = parentscript.numConv(effectpre) + " > " + parentscript.numConv(effectpost);
        texts[1].text = cost;
    }
    
    private void TaskOnClick()
    {
        if (avail)
        {
            avail = false;
            taskret = parentscript.add_upg(externid);
            if (taskret != -1)
            {
                Destroy(gameObject);
            }
            else
            {
                avail = true;
            }
        }
    } 
}
