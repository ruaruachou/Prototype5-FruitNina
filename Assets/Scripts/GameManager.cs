using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targetList;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject difficultyTitle; //菜单：难度选择。同时也是游戏开始的开关
    public bool isGameActive;
    private int score;
    void Start()
    {
        
    }

    void Update()
    {

    }
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            //协程：随机生成Target
            yield return new WaitForSeconds(1.5f);
            int index = Random.Range(0, targetList.Count);
            Instantiate(targetList[index]);
        }
    }
    public void ScoreRecord(int addScore)
    {
        score += addScore;//计算分数
        scoreText.text = "Score:" + score;//显示分数
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void Restart()
    {
        //重新加载场景
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        ScoreRecord(0);
        difficultyTitle.gameObject.SetActive(false); //游戏开始时隐藏难度菜单
    }
}
