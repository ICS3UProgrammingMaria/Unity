using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{
    public int wallDamage = 1;
    public int pointsPerFood = 10;
    public int pointsPerSoda = 20;
    public float restartLevelDelay = 1f;

    private Animator animator;
    private int food;


    protected override void Start()
    {
        //Get a component reference Player's animator component
        animator = GetComponent<Animator>();

        //Get the current food point total stored in GameManager.instance between levels.
        food = GameManager.instance.playerFoodPoints;

        //Call the Start function of the MovingObject class.
        base.Start();
    }

    private void OnDisable()
    {
        //When Player object is disabled, store the current local food total in the GameManager so it can be re-loaded in next level.
        GameManager.instance.playerFoodPoints = food;
    }

    void Update()
    {
        if (!GameManager.instance.playersTurn) return;
        //stores the horizontal move direction
        int horizontal = 0;   
        int vertical = 0;
        horizontal = (int) Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
            vertical = 0;


        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove<Wall>(horizontal, vertical);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Food")
        {
            food += pointsPerFood;
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Soda")
        {
            food += pointsPerSoda;
            other.gameObject.SetActive(false);
        }
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        //food--;
        Debug.Log("***food = " + food);

        base.AttemptMove<T>(xDir, yDir);
        RaycastHit2D hit;

        if (Move(xDir, yDir, out hit))
        {
            //Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
        }

        CheckIfGameOver();
        GameManager.instance.playersTurn = false;

    }

    protected override void OnCantMove <T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("playerChop");
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void LoseFood (int loss)
    {
        animator.SetTrigger("playerHit");
        food -= loss;
        CheckIfGameOver();
    }

    private void CheckIfGameOver ()
    {
        if (food <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

}