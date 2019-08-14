using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //属性值
    public float speed = 3;
    public Sprite[] tankSprites;
    public GameObject tankBullets;
    public GameObject explosionPerfab;
    public GameObject defendPrefab;


    private Vector3 bulletEulerAngles;
    private SpriteRenderer sr;
    private float timeVal = 0;
    private float defendTime =3 ;
    private bool isDefended = true;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDefended)
        {
            defendPrefab.SetActive(true);
            defendTime -= Time.deltaTime;
            if(defendTime <= 0)
            {
                isDefended = false;
                defendPrefab.SetActive(false);
            }
        }

    }
    private void FixedUpdate()
    {
        if ( GameManeger.Instance.isEnd)
        {
            return;
        }
        Move();

        if (timeVal >= 0.4f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime ;
        }
    }
    //tank移动方法
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(Vector3.up * v * Time.fixedDeltaTime * speed, Space.World);
        if (v > 0)
        {
            sr.sprite = tankSprites[0];
            bulletEulerAngles = new Vector3(0,0,0);
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(tankBullets, transform.position,Quaternion.Euler(transform.position +bulletEulerAngles ));
            timeVal = 0;
        } 
    }
    //tank死亡方法
    void Die()
    {
        if(isDefended == true)
        {
            return;
        }
        Instantiate(explosionPerfab, transform.position, Quaternion.identity); 
        Destroy(gameObject); 
        GameManeger.Instance.Recover();
    }
}
