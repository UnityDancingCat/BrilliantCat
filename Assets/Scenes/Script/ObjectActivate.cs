using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Trigger;
using static TutorialText;

public class ObjectActivate : MonoBehaviour
{
    public GameObject StimBox1;
    public GameObject StimBox2;
    public GameObject StimBox3;
    public GameObject StimBox4;
    public GameObject StimBoxOutline;
    public GameObject StimBoxOutlineBlue;
    public GameObject DialBox;
    public GameObject DialCat;
    public bool end;
    public bool stimEnd;
    public bool stimBox;
    public bool allDialEndTrig;
    public static bool dialBox;

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(3f);
    }

    void BlockTutorialStim(bool end)
    {
        // UnityEngine.Debug.Log(stimBox);

        if ((end == false) || (stimEnd == true))
        {
            // UnityEngine.Debug.Log("end: " + end + ", StimEnd: " + stimEnd); //두 번째 dial 시작할 때 end = true, sitmEnd = true -> 버튼 누르고 나면 end = false, stimend = true
            StartCoroutine(WaitForIt());

            DialBox.SetActive(true);
            DialCat.SetActive(true);

            dialBox = true;

            if (stimEnd == true)
            {
                StimBox1.SetActive(false);
                StimBox2.SetActive(false);
                StimBox3.SetActive(false);
                StimBox4.SetActive(false);
                // StimBoxOutline.SetActive(false);
            }
        }

        else
        {
            DialBox.SetActive(false);
            DialCat.SetActive(false);

            dialBox = false;
        }
    }

    void BlockTutorialBox(bool stimBox)
    {
        if (allDialEndTrig != true)
        {
            if ((stimBox == true) || (end == true))
            {
                //UnityEngine.Debug.Log("startStim: " + startStim);
                StimBox1.SetActive(true);
                StimBox2.SetActive(true);
                StimBox3.SetActive(true);
                StimBox4.SetActive(true);
                StimBoxOutline.SetActive(true);
                StimBoxOutlineBlue.SetActive(true);
                // UnityEngine.Debug.Log("stimBox active");
            }

            else if ((stimBox == false) || (end == false))
            {
                StartCoroutine(WaitForIt());

                StimBox1.SetActive(false);
                StimBox2.SetActive(false);
                StimBox3.SetActive(false);
                StimBox4.SetActive(false);
                StimBoxOutline.SetActive(false);
                StimBoxOutlineBlue.SetActive(false);
                // UnityEngine.Debug.Log("stimBox inactive");
            }
        }

        else {UnityEngine.Debug.Log("tutorial end");}        
    }

    void Start()
    {
        StimBox1.SetActive(false);
        StimBox2.SetActive(false);
        StimBox3.SetActive(false);
        StimBox4.SetActive(false);
        StimBoxOutline.SetActive(false);
        StimBoxOutlineBlue.SetActive(false);
    }

    void Update()
    {
        end = TutorialText.dialEnd;
        stimEnd = Trigger.stimEndTrig;
        stimBox = Trigger.stimBoxTrig;
        allDialEndTrig = TutorialText.allDialEnd;

        BlockTutorialStim(end);
        BlockTutorialBox(stimBox);
    }
}
