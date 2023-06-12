using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTail : MonoBehaviour
{
    private BoxCollider mouseBC;
    void Start()
    {
        mouseBC = GetComponent<BoxCollider>();
    }

    void Update()
    {
        // 获取鼠标在屏幕空间中的位置
        Vector3 mousePosition = Input.mousePosition;

        // 将鼠标位置转换为世界空间中的位置
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 按住鼠标时，更新物体的位置
        if (Input.GetMouseButton(0))
        {
            mouseBC.enabled = true;
            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        }
        //松开时，碰撞器关闭
        else
            mouseBC.enabled = false;
    }
}
