using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2NightCycle : MonoBehaviour {

    public int _days;                           // Defines naming convension for the days
    public int _hours;                          // Defines naming convension for the hours
    public int _minutes;                        // Defines naming convension for the minutes
    public int _seconds;                        // Defines naming convension for the seconds
    public float _counter;                      // Defines naming convension for the counter

    // based on 24 hour clock
    public int _dawnStartTime = 6;              // Defines dawn start
    public int _dayStartTime = 8;               // Defines day start
    public int _duskStartTime = 18;            // Defines dusk start
    public int _nightStartTime = 20;            // Defines night start

    public float _sunDimTime = 0.01f;           // speed at which sun dims
    public float _dawnSunIntensity = 0.5f;      // dawn sun strenght
    public float _daySunIntensity = 1f;         // day sun strenght
    public float _duskSunIntensity = 0.25f;    // dawn sun strenght
    public float _nightSunIntensity = 0f;       // night sun strenght

    public int _guiWidth = 100;                 //defines GUI Label width
    public int _guiHeight = 20;                 //defines GUI Label height

    public DayPhases _dayPhases;            // Defines naming convension for phases of day

    public enum DayPhases {                 // Enums for Day Phases
        Dawn,
        Day,
        Dusk,
        Night
    }

    void Awake() {
        _dayPhases = DayPhases.Night;                           //Set day phase to night on start up

        GetComponent<Light>().intensity = _nightSunIntensity;   //Set intensity to night at awake
    }

	// Use this for initialization
	void Start () {
        StartCoroutine("TimeOfDayFiniteStateMachine");      //Start TimeOfDayFiniteStateMachine on start up

        

        _hours = 5;                                         //hours equal 5 at start up
        _minutes = 59;                                      //minutes equal 59 at start up
        _counter = 59;                                      //counter equals 59 at start up

        _days = 1;                                          //days equal 1 on start up
    }
	
	// Update is called once per frame
	void Update () {
        SecondsCounter();                                   // Start SecondsCounter function
    }

    IEnumerator TimeOfDayFiniteStateMachine() {
        while (true) {
            switch (_dayPhases) {
                case DayPhases.Dawn:
                    Dawn();
                    break;
                case DayPhases.Day:
                    Day();
                    break;
                case DayPhases.Dusk:
                    Dusk();
                    break;
                case DayPhases.Night:
                    Night();
                    break;
            }
            yield return null;
        }
    }

    void SecondsCounter()
    {
        Debug.Log("SecondsCounter");

        if (_counter == 60)                                 //if the counter = 60
            _counter = 0;                                   //then make counter equal to 0

        _counter += Time.deltaTime;                         //counter = time sync to PC speed

        _seconds = (int)_counter;                           //Secounds equals counter cast to int

        if (_counter < 60)                                  //if counter is less than 60
            return;                                         //then to nothing and return

        if (_counter > 60)                                  //if counter is greater than 60
            _counter = 60;                                  //then make counter equal to 60

        if (_counter == 60)                                 //if counter is equal to 60
            MinutesCounter();                               //Call MinutesCounter function
    }

    void MinutesCounter()
    {
        Debug.Log("MinutesCounter");

        _minutes ++;                                        //Increase minutes counter

        if (_minutes == 60) {                               //if minutes counter equal 60
            HoursCounter();                                 //call hours counter function
            _minutes = 0;                                   //and then make minutes equal 0
        }


    }

    void HoursCounter()
    {
        Debug.Log("HoursCounter");

        _hours++;                                           //Increase hours counter

        if (_hours == 24)
        {                                                   //if hours counter equal 24
            DaysCounter();                                   //call hours counter function
            _hours = 0;                                     //and then make minutes equal 0
        }
    }

    void DaysCounter()
    {
        Debug.Log("DaysCounter");

        _days++;                                           //Increase days counter

    }

    void Dawn() {
        Debug.Log("Dawn");

        if (GetComponent<Light>().intensity < _dawnSunIntensity)                //if sun intensity is less than dawn
            GetComponent<Light>().intensity += _sunDimTime * Time.deltaTime;   //increase the intensity by sun dim time

        if (GetComponent<Light>().intensity > _dawnSunIntensity)                //if intensity is greater than dawn
            GetComponent<Light>().intensity = _dawnSunIntensity;                //then make intensity equal to dawn

        if(_hours == _dayStartTime && _hours < _duskStartTime){                 //if hours equal day start time and hours less than dusk strt timer
            _dayPhases = DayPhases.Day;                                         //set day phase to day
        }
    }

    void Day() {
        Debug.Log("Day");

        if (GetComponent<Light>().intensity < _daySunIntensity)                 //if sun intensity is less than day
            GetComponent<Light>().intensity += _sunDimTime * Time.deltaTime;   //increase the intensity by sun dim time

        if (GetComponent<Light>().intensity > _daySunIntensity)                 //if intensity is greater than day
            GetComponent<Light>().intensity = _daySunIntensity;                 //then make intensity equal to day

        if (_hours == _duskStartTime && _hours < _nightStartTime){              //if hours equal day start time and hours less than dusk strt timer
            _dayPhases = DayPhases.Dusk;                                        //set day phase to day
        }

    }

    void Dusk() {
        Debug.Log("Dusk");

        if (GetComponent<Light>().intensity > _duskSunIntensity)                //if sun intensity is greater than dusk
            GetComponent<Light>().intensity -= _sunDimTime * Time.deltaTime;   //then decrease the intensity by sun dim time

        if (GetComponent<Light>().intensity < _duskSunIntensity)                //if intensity is less than dusk
            GetComponent<Light>().intensity = _duskSunIntensity;                //then make intensity equal to dusk

        if (_hours == _nightStartTime) {                                        //if hours equal night  start night
            _dayPhases = DayPhases.Night;                                       //set day phase to night
        }

    }

    void Night() {
        Debug.Log("Night");

        if (GetComponent<Light>().intensity > _nightSunIntensity)               //if sun intensity is greater than night
            GetComponent<Light>().intensity -= _sunDimTime * Time.deltaTime;    //then decrease the intensity by sun dim time

        if (GetComponent<Light>().intensity < _nightSunIntensity)               //if intensity is less than night
            GetComponent<Light>().intensity = _nightSunIntensity;               //then make intensity equal to night

        if (_hours == _dawnStartTime && _hours < _dayStartTime){                //if hours equal dawn start time and hours less than day strt timer
            _dayPhases = DayPhases.Dawn;                                        //set day phase to dawn
        }


    }

    void OnGUI() {
        //Create GUI Label to display number of days
        GUI.Label(new Rect(Screen.width - 50, 5, _guiWidth, _guiHeight), "Day " + _days);

        //if minutes is less than 10 display extra zero
        if (_minutes < 10)
        {
            GUI.Label(new Rect(Screen.width - 50, 25, _guiWidth, _guiHeight), _hours + ":0" + _minutes + ":" + _seconds);
        }

        //else just display clock
        else
            GUI.Label(new Rect(Screen.width - 50, 25, _guiWidth, _guiHeight), _hours + ":" + _minutes + ":" + _seconds);

    }

}
