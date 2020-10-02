using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using EZCameraShake;

public class GameManager : MonoBehaviour
{
    public bool DEBUG_MODE;
    public static int currWave;
    int enemyLeft;

    bool isGameWon;
    bool isGameOver;

    [Header("References")]

    public EnemySpawner enemySpawner;
    public TextMeshProUGUI waveText;
    public GameObject debugModeGO;
    public CircleFader circleFader;


    [HideInInspector]
    public List<Face> currFaces;
    public List<Enemy> currEnemies;

    public static GameManager main;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        //-tf
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;


        if (!DEBUG_MODE)
            StartGame();
    }

    public void OnEnemyDies()
    {
        if (!DEBUG_MODE)
        {
            enemyLeft--;

            if (enemyLeft == 0)
            {
                Win();
            }
        }
    }

    void Update()
    {
        if (DEBUG_MODE && Input.GetKeyDown(KeyCode.S))
        {
            enemySpawner.SpawnRandomEnemy();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DEBUG_MODE = !DEBUG_MODE;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (currEnemies.Count > 0)
            {
                currEnemies[0].GetComponent<CentralForce>().Boost();
            }
        }

        debugModeGO.SetActive(DEBUG_MODE);
    }

    public void StartGame()
    {
        if (currWave == 0)
        {
            waveText.rectTransform.DOLocalMoveY(0, 1);
            currWave = 1;
        }

        waveText.text = "wave " + currWave.ToString();

        enemySpawner.SpawnEnemies();
        enemyLeft = currWave;
    }

    public void Win()
    {
        isGameOver = true;
        isGameWon = true;

        currWave++;
        EndGame();
    }

    public void Loose()
    {
        isGameOver = true;
        isGameWon = false;

        currWave = 0;
        EndGame();
    }

    public void EndGame()
    {
        StartCoroutine(endGameEnum());
    }

    IEnumerator endGameEnum()
    {
        waveText.transform.DOMoveY(1560, 1).SetUpdate(true);

        //Win
        if (isGameWon)
        {
            yield return new WaitForSeconds(1);

            Ball.centerBall.happyMouth.SetActive(true);

            //waveText.text = ":D";


        }
        //Loose
        else
        {
            //-ts
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            //SlowMo.main.Freeze(15);

            CameraShaker.Instance.ShakeOnce(2, 20, 0, 2);
        }

        CameraController.main.followCam = true;

        yield return new WaitForSecondsRealtime(2);

        //Scene transition
        circleFader.Fade();

        yield return new WaitForSecondsRealtime(1.5f);

        //Loads the main scene
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }


}
