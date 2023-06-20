using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTail : MonoBehaviour
{
    private GameManager gameManager;
    private BoxCollider mouseBC;
    void Start()
    {
        mouseBC = GetComponent<BoxCollider>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        // ��ȡ�������Ļ�ռ��е�λ��
        Vector3 mousePosition = Input.mousePosition;

        // �����λ��ת��Ϊ����ռ��е�λ��
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // ��ס���ʱ�����������λ��
        if (gameManager.isGameActive&gameManager.isGamePaused==false)
        {
            if (Input.GetMouseButton(0))
            {
                mouseBC.enabled = true;
                transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
            }
            //�ɿ�ʱ����ײ���ر�
            else
                mouseBC.enabled = false;
        }
    }
}
