using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Vector2 rotMinMax = new Vector2(15, 20);
    public Vector2 driftMinMax = new Vector2(.25f, 2);
    public float lifeTime = 6f;
    public float fadeTime = 4f;

    [Header("Set Dynamically")]
    public WeaponType type;
    public GameObject cube; //Ссылка на вложенный куб
    public TextMesh letter; //Ссылка на TextMesh
    public Vector3 rotPerSecond; //Скорость вращения
    public float birthTime;

    private Rigidbody rigid;
    private BoundsCheck bndCheck;
    private Renderer cubeRend;


    void Awake()
    {
        //Получить ссылку на куб
        cube = transform.Find("Cube").gameObject;
        //Получить ссылки на другие компоненты
        letter = GetComponent<TextMesh>();
        rigid = GetComponent<Rigidbody>();
        bndCheck = GetComponent<BoundsCheck>();
        cubeRend = cube.GetComponent<Renderer>();

        //Выбрать случайную скорость
        Vector3 vel = Random.onUnitSphere;

        vel.z = 0; //Отобразить vel на плоскость XY
        vel.Normalize(); //Установка длины Vector3 равной 1м
        vel *= Random.Range(driftMinMax.x, driftMinMax.y);
        rigid.velocity = vel;

        //Установить угол поворота этого игрового объекта равным R:[0,0,0]
        transform.rotation = Quaternion.identity;

        //Выбрать случайную скорость вращения для вложенного куба
        rotPerSecond = new Vector3(Random.Range(rotMinMax.x, rotMinMax.y),
            Random.Range(rotMinMax.x, rotMinMax.y),
            Random.Range(rotMinMax.x, rotMinMax.y));

        birthTime = Time.time;
    }

    void Update()
    {
        cube.transform.rotation = Quaternion.Euler(rotPerSecond * Time.time);

        //Эффект растворения куба PowerUp с течением времени
        float u = (Time.time - (birthTime + lifeTime)) / fadeTime;
        //В течение lifeTime секунд значение u будет <=0. Затем оно станет положительным и через fadeTime секунд станет больше 1

        if (u >= 1)
        {
            Destroy(this.gameObject);
            return;
        }

        //Использовать u для определения альфа-значения куба и буквы
        if (u > 0)
        {
            Color c = cubeRend.material.color;
            c.a = 1f - u;
            cubeRend.material.color = c;
            //Буква тоже должна растворяться, но медленее
            c = letter.color;
            c.a = 1f - (u * 0.5f);
            letter.color = c;
        }

        if(!bndCheck.isOnScreen)
        {
            Destroy(gameObject);
        }
    }

    public void SetType(WeaponType wt)
    {
        //Получить WeaponDefinition из Main
        WeaponDefinition def = Main.GetWeaponDefinition(wt);
        //Установить цвет дочернего куба
        cubeRend.material.color = def.color;
        //Установить отображаемую букву
        letter.text = def.letter;
        type = wt;
    }

    public void AbsorbedBy(GameObject target)
    {
        Destroy(this.gameObject);
    }

}