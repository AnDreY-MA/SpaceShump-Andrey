using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy
{
    //Тракетория движения вычисляется путем линейной интерполяции кривой Безье по более чем двум точка
    [Header("Set in Inspector: Enemy3")]
    public float lifeTime = 5;

    [Header("Set Dynamically: Enemy3")]
    public Vector3[] points;
    public float birthTime;

    void Start()
    {
        points = new Vector3[3];

        points[0] = pos;

        //Установить xMin xMax так же, как это делает Main.SpawnEnemy()
        float xMin = -bndCheck.camWidth + bndCheck.radius;
        float xMax = bndCheck.camWidth - bndCheck.radius;

        Vector3 v;

        //Случайно выбрать среднию точку ниже нижней границы экрана
        v = Vector3.zero;
        v.y = pos.y;
        v.x = Random.Range(xMin, xMax);
        points[2] = v;

        birthTime = Time.time;
    }

    public override void Move()
    {
        //Кривые Безье вычисляются на основе значения u между 0 b 1
        float u = (Time.time - birthTime - birthTime) / lifeTime;

        if(u > 1)
        {
            Destroy(this.gameObject);
            return;


        }

        //Интерполировать кривую Безье по трем точкам
        Vector3 p01, p12;
        u = u - 0.2f * Mathf.Sin(u * Mathf.PI * 2);
        p01 = (1 - u) * points[0] + u * points[1];
        p12 = (1 - u) * points[1] + u * points[2];
        pos = (1 - u) * p01 + u * p12;
    }
}