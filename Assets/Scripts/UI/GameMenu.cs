using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using System;

public class GameMenu : MonoBehaviour
{
    public enum Kind
    {
        GAMESTART,
        GAMESTOP,
        RESUME,
        RESTART,
        SETTING,
        EXIT,
        GAMEOVER,
        SETTINGEXIT,
        GAMEEXIT,
        BESTSCORE
    }
    public Kind kind;

    private Text bestScore;

    public GameObject stopButton;
    public GameObject menuObj=null;
    public GameObject settingObj;
    public GameObject gameOver;

    public string sceneName;

    private Button button;

    private void OnEnable()
    {
        if(kind == Kind.BESTSCORE)
        {
            bestScore = GetComponent<Text>();
            BestScore();
        }
    }

    private void Start()
    {
        button = GetComponent<Button>();

        switch (kind)
        {
            case Kind.GAMESTOP:
                button.onClick.AddListener(GameStopButton);
                button.onClick.AddListener(GameButtonSound);
                break;
            case Kind.RESUME:
                button.onClick.AddListener(Resume);
                button.onClick.AddListener(GameButtonSound);
                break;
            case Kind.RESTART:
                button.onClick.AddListener(Restart);
                button.onClick.AddListener(GameButtonSound);
                break;
            case Kind.SETTING:
                button.onClick.AddListener(Setting);
                button.onClick.AddListener(GameButtonSound);
                break;
            case Kind.EXIT:
                button.onClick.AddListener(Exit);
                button.onClick.AddListener(GameButtonSound);
                break;
            case Kind.SETTINGEXIT:
                button.onClick.AddListener(SettingExit);
                button.onClick.AddListener(GameButtonSound);
                break;
            case Kind.GAMEEXIT:
                button.onClick.AddListener(GameExit);
                button.onClick.AddListener(GameButtonSound);
                break;
            case Kind.GAMESTART:
                button.onClick.AddListener(GameStart);
                button.onClick.AddListener(GameButtonSound);
                break;
        }
    }

    private void Update()
    {
        if(GameManager.Instance.gameState == GameManager.GameState.GameOver && kind==Kind.GAMEOVER)
        {
            GameOver();
        }

        if (kind == Kind.GAMESTOP&&Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameManager.Instance.gameState == GameManager.GameState.Playing)
            {
                GameStopButton();
                GameButtonSound();
            }
            
            else if (GameManager.Instance.gameState == GameManager.GameState.Stop)
            {
                Resume();
                GameButtonSound();
            }

        }
    }



    //???? ?????? ???? ???? ????
    private void GameStopButton()
    {
        Time.timeScale = 0;
        menuObj.SetActive(true);
        stopButton.GetComponent<Button>().interactable = false;
        GameManager.Instance.gameState = GameManager.GameState.Stop;

        SoundManager.Instance.PauseGame();
    }

    //???? ???????? ???????? ????
    private void Resume()
    {
        Time.timeScale = 1;
        menuObj.SetActive(false);
        stopButton.GetComponent<Button>().interactable = true;

        GameManager.Instance.gameState = GameManager.GameState.Playing;
        SoundManager.Instance.OnGame();
    }
    //???? ?????? ???? ?????? ????
    private void GameStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
        GameManager.Instance.gameState = GameManager.GameState.Playing;
    }

    //???? ????????
    private void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
        GameManager.Instance.gameState = GameManager.GameState.Playing;
    }

    //???? ????
    private void Setting()
    {
        settingObj.SetActive(true);
    }

    //???? ???????? ??????
    private void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        GameManager.Instance.gameState = GameManager.GameState.Main;
    }

    //???? ?????? ?? ???? ????
    private void SettingExit()
    {
        GameManager.Instance.SaveData();
        settingObj.SetActive(false);
    }

    //???? ??????
    private void GameExit()
    {
        Application.Quit();
    }

    //???? ??????
    private void GameButtonSound()
    {
        SoundManager.Instance.OnButton();
    }

    private void GameOver()
    {
        gameOver.SetActive(true);
        stopButton.GetComponent<Button>().interactable = false;
    }

    private void BestScore()
    {
        bestScore.text = "BestScore\n"+GameManager.Instance.data.bestscore;
    }
}
