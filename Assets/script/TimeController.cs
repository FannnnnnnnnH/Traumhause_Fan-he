using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;


/*
 * TimeController.cs
 * 
 * This script manages the in-game time and day-night cycle. It controls the progression of time,
 * handles lighting changes, and manages season transitions.该脚本管理游戏中的时间和昼夜循环。它控制时间的流逝，处理光照变化，并管理季节过渡。
 * 
 * Public Properties:
 * - uhrZeit: The UI text element displaying the in-game time.显示游戏时间的 UI 文本元素。
 * - dayLight: The main directional light representing the sun.代表太阳的主定向光。
 * - nightLight: The secondary light representing moonlight.月亮
 * - rotationSpeed: The rotation speed of the day-night cycle.昼夜循环的旋转速度。
 * - currentSeason: The current season in the game.当前的季节。
 * 
 * Methods:
 * - UpdateTimer: Updates the in-game timer and manages time-related game events.更新游戏计时器并处理与时间相关的游戏事件。
 * - ChangeDayAndNight: Handles logic for changes that occur after the transition between day and night.
 * - DayAndNightRotation: Controls the rotation of the sun and moon during the day-night cycle.控制昼夜循环期间太阳和月亮的旋转。
 * - ChangeSeason: Changes the current season in the game.
 * - SaveTime: Saves the current game time and related data using PlayerPrefs.保存当前游戏时间和相关数据。
 * - LoadTime: Loads the saved game time and related data from PlayerPrefs.加载保存的游戏时间和相关数据。
 * - SetDefaultValues: Sets default values for variables in case PlayerPrefs values are missing or invalid.在 PlayerPrefs 的值丢失或无效的情况下为变量设置默认值。
 * 
 * Author: [FannnHAHA]
 * Date: []
 */


public class TimeController : MonoBehaviour
{
    //南北半球控制
    public enum DayTime { Day, Night }
    public Light daylLight; 
    public Light nachtLicht;
    public float rotationSpeed = 1f; // 自转速度，调整此值来控制旋转速度
    public static DayTime currentDayTime;

    //这里是计时器
    public Text uhrZeit;
    private int gameTimeInMinutes = 0;
    private int gameTimeInSeconds = 0;
    private float timePlus = 0;
    private float timeDay = 0;
    private int dayCounter;
    
    //四级控制seson
    public enum Season { Spring, Summer, Autumn, Winter }
    public Season currentSeason;

    public GameObject uiLichtAn;
    private bool isUiLichtAn=true;

    private LightController lightController;
    private bool lightTrigger = true;

    public PlayableDirector lichtDirector;

    private void Awake()
    {
        LoadTime();

    }
    // Start is called before the first frame update
    void Start()
    {
        //directionalLight.GetComponent<Transform>();
        // LoadTime();
        uiLichtAn.SetActive(false);
        lightController = GetComponent<LightController>();
    }

    // Update is called once per frame

