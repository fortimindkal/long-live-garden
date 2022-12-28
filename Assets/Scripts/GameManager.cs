using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] _plant;

    public Text _grandmaText;
    public Text _daysText;
    public Text _trashText;
    public Text _timeText;
    public Text _loseGrandmaText;
    public Text _plantDurationText;
    public GameObject _pauseMenu;
    public GameObject _loseMenu;
    public GameObject _quitMenu;
    public GameObject _backgroundObject;

    public Sprite _defaultBackgroundSprite;
    public Sprite _nightBackgroundSprite;

    [SerializeField]
    private int _score;
    [SerializeField]
    private int _scoreObjective;
    [SerializeField]
    private int _trash;
    [SerializeField]
    private int _trashMax;

    [SerializeField]
    private int _days;
    [SerializeField]
    private int _hours;
    [SerializeField]
    private int _hoursPlay;
    [SerializeField]
    private int _defaultHours;
    [SerializeField]
    private int _hoursRemain;
    [SerializeField]    
    private float _timers;

    public Plant[] allPlants;

    void Start()
    {
        allPlants = Resources.LoadAll<Plant>("Plants");
        LoadPlants();
        Time.timeScale = 1;
        _score = 0;
        if(!PlayerPrefs.HasKey("days"))
        {
            _days = 1;
            _hours = 10;
            _hoursPlay = 0;
            _defaultHours = _hours;
            _hoursRemain = 24;
            //_timers = 43f;
            _timers = 2.5f;
        }
        else
        {
            _days = PlayerPrefs.GetInt("days", _days);
            _hours = PlayerPrefs.GetInt("hours", _hours);
            _hoursRemain = PlayerPrefs.GetInt("hoursremain", _hoursRemain);
            _defaultHours = 10;
            _timers = PlayerPrefs.GetFloat("timers", _timers);
        }
        _trash = 0;
        _daysText.text =  string.Format("Days : {0} Day - {1} Hours", _days, _hoursPlay);
        _grandmaText.text = string.Format("Give to Grandma : {0}/{1}", _score, _scoreObjective);
        _trashText.text = string.Format("Plant in Trash : {0}/{1}", _trash, _trashMax);
        _timeText.text = string.Format("{0:00}:00", _hours);
        _pauseMenu.SetActive(false);
    }

    void Update()
    {
        _daysText.text = string.Format("Days : {0} Day - {1} Hours", _days, _hoursPlay);
        _grandmaText.text = string.Format("Give to Grandma : {0}/{1}", _score, _scoreObjective);
        _trashText.text = string.Format("Plant in Trash : {0}/{1}", _trash, _trashMax);
        _timeText.text = string.Format("{0:00}:00", _hours);

        if (_timers > 0)
        {
            _timers -= 1f * Time.deltaTime;
        }
        else
        {
            //_timers = 43f;
            _timers = 2.5f;
            _hours++;
            _hoursPlay++;
            _hoursRemain--;
        }

        if(_hours == _defaultHours)
        {
            if(_hoursRemain < 1)
            {
                _hoursRemain = 24;
                _days++;
                for(int i = 0; i < _plant.Length; i++)
                { 
                    if(_plant[i].GetComponent<PlantHealth>().plantWater > 0)
                    {
                        _plant[i].GetComponent<PlantHealth>().plantOld++;
                        _plant[i].GetComponent<PlantHealth>().fertilizerAmount = 0;
                    }
                }
            }
        }

        if(_hours == 18)
        {
            _backgroundObject.GetComponent<Image>().sprite = _nightBackgroundSprite;
        }

        if (_hours == 6)
        {
            _backgroundObject.GetComponent<Image>().sprite = _defaultBackgroundSprite;
        }

        if (_hours > 23)
        {
            _hours = 0;
        }

        if (_hoursPlay > 23)
        {
            _hoursPlay = 0;
        }

        if(_trash == _trashMax)
        {
            _trash = -1;
            LoseMenu();
        }
    }

    public void IncreaseScore(int score)
    {
        _score += score;
    }

    public void IncreaseTrash(int trash)
    {
        _trash += trash;
    }

    public void PauseMenu()
    {
        FindObjectOfType<AudioManager>().Play("UI Pop Up");
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void QuitMenu()
    {
        FindObjectOfType<AudioManager>().Play("UI Pop Up");
        _quitMenu.SetActive(true);
    }

    public void ConfirmQuitMenu()
    {
        FindObjectOfType<AudioManager>().Play("UI Clicks");
        SceneManager.LoadScene("MainMenu");
        SavePlants();
    }

    public void CancelQuitMenu()
    {
        FindObjectOfType<AudioManager>().Play("UI Clicks");
        _quitMenu.SetActive(false);
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("UI Clicks");
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        FindObjectOfType<AudioManager>().Play("UI Clicks");
        SceneManager.LoadScene("MainGame");
    }

    public void LoseMenu()
    {
        Time.timeScale = 0;
        FindObjectOfType<AudioManager>().Play("Lose Screen");
        _loseMenu.SetActive(true);
        _loseGrandmaText.text = string.Format("{0}", _score);
        _plantDurationText.text = string.Format("Days : {0} Day - {1} Hours", _days, _hoursPlay);
    }

    void LoadPlants()
    {
        for (int i = 0; i < _plant.Length; i++)
        {
            string str, str2;
            str = string.Format("planted_{0}", i);
            _plant[i].GetComponent<PlantHealth>().SetPlanted(Convert.ToBoolean(PlayerPrefs.GetInt(str)));
            Debug.Log(_plant[i].GetComponent<PlantHealth>().GetPlanted());
            str = string.Format("soiled_{0}", i);
            _plant[i].GetComponent<PlantHealth>().SetSoiled(Convert.ToBoolean(PlayerPrefs.GetInt(str)));
            str = string.Format("fertilizer_{0}", i);
            _plant[i].GetComponent<PlantHealth>().fertilizerAmount = PlayerPrefs.GetInt(str);
            str = string.Format("old_{0}", i);
            _plant[i].GetComponent<PlantHealth>().plantOld = PlayerPrefs.GetInt(str);
            str = string.Format("phase_{0}", i);
            _plant[i].GetComponent<PlantHealth>().SetPhase(PlayerPrefs.GetInt(str));
            str = string.Format("health_{0}", i);
            _plant[i].GetComponent<PlantHealth>().plantHealth = PlayerPrefs.GetFloat(str);
            str = string.Format("water_{0}", i);
            _plant[i].GetComponent<PlantHealth>().plantWater = PlayerPrefs.GetFloat(str);
            str = string.Format("plant_{0}", i);
            str2 = string.Format("Plants/{0}.asset", PlayerPrefs.GetString(str));
            Debug.Log(PlayerPrefs.GetString(str));
            var plants = Resources.Load<Plant>(str2);
            _plant[i].GetComponent<PlantHealth>().SetState();
            if (PlayerPrefs.GetString(str) != "")
            {
                for (int j = 0; j < allPlants.Length; j++)
                {
                    if (PlayerPrefs.GetString(str) == allPlants[j].plantName)
                    {
                        _plant[i].GetComponent<PlantHealth>().plants = allPlants[j];
                    }
                }
                _plant[i].GetComponent<PlantHealth>()._seedPlants.GetComponent<Image>().sprite = _plant[i].GetComponent<PlantHealth>().plants.plantStages[_plant[i].GetComponent<PlantHealth>().GetPhase()-1];
                _plant[i].GetComponent<PlantHealth>()._seedPlants.GetComponent<Image>().enabled = true;
            }
            else
            {
                _plant[i].GetComponent<PlantHealth>()._seedPlants.GetComponent<Image>().enabled = false;
            }

            if(_plant[i].GetComponent<PlantHealth>().GetSoiled() == true)
            {
                _plant[i].GetComponent<PlantHealth>().SetSoil();
            }
            else
            {
                _plant[i].GetComponent<PlantHealth>().UnsetSoil();
            }
        }

        Debug.Log("Game Loaded!");
    }

    void SavePlants()
    {
        for (int i = 0; i < _plant.Length; i++)
        {
            string str;
            str = string.Format("planted_{0}", i);
            PlayerPrefs.SetInt(str, Convert.ToInt32(_plant[i].GetComponent<PlantHealth>().GetPlanted()));
            str = string.Format("soiled_{0}", i);
            PlayerPrefs.SetInt(str, Convert.ToInt32(_plant[i].GetComponent<PlantHealth>().GetSoiled()));
            str = string.Format("fertilizer_{0}", i);
            PlayerPrefs.SetInt(str, _plant[i].GetComponent<PlantHealth>().fertilizerAmount);
            str = string.Format("old_{0}", i);
            PlayerPrefs.SetInt(str, _plant[i].GetComponent<PlantHealth>().plantOld);
            str = string.Format("phase_{0}", i);
            PlayerPrefs.SetInt(str, _plant[i].GetComponent<PlantHealth>().GetPhase());
            str = string.Format("health_{0}", i);
            PlayerPrefs.SetFloat(str, _plant[i].GetComponent<PlantHealth>().plantHealth);
            str = string.Format("water_{0}", i);
            PlayerPrefs.SetFloat(str, _plant[i].GetComponent<PlantHealth>().plantWater);
            str = string.Format("plant_{0}", i);
            Debug.Log(_plant[i].GetComponent<PlantHealth>().plants);
            if(_plant[i].GetComponent<PlantHealth>().plants != null)
            {
                PlayerPrefs.SetString(str, _plant[i].GetComponent<PlantHealth>().plants.plantName);
            }
            else
            {
                PlayerPrefs.SetString(str, null);
            }
            Debug.Log(str);
        }
        PlayerPrefs.SetInt("days", _days);
        PlayerPrefs.SetInt("hours", _hours);
        PlayerPrefs.SetInt("hoursremain", _hoursRemain);
        PlayerPrefs.SetFloat("timers", _timers);
        PlayerPrefs.Save();
        Debug.Log("Game Saved!");
    }
}
