using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect4 : MonoBehaviour
{
    public TypeWriterEffect script1Instance;
    public TypeWriterEffect2 script2Instance;
    public TypeWriterEffect4 script4Instance;
    public TypeWriterEffect5 script5Instance;

    public TMP_Text Txt;
    public TMP_Text timerText;

    string dialogue;

    public string[] Txtdialogue;

    public string[] dialogues;

    public int talkNum4;

    private bool isTyping = false;

    public Button answer1;
    public Button answer2;
    public Button answer3;

    public Button answer4;
    public Button answer5;
    public Button answer6;

    public Button answer7;
    public Button answer8;
    public Button answer9;

    public Button answer10;
    public Button answer11;
    public Button answer12;

    public Button answer13;
    public Button answer14;
    public Button answer15;

    public GameObject Answer;
    public GameObject Answer2;
    public GameObject Answer3;
    public GameObject Answer4;
    public GameObject Answer5;

    public GameObject nextAnswer;

    public GameObject gamescreen;

    public GameObject useranswer;

    public GameObject clear;
    public GameObject fail;

    public GameObject tip;
    public GameObject tip2;

    public GameObject problem;
    public GameObject problem1;
    public GameObject problem2;
    public GameObject problem3;
    public GameObject problem4;

    public AudioSource audioSource;
    public AudioClip failSound;
    public AudioSource audioSource2;
    public AudioClip clearSound;

    private AudioSource previousSound;

    public int canswer;
    public int list;

    public float time;

    public void Start()
    {
        //대사
        //dialogue = "안녕하십니까.";
        //StartCoroutine(Typing(dialogue));
        Answer.SetActive(true);
        answer1.onClick.AddListener(() => CheckAnswer(1));
        answer2.onClick.AddListener(() => CheckAnswer(2));
        answer3.onClick.AddListener(() => CheckAnswer(3));

        answer4.onClick.AddListener(() => CheckAnswer(4));
        answer5.onClick.AddListener(() => CheckAnswer(5));
        answer6.onClick.AddListener(() => CheckAnswer(6));

        answer7.onClick.AddListener(() => CheckAnswer(7));
        answer8.onClick.AddListener(() => CheckAnswer(8));
        answer9.onClick.AddListener(() => CheckAnswer(9));

        answer10.onClick.AddListener(() => CheckAnswer(10));
        answer11.onClick.AddListener(() => CheckAnswer(11));
        answer12.onClick.AddListener(() => CheckAnswer(12));

        answer13.onClick.AddListener(() => CheckAnswer(13));
        answer14.onClick.AddListener(() => CheckAnswer(14));
        answer15.onClick.AddListener(() => CheckAnswer(15));

        tip.SetActive(false);
        tip2.SetActive(true);
        canswer = 2;
        time = 60;
        list = 0;
    }
    public void CheckAnswer(int selectedButton)
    {
        if (selectedButton == canswer)
        {
            if (previousSound != null)
                previousSound.Stop();

            clear.SetActive(true);
            // 성공 효과음 재생
            if (audioSource2 != null && clearSound != null)
            {
                audioSource2.PlayOneShot(clearSound);
                // 이전에 재생한 소리 변수에 현재 소리를 저장
                previousSound = audioSource2;
            }
            switch (list)
            {
                case 0:
                    Invoke("exam2", 1f);
                    break;
                case 1:
                    Invoke("exam3", 1f);
                    break;
                case 2:
                    Invoke("exam4", 1f);
                    break;
                case 3:
                    Invoke("exam5", 1f);
                    break;
                case 4:
                    Invoke("end", 2f);
                    break;
            }
            StartCoroutine(HideClearObject());
            list++;
            time = 60;
            SHOP.currentMoney += 30;
            problemnum.clearproblemnum += 1;
        }
        else
        {
            // 이전에 재생한 소리가 있다면 정지시킴
            if (previousSound != null)
                previousSound.Stop();

            fail.SetActive(true);
            // 실패 효과음 재생
            if (audioSource != null && failSound != null)
            {
                audioSource.PlayOneShot(failSound);
                // 이전에 재생한 소리 변수에 현재 소리를 저장
                previousSound = audioSource;
            }
            switch (list)
            {
                case 0:
                    Invoke("exam2", 1f);
                    break;
                case 1:
                    Invoke("exam3", 1f);
                    break;
                case 2:
                    Invoke("exam4", 1f);
                    break;
                case 3:
                    Invoke("exam5", 1f);
                    break;
                case 4:
                    Invoke("end", 2f);
                    break;
            }
            StartCoroutine(HideFailObject());
            list++;
            time = 60;
            problemnum.failproblemnum += 1;
        }
    }
    public void end()
    {
        Answer.SetActive(false);
        Answer2.SetActive(false);
        Answer3.SetActive(false);
        Answer4.SetActive(false);
        Answer5.SetActive(false);
        problem.SetActive(false);
        problem1.SetActive(false);
        problem2.SetActive(false);
        problem3.SetActive(false);
        problem4.SetActive(false);
        useranswer.SetActive(true);
        tip2.SetActive(false);
        tip.SetActive(true);
        nextAnswer.SetActive(true);
        this.enabled = false;
        script5Instance.enabled = true;
        script5Instance.StartTalk(script5Instance.Txtdialogue);

    }
    public void exam2()
    {
        Answer.SetActive(false);
        Answer2.SetActive(true);
        problem1.SetActive(true);
        canswer = 6;
    }
    public void exam3()
    {
        Answer2.SetActive(false);
        Answer3.SetActive(true);
        problem2.SetActive(true);
        canswer = 9;
    }
    public void exam4()
    {
        Answer3.SetActive(false);
        Answer4.SetActive(true);
        problem3.SetActive(true);
        canswer = 10;
    }
    public void exam5()
    {
        Answer4.SetActive(false);
        Answer5.SetActive(true);
        problem4.SetActive(true);
        canswer = 14;
    }

    private IEnumerator HideFailObject()
    {
        // 1초 대기
        yield return new WaitForSeconds(1f);

        // fail 오브젝트를 비활성화
        fail.SetActive(false);
    }
    private IEnumerator HideClearObject()
    {
        // 1초 대기
        yield return new WaitForSeconds(1f);

        // fail 오브젝트를 비활성화
        clear.SetActive(false);
    }
    private void UpdateTimeUI()
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timerText != null)
        {
            timerText.text = "시간: " + formattedTime;
        }
        if (time < 0)
        {
            CheckAnswer(canswer);
        }
    }

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            UpdateTimeUI();
        }
    }

    IEnumerator Typing(string talk)
    {
        //텍스트를 null값으로 변경
        Txt.text = null;

        //띄어쓰기 두 번이면 줄바꿈으로 바꿔주기
        if (talk.Contains("  ")) talk = talk.Replace(" ", "\n");

        for (int i = 0; i < talk.Length; i++)
        {
            Txt.text += talk[i];
            //속도
            yield return new WaitForSeconds(0.05f);
        }
        //다음 대사 딜레이
        yield return new WaitForSeconds(1.5f);
        isTyping = false;
        NextTalk();
    }
    public void StartTalk(string[] talks)
    {
        if (!isTyping)
        {
            isTyping = true;
            dialogues = talks;
            StartCoroutine(Typing(dialogues[talkNum4]));
        }
    }
    public void NextTalk()
    {
        if (!isTyping)
        {
            isTyping = true;
            Txt.text = null;
            talkNum4++;

            if (talkNum4 == dialogues.Length)
            {
                EndTalk();
                return;
            }

            StartCoroutine(Typing(dialogues[talkNum4]));
        }
    }
    public void EndTalk()
    {
        //talkNum 초기화
        talkNum4 = 0;
        Debug.Log("대사 끝");
        this.enabled = false;
    }
}
