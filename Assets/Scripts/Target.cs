using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;

    public int score;
    public ParticleSystem explosion;
    void Start()
    {
        //Target生成和运动
        targetRb = GetComponent<Rigidbody>();
        transform.position = randomPos();
        targetRb.AddForce(Vector3.up * Random.Range(15, 20), ForceMode.Impulse);
        targetRb.AddTorque(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        //漏掉正确目标
        if (other.gameObject.CompareTag("Bottom"))
        {
            if (!gameObject.CompareTag("Bad"))
            {
                gameManager.UpdateLife(-1);
            }
        }
        if (other.gameObject.CompareTag("Mouse"))
        {
            if (gameManager.isGameActive && !gameManager.isGamePaused)
            {
                Destroy(gameObject);
                Instantiate(explosion, transform.position, explosion.transform.rotation);
                gameManager.ScoreRecord(score);
                //触碰到Bad目标
                if (gameObject.CompareTag("Bad"))
                {
                    gameManager.UpdateLife(-1);
                }
            }
        }
    }
    Vector3 randomPos()
    //从4个随机Pos返回一个
    {
        int index = Random.Range(0, 4);
        Vector3[] posArray =
            {
            new Vector3( 5f, -6f, 0),
            new Vector3(-1f, -6f, 0),
            new Vector3(1f, -6f, 0),
            new Vector3(3.5f, -6f, 0)
            };
        return posArray[index];
    }
}

