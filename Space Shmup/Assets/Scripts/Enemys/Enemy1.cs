using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    [Header("Set in Inspector: Enemy1")]
    public float waveFrequency = 1;
    //������ ���������
    public float waveWidth = 4;
    public float waveRotY = 45;

    private float x0;
    private float birthTime;

    void Start()
    {
        //���������� ��������� ���������� X ������� Enemy1
        x0 = pos.x;

        birthTime = Time.time;
    }

    //�������������� ������� Move ������ Enemy
    public override void Move()
    {
        Vector3 tempPos = pos;

        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        tempPos.x = x0 + waveWidth * sin;
        pos = tempPos;

        //��������� ������������ ��� Y
        Vector3 rot = new Vector3(0, sin * waveRotY, 0);
        this.transform.rotation = Quaternion.Euler(rot);

        //������������� �������� ����, ����� ��� Y
        base.Move();

        //print(bndCheck.isOnScreen);
    }
}