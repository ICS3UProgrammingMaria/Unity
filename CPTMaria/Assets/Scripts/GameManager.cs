using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance = null;
    public BoardManager boardScript;



    private int level = 3;

    //Awake is always called before Start functions
    void Awake()
    {
        if (instance == null)
            //If it is not, set instance to this
            instance = this;
        else if (!instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
        boardScript.SetupScene(level);
    }

    void Update()
    {
         
    }

}
