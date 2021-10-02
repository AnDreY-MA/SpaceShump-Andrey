using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    [Header("Set in Inspector: Enemy2")]
    //Определяют, насколько ярко будет выражен синусоидальный характер движения
    public float sinEccentricity = 0.6f;
    public float lifeTime = 10;

    [Header("Set Dynamically: Enemy2")]
    public Vector3 p0;
    public Vector3 p1;
    public float birthTime;

    void Start()
    {
        //Выбрать случайную точку на левой границе экрана
        p0 = Vector3.zero;
        p0.x = -bndCheck.camWidth - bndCheck.radius;
        p0.y = Random.Range(-bndCheck.camHeight, bndCheck.camHeight);

        //Выбрать случайную точку на правой границе экране
        p1 = Vector3.zero;
        p1.x = bndCheck.camWidth + bndCheck.radius;
        p1.y = Random.Range(-bndCheck.camHeight, bndCheck.camHeight);

        //Случайно поменять начальную и конечную точку местами
        if (Random.value > 0.5f)
        {
            //Изменение знака .х каждой точки переносит ее на другой край экрана
            p0.x *= -1;
            p1.x *= -1;
        }

        //Записать в bitrhTime текущее время 
        birthTime = Time.time;
    }

    public override void Move()
    {
        float u = (Time.time - birthTime) / lifeTime;

        //Если u>1 значит, корабль существует дольше, чем lifeTime
        if (u > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        //Скоректировать u добавлением значения кривой, изменяяющейся по синусоиде
        u = u + sinEccentricity * (Mathf.Sin(u * Mathf.PI * 2));

        //Интерполировать местоположение между двумя точками
        pos = (1 - u) * p0 + u * p1;
    }
}