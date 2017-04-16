using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2NightCycle : MonoBehaviour {

    public int _days;                               // Defines naming convension for the days
    public int _hours;                              // Defines naming convension for the hours
    public int _minutes;                            // Defines naming convension for the minutes
    public int _seconds;                            // Defines naming convension for the seconds
    public float _counter;                          // Defines naming convension for the counter

    public int _year;                               //Defines naming convension for year counter
    public int _leapYearsCounter;                   //Defines naming convension for leapyears counter
    public int _calendarDays;                       //Defines naming convension for days of the month

    public bool _january;                            //Defines if we are in the month of January
    public bool _february;                           //Defines if we are in the month of February
    public bool _march;                              //Defines if we are in the month of March
    public bool _april;                              //Defines if we are in the month of April
    public bool _may;                                //Defines if we are in the month of May
    public bool _june;                               //Defines if we are in the month of June
    public bool _july;                               //Defines if we are in the month of July
    public bool _august;                             //Defines if we are in the month of August
    public bool _september;                          //Defines if we are in the month of September
    public bool _october;                            //Defines if we are in the month of October
    public bool _november;                           //Defines if we are in the month of November
    public bool _december;                           //Defines if we are in the month of December

    public bool _spring;                            //Defines if we are in the season of spring
    public bool _summer;                            //Defines if we are in the season of summer
    public bool _autumn;                            //Defines if we are in the season of autumn
    public bool _winter;                            //Defines if we are in the season of winter

    // based on 24 hour clock
    public int _dawnStartTime;                      // Defines dawn start
    public int _dayStartTime;                       // Defines day start
    public int _duskStartTime;                      // Defines dusk start
    public int _nightStartTime;                     // Defines night start

    public int _dawnSpringStartTime = 6;            // Defines Spring dawn start
    public int _daySpringStartTime = 8;             // Defines Spring day start
    public int _duskSpringStartTime = 18;           // Defines Spring dusk start
    public int _nightSpringStartTime = 20;          // Defines Spring night start

    public int _dawnSummerStartTime = 6;            // Defines Summer dawn start
    public int _daySummerStartTime = 8;             // Defines Summer day start
    public int _duskSummerStartTime = 18;           // Defines Summer dusk start
    public int _nightSummerStartTime = 20;          // Defines Summer night start

    public int _dawnAutumnStartTime = 6;            // Defines Autumn dawn start
    public int _dayAutumnStartTime = 8;             // Defines Autumn day start
    public int _duskAutumnStartTime = 18;           // Defines Autumn dusk start
    public int _nightAutumnStartTime = 20;          // Defines Autumn night start

    public int _dawnWinterStartTime = 6;            // Defines Winter dawn start
    public int _dayWinterStartTime = 8;             // Defines Winter day start
    public int _duskWinterStartTime = 18;           // Defines Winter dusk start
    public int _nightWinterStartTime = 20;          // Defines Winter night start

    public float _sunDimTime = 0.01f;               // speed at which sun dims
    public float _dawnSunIntensity = 0.5f;          // dawn sun strenght
    public float _daySunIntensity = 1f;             // day sun strenght
    public float _duskSunIntensity = 0.25f;         // dawn sun strenght
    public float _nightSunIntensity = 0f;           // night sun strenght


    public float _ambientDimTime = 0.0001f;         //Speed at whih ambient light is adjusted
    public float _dawnAmbientIntensity = 0.5f;      //dawn ambient strenght
    public float _dayAmbientIntensity = 1f;         //day ambient strenght
    public float _duskAmbientIntensity = 0.25f;     //dusk amient strenght
    public float _nightAmbientIntensity = 0;        //night amient strenght

    public float _dawnSkyboxBlendFactor = 0.5f;     //defines dawn skybox blend factor
    public float _daySkyboxBlendFactor = 1f;        //defines day skybox blend factor
    public float _duskSkyboxBlendFactor = 0.25f;    //defines dusk skybox blend factor
    public float _nightSkyboxBlendFactor = 0f;      //defines night skybox blend factor

    public float _skyboxBlendFactor;                //defines the current skybox blend value
    public float _skyboxBlendSpeed = 0.01f;         //defines speed at which skybox will blend

    public int _guiWidth = 100;                     //defines GUI Label width
    public int _guiHeight = 20;                     //defines GUI Label height

    public DayPhases _dayPhases;                    // Defines naming convension for phases of day

    public enum DayPhases {                         // Enums for Day Phases
        Dawn,
        Day,
        Dusk,
        Night
    }

    void Awake() {
        _dayPhases = DayPhases.Night;                               //Set day phase to night on start up
        RenderSettings.ambientIntensity = _nightAmbientIntensity;   //render settings ambient intensity is equal to night on start up

        GetComponent<Light>().intensity = _nightSunIntensity;       //Set intensity to night at awake
    }

	// Use this for initialization
	void Start () {
        StartCoroutine("TimeOfDayFiniteStateMachine");      //Start TimeOfDayFiniteStateMachine on start up

        

        _hours = 5;                                         //hours equal 5 at start up
        _minutes = 59;                                      //minutes equal 59 at start up
        _counter = 59;                                      //counter equals 59 at start up

        _days = 1;                                          //days equal 1 on start up

        _calendarDays = 1;                                  //calendar days equals 1 at start up
        _april = true;                                      //start in the month of April
        _spring = true;                                     //Start in the season of spring on start up

        _dawnStartTime = _dawnSpringStartTime;              //Dawn Start time is equal to Spring
        _dayStartTime = _daySpringStartTime;                //Day Start time is equal to Spring
        _duskStartTime = _duskSpringStartTime;              //Dusk Start time is equal to Spring
        _nightStartTime = _nightSpringStartTime;            //Night Start time is equal to Spring


        _year = 2017;                                       //year equal 2017 at start up
        _leapYearsCounter = 1;                              //leap year counter equals 4 at start up

    }
	
	// Update is called once per frame
	void Update () {
        SecondsCounter();                                   //Call SecondsCounter function
        UpdateSkybox();                                     //Call UpdateSkybox function
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

        _days++;                                            //Increase days counter

        UpdateCalendarDays();                               //call UpdateCalendarDays function

    }

    void UpdateCalendarDays() {
        Debug.Log("UpdateCalendarDays");

        _calendarDays++;                                    //increase calendar days

        UpdateCalendarMonth();                              // call UpdateCalendarMonth function

    }

    void UpdateCalendarMonth() {
        Debug.Log("UpdateCalendarMonth");

        if (_january == true && _calendarDays > 31){        //if we are in january and calendar days is greater than 31
            _january = false;                               //set _january to false
            _february = true;                               //set _february to true
            _calendarDays = 1;                              //and make calendar days equal to 1(first of the new month of february
        }

        if (_leapYearsCounter == 4 &&                       //if leap year counter is true
            _february == true && _calendarDays > 29){       //and we are in february and calendar days is greater than 29
            _february = false;                              //set _february to false
            _march = true;                                  //set _march to true
            _calendarDays = 1;                              //and make calendar days equal to 1(first of the new month of february

            SeasonsManager();                               //call SeasonManager function
        }

        if (_leapYearsCounter < 4 &&                        //if leap year counter is less than 4
           _february == true && _calendarDays > 28){        //and we are in february and calendar days is greater than 28
            _february = false;                              //set _february to false
            _march = true;                                  //set _march to true
            _calendarDays = 1;                              //and make calendar days equal to 1(first of the new month of february

            SeasonsManager();                               //call SeasonManager function
        }

        if (_march == true && _calendarDays > 31){          //if we are in march and calendar days is greater than 31
            _march = false;                                 //set _march to false
            _april = true;                                  //set _april to true
            _calendarDays = 1;                              //and make calendar days equal to 1(first of the new month of february
        }

        if (_april == true && _calendarDays > 30){          //if we are in april and calendar days is greater than 30
            _april = false;                                 //set _april to false
            _may = true;                                    //set _may to true
            _calendarDays = 1;                              //and make calendar days equal to 1(first of the new month of february
        }

        if (_may == true && _calendarDays > 31){            //if we are in may and calendar days is greater than 31
            _may = false;                                   //set _may to false
            _june = true;                                   //set _june to true
            _calendarDays = 1;                              //and make calendar days equal to 1(first of the new month of february

            SeasonsManager();                               //call SeasonManager function

        }

        if (_june == true && _calendarDays > 30){           //if we are in june and calendar days is greater than 30
            _june = false;                                  //set _june to false
            _july = true;                                   //set _july to true
            _calendarDays = 1;                              //and make calendar days equal to 1(first of the new month of february
        }

        if (_july == true && _calendarDays > 31){           //if we are in july and calendar days is greater than 31
            _july = false;                                  //set _july to false
            _august = true;                                 //set _august to true
            _calendarDays = 1;                              //and make calendar days equal to 1(first of the new month of february
        }

        if (_august == true && _calendarDays > 31){         //if we are in august and calendar days is greater than 31
            _august = false;                                //set _august to false
            _september = true;                              //set _september to true
            _calendarDays = 1;                              //and make calendar days equal to 1(first of the new month of february
        }

        if (_september == true && _calendarDays > 30){      //if we are in september and calendar days is greater than 30
            _september = false;                             //set _september to false
            _october = true;                                //set _october to true
            _calendarDays = 1;                              //and make calendar days equal to 1(first of the new month of february

            SeasonsManager();                               //call SeasonManager function

        }

        if (_october == true && _calendarDays > 31){        //if we are in october and calendar days is greater than 31
            _october = false;                               //set _september to false
            _november = true;                               //set _october to true
            _calendarDays = 1;                              //and make calendar days equal to 1(first of the new month of february
        }

        if (_november == true && _calendarDays > 30){       //if we are in november and calendar days is greater than 30
            _november = false;                              //set _november to false
            _december = true;                               //set _december to true
            _calendarDays = 1;                              //and make calendar days equal to 1(first of the new month of february

            SeasonsManager();                               //call SeasonManager function
        }

        if (_december == true && _calendarDays > 31){       //if we are in december and calendar days is greater than 31
            _december = false;                              //set _november to false
            _january = true;                                //set _january to true
            _calendarDays = 1;                              //and make calendar days equal to 1(first of the new month of february

            YearCounter();                                  //Call YearCounter function
        }


    }

    void YearCounter()
    {
        Debug.Log("YearCounter");

        _year++;                                            //increase year

        _leapYearsCounter++;                                //increase leap year

        if (_leapYearsCounter > 4)                          //if leap yar counter greater than 4
            _leapYearsCounter = 1;                          //then make leap year counter equal to 1
    }

    void SeasonsManager()
    {
        Debug.Log("SeasonsManager");

        _spring = false;                                    //set spring to be equal to false
        _summer = false;                                    //set summer to be equal to false
        _autumn = false;                                    //set autumn to be equal to false
        _winter = false;                                    //set winter to be equal to false

        if (_march == true && _calendarDays == 1){          //if we are in march and calendar days equal 1
            _spring = true;                                 //then set spring to true

            _dawnStartTime = _dawnSpringStartTime;          //Dawn Start time is equal to Spring
            _dayStartTime = _daySpringStartTime;            //Day Start time is equal to Spring
            _duskStartTime = _duskSpringStartTime;          //Dusk Start time is equal to Spring
            _nightStartTime = _nightSpringStartTime;        //Night Start time is equal to Spring

        }

        if (_june == true && _calendarDays == 1){           //if we are in june and calendar days equal 1
            _summer = true;                                 //then set summer to true

            _dawnStartTime = _dawnSummerStartTime;          //Dawn Start time is equal to Summer
            _dayStartTime = _daySummerStartTime;            //Day Start time is equal to Summer
            _duskStartTime = _duskSummerStartTime;          //Dusk Start time is equal to Summer
            _nightStartTime = _nightSummerStartTime;        //Night Start time is equal to Summer

        }

        if (_september == true && _calendarDays == 1){      //if we are in september and calendar days equal 1
            _autumn = true;                                 //then set autumn to true

            _dawnStartTime = _dawnAutumnStartTime;          //Dawn Start time is equal to Autumn
            _dayStartTime = _dayAutumnStartTime;            //Day Start time is equal to Autumn
            _duskStartTime = _duskAutumnStartTime;          //Dusk Start time is equal to Autumn
            _nightStartTime = _nightAutumnStartTime;        //Night Start time is equal to Autumn

        }

        if (_december == true && _calendarDays == 1){       //if we are in december and calendar days equal 1
            _winter = true;                                 //then set spring to true

            _dawnStartTime = _dawnWinterStartTime;          //Dawn Start time is equal to Winter
            _dayStartTime = _dayWinterStartTime;            //Day Start time is equal to Winter
            _duskStartTime = _duskWinterStartTime;          //Dusk Start time is equal to Winter
            _nightStartTime = _nightWinterStartTime;        //Night Start time is equal to Winter

        }
    }


    void Dawn() {
        Debug.Log("Dawn");

        DawnSunLightManager();                                                  //Call DawnSunLightManager function

        DawnAmbientLightManager();                                              //call DawnAmbientLightManager funcation 

        if (_hours == _dayStartTime && _hours < _duskStartTime){                //if hours equal day start time and hours less than dusk strt timer
            _dayPhases = DayPhases.Day;                                         //set day phase to day
        }
    }

    void DawnSunLightManager() {
        Debug.Log("DawnSunLightManager");

        if (GetComponent<Light>().intensity == _dawnSunIntensity)               //if light intensity is equals to dawn intensity
            return;                                                             //then do nothing and return

        if (GetComponent<Light>().intensity < _dawnSunIntensity)                //if sun intensity is less than dawn
            GetComponent<Light>().intensity += _sunDimTime * Time.deltaTime;    //increase the intensity by sun dim time

        if (GetComponent<Light>().intensity > _dawnSunIntensity)                //if intensity is greater than dawn
            GetComponent<Light>().intensity = _dawnSunIntensity;                //then make intensity equal to dawn
    }

    void DawnAmbientLightManager() {
        Debug.Log("DawnAmbientLightManager");

        if (RenderSettings.ambientIntensity == _dawnAmbientIntensity)           //if ambient intensity is equals to dawn ambient intensity
            return;                                                             //then do nothing and return

        if (RenderSettings.ambientIntensity < _dawnAmbientIntensity)            //if ambient intensity is less than dawn
            RenderSettings.ambientIntensity += _ambientDimTime * Time.deltaTime;//increase the intensity by ambient dim time

        if (RenderSettings.ambientIntensity > _dawnAmbientIntensity)            //if ambient intensity is greater than dawn
            RenderSettings.ambientIntensity = _dawnAmbientIntensity;            //then make ambient intensity equal to dawn
    }

    void Day() {
        Debug.Log("Day");

        DaySunLightManager();                                                  //Call DaySunLightManager function

        DayAmbientLightManager();                                              //call DayAmbientLightManager funcation 

        if (_hours == _duskStartTime && _hours < _nightStartTime){              //if hours equal day start time and hours less than dusk strt timer
            _dayPhases = DayPhases.Dusk;                                        //set day phase to day
        }

    }

    void DaySunLightManager()
    {
        Debug.Log("DaySunLightManager");

        if (GetComponent<Light>().intensity == _daySunIntensity)               //if light intensity is equals to day intensity
            return;                                                             //then do nothing and return

        if (GetComponent<Light>().intensity < _daySunIntensity)                //if sun intensity is less than day
            GetComponent<Light>().intensity += _sunDimTime * Time.deltaTime;    //increase the intensity by sun dim time

        if (GetComponent<Light>().intensity > _daySunIntensity)                //if intensity is greater than day
            GetComponent<Light>().intensity = _daySunIntensity;                //then make intensity equal to day
    }

    void DayAmbientLightManager()
    {
        Debug.Log("DayAmbientLightManager");

        if (RenderSettings.ambientIntensity == _dayAmbientIntensity)           //if ambient intensity is equals to day ambient intensity
            return;                                                             //then do nothing and return

        if (RenderSettings.ambientIntensity < _dayAmbientIntensity)            //if ambient intensity is less than dawn
            RenderSettings.ambientIntensity += _ambientDimTime * Time.deltaTime;//increase the intensity by ambient dim time

        if (RenderSettings.ambientIntensity > _dayAmbientIntensity)            //if ambient intensity is greater than day
            RenderSettings.ambientIntensity = _dayAmbientIntensity;            //then make ambient intensity equal to day
    }

    void Dusk() {
        Debug.Log("Dusk");

        DuskSunLightManager();                                                  //Call DuskSunLightManager function

        DuskAmbientLightManager();                                              //call DuskAmbientLightManager funcation 

        if (_hours == _nightStartTime) {                                        //if hours equal night  start night
            _dayPhases = DayPhases.Night;                                       //set day phase to night
        }

    }

    void DuskSunLightManager()
    {
        Debug.Log("DuskSunLightManager");

        if (GetComponent<Light>().intensity == _duskSunIntensity)               //if light intensity is equals to dusk intensity
            return;                                                             //then do nothing and return

        if (GetComponent<Light>().intensity < _duskSunIntensity)                //if sun intensity is less than dusk
            GetComponent<Light>().intensity += _sunDimTime * Time.deltaTime;    //increase the intensity by sun dim time

        if (GetComponent<Light>().intensity > _duskSunIntensity)                //if intensity is greater than dusk
            GetComponent<Light>().intensity = _duskSunIntensity;                //then make intensity equal to dusk
    }

    void DuskAmbientLightManager()
    {
        Debug.Log("DuskAmbientLightManager");

        if (RenderSettings.ambientIntensity == _duskAmbientIntensity)           //if ambient intensity is equals to dusk ambient intensity
            return;                                                             //then do nothing and return

        if (RenderSettings.ambientIntensity < _duskAmbientIntensity)            //if ambient intensity is less than dusk
            RenderSettings.ambientIntensity += _ambientDimTime * Time.deltaTime;//increase the intensity by ambient dim time

        if (RenderSettings.ambientIntensity > _duskAmbientIntensity)            //if ambient intensity is greater than dusk
            RenderSettings.ambientIntensity = _duskAmbientIntensity;            //then make ambient intensity equal to dusk
    }

    void Night() {
        Debug.Log("Night");

        NightSunLightManager();                                                 //Call NightSunLightManager function

        NightAmbientLightManager();                                             //call NightAmbientLightManager funcation 

        if (_hours == _dawnStartTime && _hours < _dayStartTime){                //if hours equal dawn start time and hours less than day strt timer
            _dayPhases = DayPhases.Dawn;                                        //set day phase to dawn
        }
    }

    void NightSunLightManager()
    {
            Debug.Log("NightSunLightManager");

            if (GetComponent<Light>().intensity == _nightSunIntensity)               //if light intensity is equals to night intensity
                return;                                                             //then do nothing and return

            if (GetComponent<Light>().intensity < _nightSunIntensity)                //if sun intensity is less than night
                GetComponent<Light>().intensity += _sunDimTime * Time.deltaTime;    //increase the intensity by sun dim time

            if (GetComponent<Light>().intensity > _nightSunIntensity)                //if intensity is greater than night
                GetComponent<Light>().intensity = _nightSunIntensity;                //then make intensity equal to night
        }

    void NightAmbientLightManager()
    {
            Debug.Log("NightAmbientLightManager");

            if (RenderSettings.ambientIntensity == _nightAmbientIntensity)           //if ambient intensity is equals to night ambient intensity
                return;                                                             //then do nothing and return

            if (RenderSettings.ambientIntensity < _nightAmbientIntensity)            //if ambient intensity is less than dusk
                RenderSettings.ambientIntensity += _ambientDimTime * Time.deltaTime;//increase the intensity by ambient dim time

            if (RenderSettings.ambientIntensity > _nightAmbientIntensity)            //if ambient intensity is greater than dusk
                RenderSettings.ambientIntensity = _nightAmbientIntensity;            //then make ambient intensity equal to dusk
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

    private void UpdateSkybox() {
        Debug.Log("Update Skybox");

        if (_dayPhases == DayPhases.Dawn){                              //if day phase is equal to dawn

            if (_skyboxBlendFactor == _dawnSkyboxBlendFactor)           //if skybox blend equals dawn
                return;                                                 //then do nothing and return

            _skyboxBlendFactor += _skyboxBlendSpeed * Time.deltaTime;   //increase skybox blend by blend speed

            if (_skyboxBlendFactor > _dawnSkyboxBlendFactor)            //if skybox blend factor is greater than dawn
                _skyboxBlendFactor = _dawnSkyboxBlendFactor;            //then make skybox blend factor equal to dawn
        }

        if (_dayPhases == DayPhases.Day){                               //if day phase is equal to day

            if (_skyboxBlendFactor == _daySkyboxBlendFactor)            //if skybox blend equals day
                return;                                                 //then do nothing and return

            _skyboxBlendFactor += _skyboxBlendSpeed * Time.deltaTime;   //increase skybox blend by blend speed

            if (_skyboxBlendFactor > _daySkyboxBlendFactor)             //if skybox blend factor is greater than day
                _skyboxBlendFactor = _daySkyboxBlendFactor;             //then make skybox blend factor equal to day
        }

        if (_dayPhases == DayPhases.Dusk){                              //if day phase is equal to dusk

            if (_skyboxBlendFactor == _duskSkyboxBlendFactor)           //if skybox blend equals dusk
                return;                                                 //then do nothing and return

            _skyboxBlendFactor -= _skyboxBlendSpeed * Time.deltaTime;   //decrease skybox blend by blend speed

            if (_skyboxBlendFactor < _duskSkyboxBlendFactor)            //if skybox blend factor is less than dusk
                _skyboxBlendFactor = _duskSkyboxBlendFactor;            //then make skybox blend factor equal to dusk
        }

        if (_dayPhases == DayPhases.Night){                             //if day phase is equal to night

            if (_skyboxBlendFactor == _nightSkyboxBlendFactor)          //if skybox blend equals night
                return;                                                 //then do nothing and return

            _skyboxBlendFactor -= _skyboxBlendSpeed * Time.deltaTime;   //decrease skybox blend by blend speed

            if (_skyboxBlendFactor < _nightSkyboxBlendFactor)           //if skybox blend factor is less than night
                _skyboxBlendFactor = _nightSkyboxBlendFactor;           //then make skybox blend factor equal to night
        }

        RenderSettings.skybox.SetFloat("_Blend",_skyboxBlendFactor);    //Get render for skibox and set float for the blend                    

    }

}