    //更新每秒的变化
    private void FixedUpdate()
    {

        UpdateTimer();
        DayandNightdrehen();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SaveTime();
        }
       // Debug.Log(nachtLicht.transform.eulerAngles.x);
        if (0<nachtLicht.transform.eulerAngles.x && nachtLicht.transform.eulerAngles.x<180 )
        {
            //设置夜晚是夜晚
            currentDayTime = DayTime.Night;
           
            if (Input.GetKeyDown(KeyCode.Y)&& lightTrigger)
            {
                uiLichtAn.SetActive(false);
                lichtDirector.Play();
                Debug.Log("Turning on lights sequentially.");
                lightController.TurnOnLightsSequentially(1.0f);
                lightTrigger = false;
                lightController.particleGluehwuermchen.Play();
                isUiLichtAn = false;
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                isUiLichtAn = false;
            }
            if (isUiLichtAn)
            {
                uiLichtAn.SetActive(true);
            }
            else
            {
                uiLichtAn.SetActive(false);
            }

               


        }
        else if( nachtLicht.transform.eulerAngles.x > 180 && !lightTrigger) 
        {
            currentDayTime = DayTime.Day;
            Debug.Log("Enabling flashlights.");
            lightController.LightEnableFalsh(); 
            lightController.particleGluehwuermchen.Clear(); 
            lightTrigger = true; }
        //ChangeDayandNight();
    }

    private void UpdateTimer()
    {
        timePlus += Time.deltaTime;
        timeDay += Time.deltaTime * rotationSpeed;

        gameTimeInSeconds = Mathf.FloorToInt(timePlus % 60f);
        gameTimeInMinutes = Mathf.FloorToInt(timePlus / 60f) % 10;

        if (timeDay > 180)
        {
            gameTimeInSeconds = 0;
            timeDay = 0;
        }

        if (gameTimeInMinutes >= 5)
        {
            timePlus = 0;
            gameTimeInMinutes = 0;
            dayCounter++;

            if (dayCounter % 3 == 0)
            {
                ChangeSeason();
            }
        }

        uhrZeit.text = string.Format("{0:00}:{1:00}", gameTimeInMinutes, gameTimeInSeconds);
        DayandNightdrehen();


    }
    //日夜改变之后会发什么
    private void ChangeDayandNight()
    {
       // throw new NotImplementedException();
    }
    //这里控制日夜旋转
    private void DayandNightdrehen()
    {
        float t = timeDay / 180.0f;
        float sunRotation = Mathf.Lerp(-90, 270, t);
        float moonRotation = sunRotation - 180;
        daylLight.transform.rotation = Quaternion.Euler(sunRotation, 0, 0);
        nachtLicht.transform.rotation = Quaternion.Euler(moonRotation, 0, 0);
    }


    //四季变化，但是还没有做
    void ChangeSeason()
    {
        switch (currentSeason)
        {
            case Season.Spring:
                currentSeason = Season.Summer;
                break;
            case Season.Summer:
                currentSeason = Season.Autumn;
                break;
            case Season.Autumn:
                currentSeason = Season.Winter;
                break;
            case Season.Winter:
                currentSeason = Season.Spring;
                break;
        }
    }

    public void SaveTime()
    {
        PlayerPrefs.SetFloat("TimePlus", timePlus);
        PlayerPrefs.SetInt("GameTimeInSeconds", gameTimeInSeconds);
        PlayerPrefs.SetInt("GameTimeInMinutes", gameTimeInMinutes);
        PlayerPrefs.SetFloat("GameTimeDay", timeDay);
        PlayerPrefs.SetInt("DayCounter", dayCounter);
        PlayerPrefs.SetInt("CurrentSeason", (int)currentSeason);
    }

    public void LoadTime()
    {
        if (PlayerPrefs.HasKey("TimePlus") && PlayerPrefs.HasKey("GameTimeInSeconds") &&
        PlayerPrefs.HasKey("GameTimeInMinutes") &&
        PlayerPrefs.HasKey("GameTimeDay") &&
        PlayerPrefs.HasKey("DayCounter") &&
        PlayerPrefs.HasKey("CurrentSeason"))
        {
            timePlus = PlayerPrefs.GetFloat("TimePlus");
            gameTimeInSeconds = PlayerPrefs.GetInt("GameTimeInSeconds");
            gameTimeInMinutes = PlayerPrefs.GetInt("GameTimeInMinutes");
            timeDay = PlayerPrefs.GetFloat("GameTimeDay");
            dayCounter = PlayerPrefs.GetInt("DayCounter");
            currentSeason = (Season)PlayerPrefs.GetInt("CurrentSeason");
        }
        else
        {
            // 处理PlayerPrefs值未设置或无效的情况。
            // 可以设置默认值或采取适当的措施。
            Debug.LogWarning("PlayerPrefs值丢失或无效。加载默认值。");
            SetDefaultValues(); // 创建一个方法来设置默认值。
        }
    }
    private void SetDefaultValues()
    {
        timePlus = 0f;
        gameTimeInSeconds = 0;
        gameTimeInMinutes = 0;
        timeDay = 0f;
        dayCounter = 0;
        currentSeason = Season.Spring;
    }

}
