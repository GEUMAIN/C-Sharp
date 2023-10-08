using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class problemnum : MonoBehaviour
{
    public static int clearproblemnum = 0;
    public static int failproblemnum = 0;

    public GameObject ending1;
    public GameObject ending2;
    public GameObject ending3;

    public TypeWriterEffect11 script11Instance;
    public TypeWriterEffect12 script12Instance;
    public TypeWriterEffect13 script13Instance;

    public void Start()
    {
        if (failproblemnum >= 26)
        {
            allfailend();
            ending1.SetActive(false);
            ending3.SetActive(false);
        }
        else
        {
            normalend();
        }
        if (clearproblemnum >= 26)
        {
            allclearend();
            ending1.SetActive(false);
            ending2.SetActive(false);
        }
        else
        {
            normalend();
        }
    }
    public void normalend()
    {
        if (clearproblemnum <= 25 && failproblemnum > 0)
        {
            script11Instance.enabled = true;
            script12Instance.enabled = false;
            script13Instance.enabled = false;
            script11Instance.StartTalk(script11Instance.Txtdialogue);
        }
    }
    public void allfailend()
    {
        if (failproblemnum >= 26)
        {
            script11Instance.enabled = false;
            script12Instance.enabled = true;
            script13Instance.enabled = false;
            script12Instance.StartTalk(script12Instance.Txtdialogue);
        }
    }
    public void allclearend()
    {
        if (clearproblemnum >= 26)
        {
            script11Instance.enabled = false;
            script12Instance.enabled = false;
            script13Instance.enabled = true;
            script13Instance.StartTalk(script13Instance.Txtdialogue);
        }
    }
}
