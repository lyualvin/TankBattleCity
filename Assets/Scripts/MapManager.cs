using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    //0-heart , 1-Wall , 2-Barrier, 3-Born 4,River ,5 -Grass
    public GameObject[] envList;
    public GameObject image;

    private int envNum = 20;
    private List<Vector3> positionList = new List<Vector3>();
    private void Start()
    { 
        WallCreate();
        BornTank();
    }

    private void Update()
    {
        ReturnMain();
    }

    void WallCreate()
    {
        Copy(envList[0], new Vector3(0, -8, 0), Quaternion.identity);
        Copy(envList[1], new Vector3(-1, -8, 0), Quaternion.identity);
        Copy(envList[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i =-1; i<2;i++)
        {
            Copy(envList[1], new Vector3(i, -7, 0), Quaternion.identity);
        }

        for(int i =0; i<envNum; i++)
        {
            Copy(envList[1], CreateRandomPositon(), Quaternion.identity);
            Copy(envList[1], CreateRandomPositon(), Quaternion.identity);
            Copy(envList[2], CreateRandomPositon(), Quaternion.identity);
            Copy(envList[4], CreateRandomPositon(), Quaternion.identity);
            Copy(envList[5], CreateRandomPositon(), Quaternion.identity);
        }
        
    }
    void Copy(GameObject CreateObject, Vector3 position, Quaternion quaternion)
    {
        GameObject go = Instantiate(CreateObject, position, quaternion)  ;
        go.transform.SetParent(this.transform);
        positionList.Add(position);
    }

    private Vector3 CreateRandomPositon()
    {
        while(true)
        {
            Vector3 position = new Vector3(Random.Range(-9,10), Random.Range(-6,8),0);
            if (!HasPostion(position))
            {
                positionList.Add(position);
                return position;
            }
        }
    }

    private bool HasPostion(Vector3 pos)
    {
        if (positionList.Contains(pos))
        {
            return true;
        }
        return false;

    }

    void BornTank()
    {
        GameObject go = Instantiate(envList[3], new  Vector3(-2,-8,0), Quaternion.identity);
        go.GetComponent<Born>().createPlayer = true;

        Copy(envList[3], new Vector3(-10,8,0), Quaternion.identity);
        Copy(envList[3], new Vector3(10, 8, 0), Quaternion.identity);
        Copy(envList[3], new Vector3(0, 8, 0), Quaternion.identity);

        InvokeRepeating("RandomTank",4,5);

    }

    void RandomTank()
    {
        int rand = Random.Range(0, 3);
        Vector3 pos = new Vector3();
        if(rand == 0)
        {
            pos = new Vector3(-10, 8, 0);
        }
        else if(rand == 1)
        {
            pos = new Vector3(10, 8, 0);
        }
        else
        {
            pos = new Vector3(0, 8, 0);
        }

        Copy(envList[3], pos , Quaternion.identity);
    }
    

    void ReturnMain()
    {
        if (image.activeSelf == true)
        {
            
            SceneManager.LoadScene(0);
        }
    }
}
