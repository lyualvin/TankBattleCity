using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManeger : MonoBehaviour
{
    public int lifeValue = 2;
    public int score = 0;
    public GameObject born;
    public Text life;
    public Text scoreText;
    public Image gameover;
    public bool isEnd = false;
    public  bool isDead = false;
    private static GameManeger instance;

    public static GameManeger Instance { get => instance; set => instance = value; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        gameover.gameObject.SetActive(false);
    }
    private void Update()
    {
        life.text = lifeValue.ToString();
        scoreText.text = score.ToString();
    }

    public void Recover()
    {
        if(lifeValue <= 0)
        {
            //游戏失败
            isDead = true;
            isEnd = true;
            gameover.gameObject.SetActive(true);

        }
        else
        {
            lifeValue--;
            GameObject go =  Instantiate(born, new Vector3(-2,-8,0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }
    }

}
