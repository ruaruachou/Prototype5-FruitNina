using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    void Start()
    {
        //��Ϊ��ȡ���������Button�������������Find
        button = gameObject.GetComponent<Button>();
        //�������SetDifficulty����
        button.onClick.AddListener(SetDifficulty);
        //��������ȡGameManager���
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    void Update()
    {
        
    }
    void SetDifficulty()
    {
        //���ÿ�ʼ��Ϸ����
        gameManager.StartGame();
    }
}
