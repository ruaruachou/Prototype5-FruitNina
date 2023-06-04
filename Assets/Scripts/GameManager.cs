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
    public GameObject difficultyTitle; //�˵����Ѷ�ѡ��ͬʱҲ����Ϸ��ʼ�Ŀ���
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
            //Э�̣��������Target
            yield return new WaitForSeconds(1.5f);
            int index = Random.Range(0, targetList.Count);
            Instantiate(targetList[index]);
        }
    }
    public void ScoreRecord(int addScore)
    {
        score += addScore;//�������
        scoreText.text = "Score:" + score;//��ʾ����
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void Restart()
    {
        //���¼��س���
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        ScoreRecord(0);
        difficultyTitle.gameObject.SetActive(false); //��Ϸ��ʼʱ�����ѶȲ˵�
    }
}
