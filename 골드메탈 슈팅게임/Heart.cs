using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public static int heart; //체력

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;

    public static int minusheart; //체력깎기

    private void Awake()
    {
        heart = 3;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("체력 깎는중");
            heart -= 1;
        
        }
        if (heart == 2)
        {
            heart3.SetActive(false);
        }
        if (heart == 1)
        {
            heart2.SetActive(false);
        }
        if (heart == 0)
        {
            heart1.SetActive(false);
        }
        if (heart == 4)
        {
            heart4.SetActive(true);
        }
        if (heart == 5)
        {
            heart5.SetActive(true);
        }
        if (minusheart == 1) //만약 마이너스 하트가 1이라면
        {
            if (heart == 5) //만약 하트가 5라면
            {
                heart5.SetActive(false); //하트 5를 비활성화 하고
            }
            if (heart == 4) //만약 하트가 4라면
            {
                heart4.SetActive(false); //하트 4를 비활성화 한다
            }
        }
        if (minusheart == 2) //만약 마이너스 하트가 2라면
        {
            heart4.SetActive(false); //하트 4를 비활성화 한다
            minusheart = 0;
        }
    }
}
