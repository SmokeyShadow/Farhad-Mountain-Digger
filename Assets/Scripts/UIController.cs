using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    #region STATIC FIELDS
    private static UIController _instance;
    public static UIController instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<UIController>();
            return _instance;
        }
    }
    #endregion

    #region PUBLIC FIELDS
    public bool OnPanel;
    #endregion

    #region SERIALIZED FIELDS
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Text highScoreText;
    #endregion

    #region PRIVATE FIELDS
    float highScore;
    #endregion

    #region PUBLIC METHODS
    void Update()
    {
        highScore += Time.deltaTime;
        highScoreText.text = ((int)highScore).ToString();
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        OnPanel = true;
    }
    public void RestartBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void QuitBtn()
    {
        Application.Quit();
    }
    #endregion
}
