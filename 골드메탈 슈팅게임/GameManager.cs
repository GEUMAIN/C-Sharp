using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public GameObject player;

    public GameObject item4display1;
    public GameObject item4display2;
    public GameObject item4display3;
    public GameObject item4display4;
    public GameObject item4display5;
    public GameObject item4display6;
    public GameObject item3display1;
    public GameObject item3display2;
    public GameObject item2display1;
    public GameObject item2display2;
    public GameObject item1display1;
    public GameObject item1display2;
    public GameObject item1display3;

    public Transform[] spawnPoint;

    public float maxSpawnDelay;//스폰 최고 딜레이
    public float curSpawnDelay;
    public float min;
    public float max;


    public bool spawn;
    public bool stage1;
    public bool stage2;
    public bool stage3;



    public int itemrandom;

    public static int stage;

    private void Awake()
    {
        spawn = true;
        stage = 9;
        stage1 = false;
        stage2 = false;
        stage3 = false;
        min = 0.5f;
        max = 5f;
    }

    void FixedUpdate()
    {
        if (spawn)
        {
            spawnEnemy();
        }
        stopSpawn();
    }
    void spawnEnemy() {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(min, max);
            curSpawnDelay = 0;
        }
    }
    void stopSpawn()
    {
        if (Enemy.deathcnt > stage) //만약 데드 카운트가 9보다 크다면
        {
            spawn = false; //스폰을 멈추고
            stage1 = true; //스테이지1을 활성화 한다
            if (stage1 == true) //만약 스테이지1이 활성화 된다면
            {
                if (Enemy.deathcnt == 10 || Enemy.deathcnt == 11 || Enemy.deathcnt == 12 || Enemy.deathcnt == 13)
                {
                    item1.SetActive(true); //아이템1 활성화
                    item2.SetActive(true); //아이템2 활성화
                    max = 2.5f;

                    item2display1.SetActive(true);
                    item2display2.SetActive(true);
                    item1display1.SetActive(true);
                    item1display2.SetActive(true);
                    item1display3.SetActive(true);


                }
            }

            if (Enemy.deathcnt > stage) //데드 카운트가 19 보다 크다면
            {
                spawn = false;
                stage2 = true; //스테이지 2를 활성화
                if (stage2 == true) //만약 스테이지2가 활성화 된다면
                {
                    if(Enemy.deathcnt == 20 || Enemy.deathcnt == 21 || Enemy.deathcnt == 22 || Enemy.deathcnt == 23)
                    {
                        item3.SetActive(true); //아이템3 활성화
                        item4.SetActive(true); //아이템4 활성화
                        max = 2f;
                        item4display1.SetActive(true);
                        item4display2.SetActive(true);
                        item4display3.SetActive(true);
                        item4display4.SetActive(true);
                        item4display5.SetActive(true);
                        item4display6.SetActive(true);
                        item3display1.SetActive(true);
                        item3display2.SetActive(true);
                    }
                }
            }
        }
    
        else if (Enemy.deathcnt == 0) //만약 데드카운트가 0이라면
        {
            maxSpawnDelay = Random.Range(min, max); //초기화
            spawn = true; //적 스폰 다시 시작
            item4display1.SetActive(false);
            item4display2.SetActive(false);
            item4display3.SetActive(false);
            item4display4.SetActive(false);
            item4display5.SetActive(false);
            item4display6.SetActive(false);
            item3display1.SetActive(false);
            item3display2.SetActive(false);
            item2display1.SetActive(false);
            item2display2.SetActive(false);
            item1display1.SetActive(false);
            item1display2.SetActive(false);
            item1display3.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            item Item = collision.gameObject.GetComponent<item>();
            switch (Item.type)
            {
                case "1":
                    Enemy.deathcnt = 0;
                    break;
            }

        }
        if (collision.gameObject.tag == "Item")
        {
            item Item = collision.gameObject.GetComponent<item>();
            switch (Item.type)
            {
                case "2":
                    Enemy.deathcnt = 0;
                    break;
            }
        }
        if (collision.gameObject.tag == "Item")
        {
            item Item = collision.gameObject.GetComponent<item>();
            switch (Item.type)
            {
                case "3":
                    Enemy.deathcnt = 0;
                    break;
            }
        }
        if (collision.gameObject.tag == "Item")
        {
            item Item = collision.gameObject.GetComponent<item>();
            switch (Item.type)
            {
                case "4":
                    Enemy.deathcnt = 0;
                    break;
            }
        }
    }
    
    void SpawnEnemy()
    {
        int ranEnemey = Random.Range(0, 3); //소환될 적
        int ranPoint = Random.Range(0, 9); //소환될 위치

        GameObject enemy =Instantiate(enemyObjs[ranEnemey],
            spawnPoint[ranPoint].position,
            spawnPoint[ranPoint].rotation);
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;

        if (ranPoint == 5 || ranPoint == 6) // 오른쪽에서 오는 적 속도 방향
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if (ranPoint == 7 || ranPoint == 8) //왼쪽에서 오는 적 속도 방향
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else //중앙에서 오는 적  속도 방향
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }
    }
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe",2f); //2초 쿨타임
        player.transform.position = Vector3.down * -4f;
        player.SetActive(true);

    }
    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 4f;
        player.SetActive(true);
    }

}
