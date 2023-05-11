﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxspeed;
    Rigidbody2D Rigid;
    public void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //키 컨트롤로 움직이기
        float h = Input.GetAxisRaw("Horizontal");

        Rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse); //힘을 1초에 50번을 줌

        if (Rigid.velocity.x > maxspeed) //만약 오른쪽 속도가 최고속도를 넘으면 제한해주는 것
            Rigid.velocity = new Vector2(maxspeed, Rigid.velocity.y);
        else if (Rigid.velocity.x < maxspeed*(-1)) //만약 왼쪽 속도가 최고속도를 넘으면 제한해주는 것 (왼쪽은 음수라서 maxspped에  -1을 곱한다)
            Rigid.velocity = new Vector2(maxspeed * (-1), Rigid.velocity.y);
    }
}