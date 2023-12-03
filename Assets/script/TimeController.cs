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
 * handles lighting changes, and manages season transitions.�ýű�������Ϸ�е�ʱ�����ҹѭ����������ʱ������ţ�������ձ仯���������ڹ��ɡ�
 * 
 * Public Properties:
 * - uhrZeit: The UI text element displaying the in-game time.��ʾ��Ϸʱ��� UI �ı�Ԫ�ء�
 * - dayLight: The main directional light representing the sun.����̫����������⡣
 * - nightLight: The secondary light representing moonlight.����
 * - rotationSpeed: The rotation speed of the day-night cycle.��ҹѭ������ת�ٶȡ�
 * - currentSeason: The current season in the game.��ǰ�ļ��ڡ�
 * 
 * Methods:
 * - UpdateTimer: Updates the in-game timer and manages time-related game events.������Ϸ��ʱ����������ʱ����ص���Ϸ�¼���
 * - ChangeDayAndNight: Handles logic for changes that occur after the transition between day and night.
 * - DayAndNightRotation: Controls the rotation of the sun and moon during the day-night cycle.������ҹѭ���ڼ�̫������������ת��
 * - ChangeSeason: Changes the current season in the game.
 * - SaveTime: Saves the current game time and related data using PlayerPrefs.���浱ǰ��Ϸʱ���������ݡ�
 * - LoadTime: Loads the saved game time and related data from PlayerPrefs.���ر������Ϸʱ���������ݡ�
 * - SetDefaultValues: Sets default values for variables in case PlayerPrefs values are missing or invalid.�� PlayerPrefs ��ֵ��ʧ����Ч�������Ϊ��������Ĭ��ֵ��
 * 
 * Author: [FannnHAHA]
 * Date: []
 */


public class TimeController : MonoBehaviour
{
    //�ϱ��������
    public enum DayTime { Day, Night }
    public Light daylLight; 
    public Light nachtLicht;
    public float rotationSpeed = 1f; // ��ת�ٶȣ�������ֵ��������ת�ٶ�
    public static DayTime currentDayTime;

    //�����Ǽ�ʱ��
    public Text uhrZeit;
    private int gameTimeInMinutes = 0;
    private int gameTimeInSeconds = 0;
    private float timePlus = 0;
    private float timeDay = 0;
    private int dayCounter;
    
    //�ļ�����seson
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

    //����ÿ��ı仯
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
            //����ҹ����ҹ��
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
    //��ҹ�ı�֮��ᷢʲô
    private void ChangeDayandNight()
    {
       // throw new NotImplementedException();
    }
    //���������ҹ��ת
    private void DayandNightdrehen()
    {
        float t = timeDay / 180.0f;
        float sunRotation = Mathf.Lerp(-90, 270, t);
        float moonRotation = sunRotation - 180;
        daylLight.transform.rotation = Quaternion.Euler(sunRotation, 0, 0);
        nachtLicht.transform.rotation = Quaternion.Euler(moonRotation, 0, 0);
    }


    //�ļ��仯�����ǻ�û����
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
            // ����PlayerPrefsֵδ���û���Ч�������
            // ��������Ĭ��ֵ���ȡ�ʵ��Ĵ�ʩ��
            Debug.LogWarning("PlayerPrefsֵ��ʧ����Ч������Ĭ��ֵ��");
            SetDefaultValues(); // ����һ������������Ĭ��ֵ��
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
