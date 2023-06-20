using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{  //Ŀ���б�
    public List<GameObject> targetList;

    //UI���
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI recordText;
    public Button restartButton;
    private Button menuButton;
    public GameObject menuPanel;
    public GameObject difficultyTitle; //�˵����Ѷ�ѡ��ͬʱҲ����Ϸ��ʼ�Ŀ���

    //��Ϸ״̬
    public bool isGameActive;
    public bool isGamePaused = false;

    //��������������ʱ
    private int score;
    public int lives = 3;
    public int currentTime;
    void Start()
    {
        isGamePaused = false;
        //��ȡMenu��ť����Ϊ��ע����ͣ����
        menuButton = GameObject.Find("MenuButton").GetComponent<Button>();
        menuButton.onClick.AddListener(PauseGame);
    }

    void Update()
    {

    }
    IEnumerator SpawnTarget()
    {   //Э�̣��������Target������isGameActive���ƿ���
        while (isGameActive)
        {
            yield return new WaitForSeconds(1.5f);
            int index = Random.Range(0, targetList.Count);
            Instantiate(targetList[index]);
        }
    }
    IEnumerator Timer()
    {
        //Э�̣�ÿ��ʱ���1��Ϊ0ʱ�˳�
        while (isGameActive)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            timeText.text = currentTime.ToString();
            if (currentTime <= 0)
                break;
        }
        GameOver();//������whileѭ��������ִ��GameOver()
    }
    public void ScoreRecord(int addScore)
    {
        //������¼������addScore������ÿ��Target�ϵ������룬��GameManager�л��ܷ���
        score += addScore;//�������
        scoreText.text = "Score:" + score;//��ʾ����
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
        //�����������ο�ScoreRecord()
        if (isGameActive)//ȷ����Ϸ������ֹͣ�仯Life��������ʾBug
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
        //���¼��س���
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isGamePaused = false;
        Time.timeScale = 1;
    }
    public void StartGame()
    {
        //�ڱ����������ø��ֲ���
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        StartCoroutine(Timer());
        ScoreRecord(0);
        UpdateLife(0);
        difficultyTitle.gameObject.SetActive(false); //��Ϸ��ʼʱ�����ѶȲ˵�
        lives = 3;
    }
    public void StartGameMid()
    {
        //���е��ѶȰ�ť�е���һ�α�����������SetDifficulty�Ѿ����ù�StartGame�ˣ����Ա�����û�е���ʱЭ��
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        ScoreRecord(0);
        UpdateLife(0);
        difficultyTitle.gameObject.SetActive(false); //��Ϸ��ʼʱ�����ѶȲ˵�
        lives = 3;
    }
    public void StartGameHard()
    {
        //������������Ŀ��Э��
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        StartCoroutine(SpawnTarget());
        ScoreRecord(0);
        UpdateLife(0);
        difficultyTitle.gameObject.SetActive(false); //��Ϸ��ʼʱ�����ѶȲ˵�
        lives = 3;
    }
    public void PauseGame()
    {
        //��ͣ��Ϸ�ķ���
        if (!isGamePaused)
        {
            Debug.Log("Pause");
            isGamePaused = true;
            Time.timeScale = 0;
            menuPanel.gameObject.SetActive(true);//�˵�����

        }
        else if (isGamePaused)
        {
            //���������else����������ͣ�������ָ�
            Debug.Log("Recover");
            isGamePaused = false;
            Time.timeScale = 1;
            menuPanel.gameObject.SetActive(false);
        }
    }
}
