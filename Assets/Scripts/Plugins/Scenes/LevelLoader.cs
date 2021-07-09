using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject background,characterChoice,canvas;
    public Button nextBut;
    public Animator backgroundAni;
    GameManager gameManager;
    private bool isBegin = true;
    private bool isUnclicked = true;

    private void Awake()
    {
        gameManager= GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (isBegin)
        {
            if (Input.anyKeyDown)
            {
                canvas.SetActive(true);
                characterChoice.SetActive(true);
                backgroundAni.SetBool("isExit", true);
                isBegin = false;  
            }
        }
        if (isUnclicked)
        {
            nextBut.onClick.AddListener(gameManager.LoadMap1);
            isUnclicked = false;
        }
    }
}
