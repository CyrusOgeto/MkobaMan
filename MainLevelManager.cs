using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLevelManager : MonoBehaviour
{
    public GameObject jangili;
    public GameObject mainjangili;
    public GameObject[] Majangili = new GameObject[5];
    public GameObject Mainguy;
    public Camera mainCamera;
    public int Mainhealth;
    public int enemycount;
    public int[] enemyhealth = new int[5];
    public Vector3[] spawnpoints = new Vector3[5];

    private int Xpos, Zpos;

    private bool isPaused = false;
    public GameObject InGameUI, PauseUI;
    MkobaController mkobaController;

    private void Start()
    {
        mkobaController = Mainguy.GetComponentInChildren<MkobaController>();
        if(mkobaController != null )
        {
            Debug.Log("I have found mkoba controller");
        }
        StartCoroutine(EnemySpawn());
    }
    private void Update()
    {
        GetAnazua();
        CheckActivity();
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
        EnemyMover enemyMover = x.GetComponent<EnemyMover>();
        enemyMover.mainguy = Mainguy;
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
    public void CheckActivity()
    {
        int randomChoice = Random.Range(0, Majangili.Length);
        if(mkobaController.idle)
        {
            EnemyMover enemyMover = Majangili[randomChoice].GetComponent<EnemyMover>();
            enemyMover.move = true;
            enemyMover.canact = true;
            mkobaController.idle = false;   
            ///Set mkoba to look alive
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
    public void GetAnazua()
    {
        var y = mainCamera.GetComponent<BoondocksCam>();
        // Check if the mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            y.SetDanceMode(true);
            // Get the mouse position in screen coordinates
            Vector3 mousePosition = Input.mousePosition;

            // Convert the screen position to a ray
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hitInfo;

            // Perform the raycast
            if (Physics.Raycast(ray, out hitInfo))
            {
                // Check if the object hit by the ray has the tag "Adui"
                if (hitInfo.collider.CompareTag("Adui"))
                {
                    //The clicked object has the tag "Adui"
                    Debug.Log("Clicked on an object with tag 'Adui'");//The Detector
                    var x = Mainguy.GetComponent<MkobaMover>();
                    x.Anazua = hitInfo.collider.gameObject;
                    y.SetDanceMode(false);
                }
                else
                {

                }
            }
            else
            {
                // The ray did not hit any object
                Debug.Log("Clicked on an empty space");
            }
        }
    }
    //For toogling pause
    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            //Time.timeScale = 0f; 
            // Pause the game
            InGameUI.gameObject.SetActive(false);
            PauseUI.gameObject.SetActive(true);
        }
        else
        {
            //Time.timeScale = 1f; 
            // Resume the game
            InGameUI.gameObject.SetActive(true);
            PauseUI.gameObject.SetActive(false);
        }
    }
    public void PauseBtn()
    {
        TogglePause();
    }
    public void LoadScene(string sceneName)
    {
        PauseUI.gameObject.SetActive(false);
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            Debug.Log($"Loading {sceneName}: {asyncLoad.progress * 100}%");
            yield return null;
        }
        Debug.Log($"Scene {sceneName} loaded successfully.");
    }
}
