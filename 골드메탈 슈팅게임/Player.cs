using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public float speed; //속도
    public float power;
    public float maxShotDelay; //발사 딜레이 변수 최대
    public float curShotDelay; //현재


    public bool isTouchTop; //위에 닿았는가?
    public bool isTouchBottom; //아래에 닿았는가?
    public bool isTouchLeft; //왼쪽에 닿았는가?
    public bool isTouchRight; //오른쪽에 닿았는가?

    public GameObject bulletobjA; //총알A 오브젝트
    public GameObject bulletobjB; //총알B 오브젝트
    public GameObject soulHeart1; //소울하트1
    public GameObject soulHeart2; //소울하트2
    public GameObject soulHeart3; //소울하트3
    public GameObject soulHeart4; //소울하트4
    public GameObject soulHeart5; //소울하트5
    public GameObject heart1; //체력
    public GameObject heart2; //체력
    public GameObject heart3; //체력
    public GameObject heart4; //체력
    public GameObject heart5; //체력

    public GameObject item1; //아이템1(점심밥)
    public GameObject item2; //아이템2(악마템1)
    public GameObject item3; //아이템3(과일케이크)
    public GameObject item4; //아이템4(악마템2)
    public GameObject soulheartitem; //소울하트 아이템

    public int soulheart; //보호막 같은 거
    public int minussoulheart;
    public int maxsoulheart; //최대 소울하트 수
    public static int plusdmg; //추가 되는 데미지

    public GameManager manager; //매니저

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        soulheart = 0; 
        maxsoulheart = 6;
        plusdmg = 0;
        minussoulheart = 0;
    }
    void Update()
    {
        Move();
        Fire();
        Reload();
        if (Input.GetKeyDown(KeyCode.Keypad1)) //콘솔 키패드1을 누르면 레벨1로 변경
        {
            Debug.Log("레벨1");
            power = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2)) //콘솔 키패드2을 누르면 레벨2로 변경
        {
            Debug.Log("레벨2");
            power = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3)) //콘솔 키패드3을 누르면 레벨3으로 변경
        {
            Debug.Log("레벨3");
            power = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4)) //콘솔 키패드4을 누르면 소울하트 하나 추가
        {
            Debug.Log("소울하트 추가");
            soulheart = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5)) //콘솔 키패드5을 누르면 소울하트 하나 더 추가
        {
            Debug.Log("소울하트 추가");
            soulheart = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6)) //콘솔 키패드6을 누르면 소울하트 하나 더 추가
        {
            Debug.Log("소울하트 추가");
            soulheart = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7)) //콘솔 키패드7을 누르면 소울하트 하나 더 추가
        {
            Debug.Log("소울하트 추가");
            soulheart = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8)) //콘솔 키패드8을 누르면 소울하트 하나 더 추가
        {
            Debug.Log("소울하트 추가");
            soulheart = 5;
        }
        if (soulheart == 1) //소울하트가 1이라면 소울하트 오브젝트 활성화하기
        {
            soulHeart1.SetActive(true);
        }
        else if (soulheart == 2) //소울하트가 2이라면 소울하트 오브젝트 하나 더 활성화하기
        {
            soulHeart2.SetActive(true); //소울하트가 3이라면 소울하트 오브젝트 하나 더 활성화하기
        }
        else if (soulheart == 3)
        {
            soulHeart3.SetActive(true); //소울하트가 4이라면 소울하트 오브젝트 하나 더 활성화하기
        }
        else if (soulheart == 4)
        {
            soulHeart4.SetActive(true); //소울하트가 5이라면 소울하트 오브젝트 하나 더 활성화하기 
        }
        else if (soulheart == 5)
        {
            soulHeart5.SetActive(true); //소울하트가 6이라면 소울하트 오브젝트 하나 더 활성화하기
        }
        if (minussoulheart == 1) //만약 마이너스 하트가 1일때
        {
            soulHeart5.SetActive(false); //소울하트5를 비활성화
            minussoulheart = 0; //마이너스 하트를 초기화
        }
        else if (minussoulheart == 2)//만약 마이너스 하트가 2일때
        {
            soulHeart4.SetActive(false); //소울하트4를 비활성화
            minussoulheart = 0; //마이너스 하트를 초기화
        }
        else if (minussoulheart == 3) //만약 마이너스 하트가 3일때
        {
            soulHeart3.SetActive(false); //소울하트3를 비활성화
            minussoulheart = 0; //마이너스 하트를 초기화
        }
        else if (minussoulheart == 4) //만약 마이너스 하트가 4일때
        {
            soulHeart2.SetActive(false); //소울하트2를 비활성화
            minussoulheart = 0; //마이너스 하트를 초기화
        }
        else if (minussoulheart == 5) //만약 마이너스 하트가 5일때
        {
            soulHeart1.SetActive(false); //소울하트1를 비활성화
            minussoulheart = 0; //마이너스 하트를 초기화
        }
    }
    void Move()
    {
        //플레이어 움직이기
        float h = Input.GetAxisRaw("Horizontal"); //수평
        if ((isTouchRight == true && h == -1) || (isTouchLeft == true && h == 1)) //만약 h가 1이거나 Right가 true라면 or 만약 h가 -1이거나 Left가 true라면
            h = 0;
        float v = Input.GetAxisRaw("Vertical"); //수직
        if ((isTouchTop == true && v == 1) || (isTouchBottom == true && v == -1)) //만약 v가 1이거나 Top가 true라면 or 만약 v가 -1이거나 Bottom가 true라면
            v = 0;
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime; //!!transform은 반드시 Time.deltaTime을 곱해줄 것!(물리적 이동은 상관 x)
        transform.position = curPos + nextPos;


        if (Input.GetButtonDown("Horizontal") ||
            Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
    }
    void Fire()
    {
        //총알 쏘기
        if (!Input.GetButton("Fire1")) //fire1버튼을 눌렀다면?
        {
            return;
        }
        if(curShotDelay < maxShotDelay) //현재 장전수가 최대보다 넘지 않았다면?
        {
            return;
        }
        switch (power){
            case 1: //레벨1
                //파워가 하나다
                GameObject bullet = Instantiate(bulletobjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>(); //힘주기
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//위 방향으로 속도*10만큼 나아가게
                break;
            case 2: //레벨2
                GameObject bulletR = Instantiate(bulletobjA, transform.position+Vector3.right*0.1f, transform.rotation);
                GameObject bulletL = Instantiate(bulletobjA, transform.position+Vector3.left*0.1f, transform.rotation);
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>(); //오른쪽 힘주기
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>(); //왼쪽 힘주기
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//위 방향으로 속도*10만큼 나아가게
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//위 방향으로 속도*10만큼 나아가게
                break;
            case 3: //레벨3
                GameObject bulletRR = Instantiate(bulletobjA, transform.position + Vector3.right * 0.25f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletobjB, transform.position, transform.rotation); //혼자 다르게 큰 총알 쏘기
                GameObject bulletLL = Instantiate(bulletobjA, transform.position + Vector3.left * 0.25f, transform.rotation);
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>(); //오른쪽 힘주기
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>(); //가운데 힘주기
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>(); //왼쪽 힘주기
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//위 방향으로 속도*10만큼 나아가게
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//위 방향으로 속도*10만큼 나아가게
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);//위 방향으로 속도*10만큼 나아가게
                break;
        }

        curShotDelay = 0; //총알을 쏘고 0으로 초기화 해주기
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;

    }

    void OnTriggerEnter2D(Collider2D collision) //경계선에 들어왔다면
    {
        if(collision.gameObject.tag == "Border") //만약 Border라는 태그를 가진 게임 오브젝트에 닿았으면
        {
            //게임 오브젝트 top bottom left right에 닿았다면 isTouch들을 true로 바꿔준다
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
        else if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet") //만약 적에게 닿았거나 적 탄에 닿았다면
        {
            manager.RespawnPlayer(); //리스폰 대기시간
            gameObject.SetActive(false); //플레이어를 비활성화
            if(soulheart == 5)  //만약 소울하트가 5라면
            {
                minussoulheart += 1; //-소울하트 하나 추가
                soulheart -= 1; //소울하트 하나 깎음
            }
            else if (soulheart == 4)  //만약 소울하트가 4라면
            {
                for (int i = 1; i <= 2; i++) //반복문 2번
                {
                    minussoulheart += 1; //-소울하트 하나 추가
                }
                soulheart -= 1; //소울하트 하나 깎음
            }
            else if (soulheart == 3)  //만약 소울하트가 3이라면
            {
                for (int i = 1; i <= 3; i++) //반복문 3번
                {
                    minussoulheart += 1; //-소울하트 하나 추가
                }
                soulheart -= 1; //소울하트 하나 깎음
            }
            else if (soulheart == 2)  //만약 소울하트가 2라면
            {
                for (int i = 1; i <= 4; i++) //반복문 4번
                {
                    minussoulheart += 1; //-소울하트 하나 추가
                }
                soulheart -= 1; //소울하트 하나 깎음
            }
            else if (soulheart == 1)  //만약 소울하트가 1이라면
            {
                for (int i = 1; i <= 5; i++) //반복문 5번
                {
                    minussoulheart += 1; //-소울하트 하나 추가
                }
                soulheart -= 1; //소울하트 하나 깎음
            }
            else if (soulheart != 5 && soulheart != 4 && soulheart != 3 && soulheart != 2 && soulheart != 1 && Heart.heart == 5)
            { //만약 소울하트가 전부 없지만 체력이 5칸이라면
                Heart.minusheart += 1; //추가 체력을 깎음
            }
            else if (soulheart != 5 && soulheart != 4 && soulheart != 3 && soulheart != 2 && soulheart != 1 && Heart.heart == 4)
            { //만약 소울하트가 전부 없다면 체력이 4칸이라면
                Heart.minusheart += 1; //추가 체력을 깎음
            }
            else //전부다 아니라면
            {
                Heart.heart -= 1; //그냥 체력을 깎음
            }
        }
        else if (collision.gameObject.tag == "Item")
        {
            item Item = collision.gameObject.GetComponent<item>();
            switch (Item.type)
            {
                case "SoulHeart": //소울하트 아이템을 먹었다면
                    if(soulheart < maxsoulheart)
                    {
                        soulheart += 1; //소울하트 하나 추가
                    }
                    break;
                case "1": //아이템1을 먹었다면
                    Heart.heart += 1; //체력 추가
                    speed -= 0.3f; //속도 0.3만큼 줄어듬
                    plusdmg += 1; //데미지 1 증가
                    Enemy.deathcnt = 0; //데드 카운트 초기화
                    GameManager.stage += 10; //다음 스테이지 제한 +10
                    item2.SetActive(false);
                    break;
                case "2":
                    speed -= 2; //속도 -2만큼 줄어듬
                    plusdmg += 15; //공격력 15만큼 증가
                    Enemy.deathcnt = 0; //데드 카운트 초기화
                    GameManager.stage += 10; //다음 스테이지 제한 +10
                    item1.SetActive(false);
                    break;
                case "3":
                    maxShotDelay -= 0.1f; //총쏠때 딜레이가 0.1만큼 줄어듬
                    Heart.heart += 1; //최대체력 증가
                    Enemy.deathcnt = 0; //데드 카운트 초기화
                    GameManager.stage += 10; //다음 스테이지 제한 +10
                    item4.SetActive(false);
                    break;
                case "4":
                    maxShotDelay -= 0.17f; //총쏠때 딜레이가 0.17만큼 줄어듬
                    plusdmg -= 1; //데미지 1만큼  감소
                    if(Bullet.attack >= 15) //만약 데미지가 15랑 같거나 넘는다면
                    {
                        plusdmg -= 10; //데미지를 10깎음
                    }
                    if(power == 3) //만약 레벨이 3아리면
                    {
                        plusdmg -= 3; //데미지를 3 줄임
                    }
                    if(Heart.heart == 5) //만약 체력이 5 칸이라면 
                    {
                        Heart.minusheart += 2; //체력을 두칸 깎는다
                        heart5.SetActive(false);
                        heart4.SetActive(false);

                    }
                    else if (Heart.heart == 4)
                    { //만약 체력이 4 칸이라면 
                        Heart.minusheart += 2; //체력을 두칸 깎는다
                        Heart.heart -= 1;
                        heart4.SetActive(false);
                        heart3.SetActive(false);

                    }
                    else if (Heart.heart == 3) //만약 체력이 3 칸이라면 
                    {
                        Heart.heart -= 2; //체력을 두칸 깎는다
                        heart3.SetActive(false);
                        heart2.SetActive(false);
                    }
                    else if (Heart.heart == 2) //만약 체력이 2 칸이라면 
                    {
                        Heart.heart -= 2; //체력을 두칸 깎는다
                        heart2.SetActive(false);
                        heart1.SetActive(false);
                    }
                    else
                    {
                        Heart.heart -= 1; //체력을 깎는다
                        heart1.SetActive(false);
                    }
                    Enemy.deathcnt = 0; //데드 카운트 초기화
                    GameManager.stage += 10; //다음 스테이지 제한 +10
                    item3.SetActive(false);
                    break;

            }
            Destroy(collision.gameObject);
            
        }

    }

    void OnTriggerExit2D(Collider2D collision) //경계선에서 나왔다면
    {
        if (collision.gameObject.tag == "Border") //만약 Border라는 태그를 가진 게임 오브젝트에 닿았으면
        {
            //게임 오브젝트 top bottom left right에 닿았다면 isTouch들을 false로 바꿔준다
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
}
