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
        //Target���ɺ��˶�
        targetRb = GetComponent<Rigidbody>();
        transform.position = randomPos();
        targetRb.AddForce(Vector3.up * Random.Range(15, 20), ForceMode.Impulse);
        targetRb.AddTorque(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            gameManager.ScoreRecord(score);
            //��Ϸʧ��ԭ�򣺵����BadĿ��
            if (gameObject.CompareTag("Bad"))
            {
                gameManager.GameOver();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        //��Ϸʧ��ԭ��©����ȷĿ��
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }
    Vector3 randomPos()
    //��4�����Pos����һ��
    {
        int index = Random.Range(0, 4);
        Vector3[] posArray =
            {
            new Vector3( 5f, -6f, 0),
            new Vector3(-1f, -6f, 0),
            new Vector3(1f, -6f, 0),
            new Vector3(3.5f, -6f, 0)
            };
        return  posArray[index];
    }
}

