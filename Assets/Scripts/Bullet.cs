using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10;

    private Collider2D bulletCollider;
    public  bool isPlayerBullet = true;
    // Start is called before the first frame update
    void Start()
    {
        bulletCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        BulletMove();
    }

    void BulletMove()
    {
        transform.Translate(transform.up * bulletSpeed * Time.deltaTime , Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "Tank":
                if (isPlayerBullet == false)
                { 
                    other.SendMessage("Die");
                    Destroy(gameObject);
                }
                
                break;
            case "Heart":
                other.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Wall":
                Destroy(other.gameObject);
                Destroy(gameObject);
                break;
            case "Barrier":
                Destroy(gameObject);
                break;
            case "Enemy":
                if(isPlayerBullet)
                {
                    other.SendMessage("Die");
                    Destroy(gameObject);

                }
                
                break;
        }
    }
}

