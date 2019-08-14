using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite sprite;

    public GameObject explosionPrefab;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    public void Die()
    {

        sr.sprite = sprite;
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameManeger.Instance.isEnd = true;
        GameManeger.Instance.gameover.gameObject.SetActive(true);

    }
}
