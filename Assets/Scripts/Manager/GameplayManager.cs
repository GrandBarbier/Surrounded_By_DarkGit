using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public GameObject facemask;
    public GameObject player;
    public Animator playerAnimator;
    public GameObject deathParticles;
    
    [Header("Death")]
    public GameObject vignette;
   
    public float restartTime;
    private float _actualRestartTime;

    private bool doOnce = true;
    
    void Start()
    {
        deathParticles.SetActive(false);
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
        Debug.Log("Death");
        
        Gears.gears.menuManager.EnablePause(false);

        if ((doOnce))
        {
            doOnce = false;
            StartCoroutine(LevelManager.FadeDuration(Gears.gears.menuManager.blackPanel, new Color(0f, 0f, 0f, 0f),
                new Color(0f, 0f, 0f, 1f), 0.8f, onComplete: DeathEffect));
        }
        
    }

    public void DeathEffect()
    {
        StartCoroutine(DeathEffectC());
        return;
        
        vignette.SetActive(false);
        player.GetComponent<Movement>().enabled = false;
        deathParticles.SetActive(true);
        _actualRestartTime -= Time.deltaTime;
        if (_actualRestartTime <= 4.4f)
        {
            facemask.GetComponent<Renderer>().materials[1].color = Color.black;
            facemask.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", Color.black);
        }
        if (_actualRestartTime <= 0)
        {
            _actualRestartTime = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        playerAnimator.SetBool("IsDead", true);
    }

    public IEnumerator DeathEffectC()
    {
        vignette.SetActive(false);
        player.GetComponent<Movement>().enabled = false;
        deathParticles.SetActive(true);
        playerAnimator.SetBool("IsDead", true);
        _actualRestartTime = 4.4f;
        while (_actualRestartTime > 0)
        {
            facemask.GetComponent<Renderer>().materials[1].color = Color.black;
            facemask.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", Color.black);
            _actualRestartTime -= Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}