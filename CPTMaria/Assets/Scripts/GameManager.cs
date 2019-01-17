using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public float turnDelay = 0.1f;
    public static GameManager instance = null;
    public BoardManager boardScript;
    public int pointsPerFood = 10;          
    public int pointsPerSoda = 20;
    [HideInInspector] public bool playersTurn = true;
    public int playerFoodPoints = 100;

    private int level = 1;
    private List<Enemy> enemies;
    private bool enemiesMoving;

    //Awake is always called before Start functions
    void Awake()
    {
        if (instance == null)
            //If it is not, set instance to this
            instance = this;
        else if (!instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        enemies = new List<Enemy>();
        boardScript = GetComponent<BoardManager>();
        InitGame();

    }

    void InitGame()
    {
        enemies.Clear();
        boardScript.SetupScene(level);
    }

    void Update()
    {
        if (playersTurn || enemiesMoving)
            return;

        StartCoroutine(MoveEnemies());

    }

    public void AddEnemyToList(Enemy script)
    {
        //Add enemy to list enemies
        enemies.Add(script);
    }

    public void GameOver()
    {
        enabled = false;
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);
      
        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }
        //when enemies are fininshed moving set players turn to true
        playersTurn = true;
        //when enemies are done moving set enemiesMoving to false
        enemiesMoving = false;

    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //add one to our level number
        level++;
        InitGame();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
}
