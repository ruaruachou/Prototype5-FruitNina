using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targetList;
    public TextMeshProUGUI scoreText;
    private int score;
    void Start()
    {
        StartCoroutine(SpawnTarget());
        
    }

    void Update()
    {

    }
    IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            int index = Random.Range(0, targetList.Count);
            Instantiate(targetList[index]);
            ScoreRecord(5);
        }
    }
    void ScoreRecord(int addScore)
    {
        score += addScore;//计算分数
        scoreText.text = "Score:" + score;//显示分数
    }
}
