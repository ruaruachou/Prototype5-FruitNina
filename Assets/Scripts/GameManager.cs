using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{  //目标列表
    public List<GameObject> targetList;

    //UI组件
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI recordText;
    public Button restartButton;
    private Button menuButton;
    public GameObject menuPanel;
    public GameObject difficultyTitle; //菜单：难度选择。同时也是游戏开始的开关

    //游戏状态
    public bool isGameActive;
    public bool isGamePaused = false;

    //分数、生命、计时
    private int score;
    public int lives = 3;
    public int currentTime;
    void Start()
    {
        isGamePaused = false;
        //获取Menu按钮，并为其注册暂停方法
        menuButton = GameObject.Find("MenuButton").GetComponent<Button>();
        menuButton.onClick.AddListener(PauseGame);
    }

    void Update()
    {

    }
    IEnumerator SpawnTarget()
    {   //协程：随机生成Target，并用isGameActive控制开关
        while (isGameActive)
        {
            yield return new WaitForSeconds(1.5f);
            int index = Random.Range(0, targetList.Count);
            Instantiate(targetList[index]);
        }
    }
    IEnumerator Timer()
    {
        //协程：每秒时间减1，为0时退出
        while (isGameActive)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            timeText.text = currentTime.ToString();
            if (currentTime <= 0)
                break;
        }
        GameOver();//在整个while循环结束后执行GameOver()
    }
    public void ScoreRecord(int addScore)
    {
        //分数记录方法，addScore参数在每个Target上单独传入，在GameManager中汇总分数
        score += addScore;//计算分数
        scoreText.text = "Score:" + score;//显示分数
        recordText.text = "Record: " + RecordManager.Instance.recordScore;
  
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
       
            
            if (score > RecordManager.Instance.recordScore) 
            {
                RecordManager.Instance.recordScore = score;
            recordText.text = "Record: " + RecordManager.Instance.recordScore;
            RecordManager.Instance.SaveRecord();
        }
        Debug.Log(RecordManager.Instance.recordScore);
    }
    public void UpdateLife(int liveChange)
    {
        //生命方法，参考ScoreRecord()
        if (isGameActive)//确保游戏结束后停止变化Life，避免显示Bug
        {
            lives += liveChange;
            if (lives <= 0)
            {
                lives = 0;
                GameOver();
            }
            livesText.text = "Lives: " + lives;
        }
    }

    public void Restart()
    {
        //重新加载场景
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isGamePaused = false;
        Time.timeScale = 1;
    }
    public void StartGame()
    {
        //在本方法中重置各种参数
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        StartCoroutine(Timer());
        ScoreRecord(0);
        UpdateLife(0);
        difficultyTitle.gameObject.SetActive(false); //游戏开始时隐藏难度菜单
        lives = 3;
    }
    public void StartGameMid()
    {
        //在中等难度按钮中调用一次本方法，由于SetDifficulty已经调用过StartGame了，所以本方法没有倒计时协程
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        ScoreRecord(0);
        UpdateLife(0);
        difficultyTitle.gameObject.SetActive(false); //游戏开始时隐藏难度菜单
        lives = 3;
    }
    public void StartGameHard()
    {
        //调用两次生成目标协程
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        StartCoroutine(SpawnTarget());
        ScoreRecord(0);
        UpdateLife(0);
        difficultyTitle.gameObject.SetActive(false); //游戏开始时隐藏难度菜单
        lives = 3;
    }
    public void PauseGame()
    {
        //暂停游戏的方法
        if (!isGamePaused)
        {
            Debug.Log("Pause");
            isGamePaused = true;
            Time.timeScale = 0;
            menuPanel.gameObject.SetActive(true);//菜单界面

        }
        else if (isGamePaused)
        {
            //这里必须用else，否则点击暂停会立即恢复
            Debug.Log("Recover");
            isGamePaused = false;
            Time.timeScale = 1;
            menuPanel.gameObject.SetActive(false);
        }
    }
}
