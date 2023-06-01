using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targetList;
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
        }
    }
}
