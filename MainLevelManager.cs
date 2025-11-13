using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLevelManager : MonoBehaviour
{
    public GameObject jangili;
    public GameObject[] Majangili = new GameObject[5];
    public GameObject Mainguy;
    public int Mainhealth;
    public int enemycount;
    public int[] enemyhealth = new int[5];
    public Vector3[] spawnpoints = new Vector3[5];

    private int Xpos, Zpos;

    private void Start()
    {
        StartCoroutine(EnemySpawn());
    }
    IEnumerator EnemySpawn()
    {
        while (enemycount < 5)
        {
            spawnproperly(enemycount);
            yield return new WaitForSeconds(0.2f);
            enemycount++;
        }
    }

    void spawnproperly(int index)
    {
        GameObject x = Instantiate(jangili, spawnpoints[index], Quaternion.identity);
        // Calculate the direction from the instantiated object to the target
        Vector3 direction = Mainguy.transform.position - x.transform.position;
        x.name = "Enemy" + index;
        // Ensure the y-component of the direction is zero to keep the object upright
        direction.y = 0;

        // If the direction is not zero, rotate the object to face the target
        if (direction != Vector3.zero)
        {
            x.transform.rotation = Quaternion.LookRotation(direction);
        }
        Majangili[index] = x;
    }

    public void SetEnemyHealth()
    {
        for (int i = 0; i < enemyhealth.Length; i++)
        {
            if (Majangili[i] != null)
            {
                EnemyMover enemyMover = Majangili[i].GetComponent<EnemyMover>();
                enemyhealth[i] = enemyMover.myhealth;
            }
        }
    }

    public void KillEnemy()
    {
        for (int i = 0; i < enemyhealth.Length; i++)
        {
            if (enemyhealth[i] <= 0)
            {
                Majangili[i].SetActive(false);
            }
        }
    }


}
