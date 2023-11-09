using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pannels : MonoBehaviour
{
    [SerializeField] private GameplayManager _gm;
    public GameObject pannels;
    public Player player;
    public GameObject _deathpanel;
    public GameObject _player;
    public GameObject _dusman;
    public GameObject _score;
    public GameObject _cemberler;
    private void Start()
    {
        pannels.SetActive(true);
    }
    public void gamePause()
    {
        Time.timeScale = 0;
        pannels.SetActive(true);

    }
    public void gameResume()
    {
        pannels.SetActive(false);
        Time.timeScale = 1;
    }
    public void gameExit()
    {
        _gm.GameEnded();
        Time.timeScale = 1f;
    }
    public void gameExtra() {
        player.Can += 3;
        _deathpanel.SetActive(false);
        _player.SetActive(true);
        _dusman.SetActive(true);
        _cemberler.SetActive(true);
        _score.SetActive(true);
        Time.timeScale = 2f;  

    }
}
