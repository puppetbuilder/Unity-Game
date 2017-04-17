using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicWeather : MonoBehaviour {

    public WeatherStates _weatherState;                     //Definesthe naming convention of our weather states


    public enum WeatherStates {                             //Defines all staes the weather can be
        PickWeather,
        SunnyWeather,
        ThunderWeather,
        MistWeather,
        OvercastWeather,
        SnowWeather
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator WeatherFSM() {
        while (true) {                                      //while weather state machine is active
            switch (_weatherState) {                        //switch the weather states
                case WeatherStates.PickWeather:
                    PickWeather();
                    break;
                case WeatherStates.SunnyWeather:
                    SunnyWeather();
                    break;
                case WeatherStates.ThunderWeather:
                    ThunderWeather();
                    break;
                case WeatherStates.MistWeather:
                    MistWeather();
                    break;
                case WeatherStates.OvercastWeather:
                    OvercastWeather();
                    break;
                case WeatherStates.SnowWeather:
                    SnowWeather();
                    break;

            }
            yield return null;
        }
    }


    void PickWeather(){

    }

    void SunnyWeather(){

    }

    void ThunderWeather(){

    }

    void MistWeather(){

    }

    void OvercastWeather(){

    }

    void SnowWeather(){

    }
 

}
