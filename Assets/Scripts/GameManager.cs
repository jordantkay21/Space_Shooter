using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    [SerializeField]
    private int _waveAmmount = 10;

    private Player _player;


    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("Player Script is NULL in GameManager");
        }


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene("Game");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        _player.UpdateWaveAmmount(_waveAmmount);
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    


}
