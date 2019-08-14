using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject bornPrefab;

    public GameObject[] enemyList;
    public bool createPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank", 0.8f);
        Destroy(gameObject, 0.8f);
    }

    void BornTank()
    {
        if (createPlayer)
        { Instantiate(bornPrefab, transform.position, Quaternion.identity); }
        else
        {
            int index = Random.Range(0,enemyList.Length);
            Instantiate(enemyList[index], transform.position, Quaternion.identity);
        }
    }


}
