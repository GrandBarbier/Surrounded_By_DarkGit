using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public GameObject player;
    public Animator playerAnimator;
    public GameObject deathParticles;
    
    [Header("Death")]
    public GameObject deathUI;
    public TMP_Text deathText;
    public float restartTime;
    private float _actualRestartTime;
    void Start()
    {
        deathParticles.SetActive(false);
        deathUI.SetActive(false);
        _actualRestartTime = restartTime;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        
        if(Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

       

       
    }

    public void Death()
    {
        deathUI.SetActive(true);
        player.GetComponent<Movement>().enabled = false;
        _actualRestartTime -= Time.deltaTime;
        if (_actualRestartTime <= 0)
        {
            _actualRestartTime = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        deathText.text = "Restart in  " + Mathf.FloorToInt(_actualRestartTime);
        deathParticles.SetActive(true);
        playerAnimator.SetBool("IsDead", true);
        
    }
}