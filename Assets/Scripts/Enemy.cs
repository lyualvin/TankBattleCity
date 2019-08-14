using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //属性值
    public float speed = 3;
    public Sprite[] tankSprites;
    public GameObject tankBullets;
    public GameObject explosionPerfab;

    private Vector3 bulletEulerAngles;
    private SpriteRenderer sr;
    private float timeVal = 0;
    private float changeTimeVal = 4;
    private float v =-1;
    private float h;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (timeVal >= 3f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    //tank移动方法
    private void Move()
    {
        if(changeTimeVal >= 4)
        {
            int tmp = Random.Range(0,8);
            if(tmp >=5)
            {
                v = -1;
                h = 0;
            }
            else if(tmp == 0)
            {
                v = 1;
                h = 0;
            }
            else if(tmp >=1 && tmp <= 2)
            {
                v = 0;
                h = 1;
            }
            else 
            {
                v = 0;
                h = -1;
            }
            changeTimeVal = 0;

        }
        else
        {
            changeTimeVal += Time.fixedDeltaTime;
        }

        transform.Translate(Vector3.up * v * Time.fixedDeltaTime * speed, Space.World);
        if (v > 0)
        {
            sr.sprite = tankSprites[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
        else if (v < 0)
        {
            sr.sprite = tankSprites[2];
            bulletEulerAngles = new Vector3(0, 0, 180);
        }
        if (v != 0)
        {
            return;
        }


        transform.Translate(Vector3.right * h * Time.fixedDeltaTime * speed, Space.World);
        if (h > 0)
        {
            sr.sprite = tankSprites[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
        else if (h < 0)
        {
            sr.sprite = tankSprites[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
    }
    //tank攻击方法
    private void Attack()
    {
        Instantiate(tankBullets, transform.position, Quaternion.Euler(transform.position + bulletEulerAngles));
        timeVal = 0;
    }
    //tank死亡方法
    void Die()
    { 
        Instantiate(explosionPerfab, transform.position, Quaternion.identity);
        Destroy(gameObject);

        GameManeger.Instance.score++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            changeTimeVal = 4;
        }
    }
}
