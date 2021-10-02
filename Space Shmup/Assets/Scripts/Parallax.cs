using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject poi; //Корабль игрока
    public GameObject[] panels; //Прокручиваемые панели переднего плана
    public float scrollSpeed = -30f;
    public float motionMult = 0.25f; // Определяет степень реакции панелей на перемещение корабля игрока

    private float panelHit; //Высота каждой панели
    private float depth; //Глубина панелей

    void Start()
    {
        panelHit = panels[0].transform.localScale.y;
        depth = panels[0].transform.position.z;

        //Установить панели в начальные позиции
        panels[0].transform.position = new Vector3(0, 0, depth);
        panels[1].transform.position = new Vector3(0, panelHit, depth);
    }

    void Update()
    {
        float tY, tX = 0;
        tY = Time.time * scrollSpeed % panelHit + (panelHit * 0.5f);

        if (poi != null)
        {
            tX = -poi.transform.position.x * motionMult;
        }

        //Сместить панель panels[0]
        panels[0].transform.position = new Vector3(tX, tY, depth);
        //Сместить панель panels[1], чтобы создать эффект непрерывности звездного поля
        if (tY >= 0)
        {
            panels[1].transform.position = new Vector3(tX, tY - panelHit, depth);
        }
        else
        {
            panels[1].transform.position = new Vector3(tX, tY + panelHit, depth);
        }
    }
}