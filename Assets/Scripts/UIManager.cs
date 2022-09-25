using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //Need to create handle on the text component
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _livesSprites; //0=0 lives | 1=1 lives | 2=2 lives | 3=3 lives
    [SerializeField]
    private Image _thrusterImg;
    [SerializeField]
    private Sprite[] _thrusterSprites; //0=empty | 1=1 second left | 2=2 seconds left | 3=3 seconds left | 4=4 seconds left | 5=5 seconds left | 6=6 seconds left | 7=7 seconds left | 8=8 seconds left | 9=9 seconds left | 10=10 seconds left | 11=11 seconds left | 12=12 seconds left | 13=13 seconds left | 14=14 seconds left | 15=Full 
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Text _ammoCountText;
    [SerializeField]
    private Text _missileCountText;


    private GameManager _gameManager;


    // Start is called before the first frame update
    void Start()
    {

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is NULL.");
        }
        
        _scoreText.text = "Score: " + 0;
        _ammoCountText.text = "Ammo: 15/15";
        _missileCountText.text = "Missile: 0/3";
        
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        //display img sprite
        //give it a new one based on the currentLives index
        _livesImg.sprite = _livesSprites[currentLives];
    }

    public void UpdateThrusterScale(int currentSecondsLeft)
    {
        _thrusterImg.sprite = _thrusterSprites[currentSecondsLeft];
    }

    public void GameOverSequence()
    {
        _gameManager.GameOver();
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverRoutine());
    }

    public void UpdateAmmoCount(int currentAmmo)
    {
        _ammoCountText.text = "Ammo: " + currentAmmo + "/15";
    }

    IEnumerator GameOverRoutine()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void UpdateMissileAmmo(int currentMissile)
    {
        _missileCountText.text = "Missile: " + currentMissile + "/3";
    }
}
