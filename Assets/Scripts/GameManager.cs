using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Wave System
    private Scene _currentScene;
    private string _sceneName;
    private int _sceneIndex;
    
    
    [SerializeField]
    private bool _isGameOver;

    [SerializeField]
    private int _waveAmmount;

    private Player _player;


    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("Player Script is NULL in GameManager");
        }

        _currentScene = SceneManager.GetActiveScene();
        _sceneName = _currentScene.name;
        _sceneIndex = CheckCurrentSceneIndex();
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

        SetWaveAmmount();
    }


    public void GameOver()
    {
        _isGameOver = true;
    }
    public void SetWaveAmmount()
    {
        if (_sceneName == "Game")
        {
            _waveAmmount = 10;
        }
        else if (_sceneName == "Wave_2")
        {
            _waveAmmount = 15;
        }
        else if (_sceneName == "Wave_3")
        {
            _waveAmmount = 20;
        }
        else if (_sceneName == "Wave_4")
        {
            _waveAmmount = 25;
        }
        else if (_sceneName == "Wave_5")
        {
            _waveAmmount = 50;
            // + need to kill boss
        }


        _player.UpdateWaveAmmount(_waveAmmount);

    }

    public int CheckCurrentSceneIndex()
    {
        if (_sceneName == "Game")
        {
            return 1;
        }
        else if (_sceneName == "Wave_2")
        {
            return 2;
        }
        else if (_sceneName == "Wave_3")
        {
            return 3;
        }
        else if (_sceneName == "Wave_4")
        {
            return 4;
        }
        else if (_sceneName == "Wave_5")
        {
            return 5;
        }
        else
        {
            return 0;
        }
    }

    public void LoadNextScene()
    {
        _sceneIndex++;
        SceneManager.LoadScene(_sceneIndex);
    }

    


}
