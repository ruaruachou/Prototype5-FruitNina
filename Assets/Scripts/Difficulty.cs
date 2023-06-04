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
        //因为获取的是自身的Button组件，所以无需Find
        button = gameObject.GetComponent<Button>();
        //按键监测SetDifficulty方法
        button.onClick.AddListener(SetDifficulty);
        //搜索并获取GameManager组件
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    void Update()
    {
        
    }
    void SetDifficulty()
    {
        //调用开始游戏方法
        gameManager.StartGame();
    }
}
