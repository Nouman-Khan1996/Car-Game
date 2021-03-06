using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject pausePanel,failPanel,completePanel,steering,arrows,steeringSelect, arrowsSelect,controlChangePanel;
    public GameObject hudPanel,loadingPanel;
    public Text crashCountTxt, levelCountTxt;
    public Sprite brightStar;
    public Image[] lifeBars; 
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        ChangeControls(SettingsPanel.isSteering);
        if (MainMenu.levelNum==2)
            controlChangePanel.SetActive(true);
    }
    void ChangeControls(bool _isSteering)
    {
        SettingsPanel.isSteering = _isSteering;
        PlayerPrefs.SetInt("Steering", _isSteering?1:0);
        steeringSelect.SetActive(!_isSteering);
        arrowsSelect.SetActive(_isSteering);
        steering.SetActive(_isSteering);
        arrows.SetActive(!_isSteering);
    }
    public void ChangeControlsBtn(bool _isSteering)
    { 
        AudioManager.instance.ButtonClick();
        ChangeControls(_isSteering);
    }
    public void PauseBtn()
    {
        Time.timeScale = 0;
        AudioManager.instance.ButtonClick();
        pausePanel.SetActive(true);
        AdsManager.instance?.ShowBanner(GoogleMobileAds.Api.AdSize.MediumRectangle, GoogleMobileAds.Api.AdPosition.TopLeft);
    }
    public void ResumeBtn()
    {
        Time.timeScale = 1;
        AudioManager.instance.ButtonClick();
        pausePanel.SetActive(false);
        AdsManager.instance?.HideBanner();
    }
    public void HomeBtn()
    {
        AudioManager.instance.ButtonClick();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    } 
    public void RestartBtn()
    {
        AudioManager.instance.ButtonClick();
        loadingPanel.SetActive(true);
        failPanel.SetActive(false);
        AdsManager.instance?.HideBanner();
        Collide.lifeCount = 3;
        for (int i = 0; i < lifeBars.Length; i++)
        {
            lifeBars[i].sprite = brightStar;
        }
        GameManager.instance.SetSpawnPosition();
    }
    public void NextBtn()
    {
        AudioManager.instance?.ButtonClick();
        MainMenu.levelNum++;
        Analytics.CustomEvent("LevelStart" + MainMenu.levelNum);
        if (MainMenu.levelNum > 500)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
