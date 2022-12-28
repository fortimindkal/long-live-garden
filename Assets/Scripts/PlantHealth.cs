using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PlantHealth : MonoBehaviour
{
    public GameObject _objectPlants;
    public GameObject _seedPlants;
    public GameObject[] _soil;
    public PlayerInteraction player;

    public Image _selectPlants;

    public enum PlantState
    {
        Empty,
        Seeds,
        Healthy,
        Pollinated,
        Mature,
        Withered,
        Dead
    }
    public PlantState plantState;

    public Plant plants;

    private bool isPlanted;
    private bool isSoiled = false;
    public int fertilizerAmount = 0;
    public int plantOld;
    [SerializeField]
    private int plantPhase;

    public float plantHealth;
    public float plantWater;

    void Start()
    {
        _selectPlants.enabled = false;

        for (int i = 0; i < _soil.Length; i++)
        {
            if(isSoiled == false)
            {
                _soil[i].SetActive(false);
            }
        }
    }

    void Update()
    {
        if(isPlanted == true)
        {
            if (plantWater > 0)
            {
                if (plantHealth < plants.plantMaxHealth) 
                    plantHealth += 5f * Time.deltaTime;
                if (plantHealth > plants.plantMaxHealth) 
                    plantHealth = plants.plantMaxHealth;
                //plantWater -= plants.plantConsume / 43f * Time.deltaTime;
                plantWater -= plants.plantConsume / 10f * Time.deltaTime;
            }
            else
            {
                if (plantHealth < plants.plantPoorHealth)
                {
                    if (plantPhase != 4)
                    {
                        plantPhase = 4;
                        UpdatePlants();
                        plantState = PlantState.Withered;
                    }
                }
                if (plantHealth < 0f)
                {
                    if (plantPhase != 5)
                    {
                        plantPhase = 5;
                        UpdatePlants();
                        plantState = PlantState.Dead;
                        plantHealth = 0f;
                    }
                }
                plantHealth -= 5f * Time.deltaTime;
            }

            if(plantOld >= 2 * plants.plantPhaseDay)
            {
                if(plantPhase == 1)
                {
                    plantPhase = 2;
                    plantState = PlantState.Healthy;
                    UpdatePlants();
                } 
            }

            if (plantOld >= 3 * plants.plantPhaseDay)
            {
                if (plantPhase == 2)
                {
                    plantPhase = 3;
                    plantState = PlantState.Mature;
                    UpdatePlants();
                }    
            }
        }
    }

    public void SetSoil()
    {
        isSoiled = true;
        for (int i = 0; i < _soil.Length; i++)
        {
            _soil[i].SetActive(true);
        }
    }

    public void UnsetSoil()
    {
        isSoiled = false;
        for (int i = 0; i < _soil.Length; i++)
        {
            _soil[i].SetActive(false);
        }
    }

    public void ResetPlants()
    {
        plantState = PlantState.Empty;
        plantHealth = 0;
        plantWater = 0;
        plantOld = 0;
        plantPhase = 0;
        plants = null;
        _seedPlants.GetComponent<Image>().enabled = false;
    }

    public void SetPlants()
    {
        plantState = PlantState.Seeds;
        plantHealth = plants.plantMaxHealth;
        plantWater = 0;
        plantOld = 0;
        plantPhase = 1;
        _seedPlants.GetComponent<Image>().sprite = plants.plantStages[0];
        _seedPlants.GetComponent<Image>().enabled = true;
    }

    void UpdatePlants()
    {
        if(plantHealth > plants.plantPoorHealth)
        {
            if(plantOld >= 2 * plants.plantPhaseDay)
            {
                _seedPlants.GetComponent<Image>().sprite = plants.plantStages[1];
            }

            if (plantOld >= 3 * plants.plantPhaseDay)
            {
                _seedPlants.GetComponent<Image>().sprite = plants.plantStages[2];
            }
        }
        else
        {
            if(plantHealth < 0f)
            {
                if(plantState != PlantState.Withered)
                    _seedPlants.GetComponent<Image>().sprite = plants.plantStages[4];
            }
            else
            {
                if (plantState != PlantState.Dead)
                    _seedPlants.GetComponent<Image>().sprite = plants.plantStages[3];
            }
        }
        FindObjectOfType<AudioManager>().Play("Planting Grow");
    }

    public bool GetSoiled()
    {
        return isSoiled;
    }

    public bool SetSoiled(bool status)
    {
        return isSoiled = status;
    }

    public bool GetPlanted()
    {
        return isPlanted;
    }

    public bool SetPlanted(bool status)
    {
        return isPlanted = status;
    }

    public int GetPhase()
    {
        return plantPhase;
    }

    public int SetPhase(int phase)
    {
        return plantPhase = phase;
    }

    public void SetPlantState(PlantState state)
    {
        plantState = state;
    }

    public PlantState GetPlantState()
    {
        return plantState;
    }

    public PlantState ConvertState(string state)
    {
        PlantState ps = (PlantState)System.Enum.Parse(typeof(PlantState), state);
        return ps;
    }

    public void SetState()
    {
        switch(plantPhase)
        {
            case 0:
            {
                plantState = PlantState.Empty;
                break;
            }
            case 1:
            {
                plantState = PlantState.Seeds;
                break;
            }
            case 2:
            {
                plantState = PlantState.Healthy;
                break;
            }
            case 3:
            {
                plantState = PlantState.Mature;
                break;
            }
            case 4:
            {
                plantState = PlantState.Withered;
                break;
            }
            case 5:
            {
                plantState = PlantState.Dead;
                break;
            }
        }
    }
}