using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject _selectedObject;
    public Plant _selectedPlant;

    public Image _healthBar;
    public Image _waterBar;
    public Button _waterButton;
    public Button _giveButton;
    public Button _trashButton;
    public Button _soilButton;
    public Button[] _seedButton;
    public Button _fertilizerButton;
    public Button _fireSprinklerButton;
    public Text _plantText;
    public Text _stateText;
    public Text _healthText;

    void Start()
    {
        _waterButton.interactable = false;
        _giveButton.interactable = false;
        _trashButton.interactable = false;
        _soilButton.interactable = false;
        for(int i = 0; i < _seedButton.Length; i++)
        {
            _seedButton[i].interactable = false;
        }
        _fertilizerButton.interactable = false;
        _fireSprinklerButton.interactable = false;
        _healthBar.fillAmount = 0f;
        _waterBar.fillAmount = 0f;
        _plantText.text = "No Plant";
        _stateText.text = "";
        _healthText.text = "";
    }

    void Update()
    {       
        if(_selectedObject != null)
        {
            if(_selectedObject.GetComponent<PlantHealth>().plantState == PlantHealth.PlantState.Mature)
            {
                _giveButton.interactable = true;
            }

            if (_selectedObject.GetComponent<PlantHealth>().plants != null)
            {
                _plantText.text = _selectedObject.GetComponent<PlantHealth>().plants.plantName;
            }
            else
            {
                _plantText.text = "No Plant";
            }

            if (_selectedObject.GetComponent<PlantHealth>().GetSoiled() == true)
            {
                _soilButton.interactable = false;
                if(_selectedObject.GetComponent<PlantHealth>().GetPlanted() == false)
                {
                    for (int i = 0; i < _seedButton.Length; i++)
                    {
                        _seedButton[i].interactable = true;
                    }
                }
                else
                {
                    if (_selectedObject.GetComponent<PlantHealth>().fertilizerAmount >= 2)
                    {
                        _fertilizerButton.interactable = false;
                    }
                    else
                    {
                        _fertilizerButton.interactable = true;

                    }

                    for (int i = 0; i < _seedButton.Length; i++)
                    {
                        _seedButton[i].interactable = false;
                    }
                    _trashButton.interactable = true;
                    _waterButton.interactable = true;
                    _fireSprinklerButton.interactable = true;
                }
            }
            else
            {
                for (int i = 0; i < _seedButton.Length; i++)
                {
                    _seedButton[i].interactable = false;
                }
                _soilButton.interactable = true;
                _waterButton.interactable = false;
                _fertilizerButton.interactable = false;
                _fireSprinklerButton.interactable = false;
            }
            _stateText.text = string.Format("State : {0}", _selectedObject.GetComponent<PlantHealth>().plantState.ToString());
            _healthText.text = string.Format("Health : {0}%", Mathf.RoundToInt(_selectedObject.GetComponent<PlantHealth>().plantHealth));
            _healthBar.fillAmount = _selectedObject.GetComponent<PlantHealth>().plantHealth / 100f;
            _waterBar.fillAmount = _selectedObject.GetComponent<PlantHealth>().plantWater / 30f;

        }
        else
        {
            _giveButton.interactable = false;
            _waterButton.interactable = false;
            _trashButton.interactable = false;
            _plantText.text = "No Plant";
            _stateText.text = "";
            _healthText.text = "";
            _healthBar.fillAmount = 0f;
            _waterBar.fillAmount = 0f;
        }
    }

    public void WaterPlant()
    {
        if (_selectedObject != null)
        {
            FindObjectOfType<AudioManager>().Play("Water Pouring");
            _selectedObject.GetComponent<PlantHealth>().plantWater += 5f;
            Debug.Log("The plant has been watered.");
        }
    }

    public void FertilizePlant()
    {
        if (_selectedObject != null)
        {
            FindObjectOfType<AudioManager>().Play("Put Soil");
            _selectedObject.GetComponent<PlantHealth>().plantHealth += 75f;
            _selectedObject.GetComponent<PlantHealth>().fertilizerAmount++;
            Debug.Log("The plant has been fertilized.");
        }
    }

    public void HarvestPlant()
    {
        if (_selectedObject != null)
        {
            FindObjectOfType<AudioManager>().Play("Give to Grandma");
            gameManager.GetComponent<GameManager>().IncreaseScore(1);
            DisabledButton();
        }
    }

    public void TrashPlant()
    {
        if(_selectedObject != null)
        {
            FindObjectOfType<AudioManager>().Play("Put Trash");
            gameManager.GetComponent<GameManager>().IncreaseTrash(1);
            DisabledButton();
        }
    }

    public void PlantSeed(Plant seed)
    {
        if (_selectedObject.GetComponent<PlantHealth>().GetPlanted() == false && _selectedObject.GetComponent<PlantHealth>().GetSoiled() == true)
        {
            FindObjectOfType<AudioManager>().Play("Planting Seeds");
            _selectedObject.GetComponent<PlantHealth>().plants = seed;
            _selectedObject.GetComponent<PlantHealth>().SetPlanted(true);
            _selectedObject.GetComponent<PlantHealth>().SetPlants();
            Debug.Log(seed);
        }
    }

    public void PlantSoil()
    {
        if (_selectedObject != null)
        {
            if (_selectedObject.GetComponent<PlantHealth>().GetSoiled() == false)
            {
                FindObjectOfType<AudioManager>().Play("Put Soil");
                _selectedObject.GetComponent<PlantHealth>().SetSoil();
            }
        }
    }

    private void DisabledButton()
    {
        _selectedObject.GetComponent<PlantHealth>()._selectPlants.enabled = false;
        _selectedObject.GetComponent<PlantHealth>().SetPlanted(false);
        _selectedObject.GetComponent<PlantHealth>().SetSoiled(false);
        _waterButton.interactable = false;
        _selectedObject.GetComponent<PlantHealth>().ResetPlants();
        _selectedObject.GetComponent<PlantHealth>().UnsetSoil();
        _selectedObject.GetComponent<PlantHealth>()._seedPlants.GetComponent<Image>().enabled = false;
        _selectedObject = null;
        _trashButton.interactable = false;
        _fertilizerButton.interactable = false;
        _fireSprinklerButton.interactable = false;
        for (int i = 0; i < _seedButton.Length; i++)
        {
            _seedButton[i].interactable = false;
        }
        _giveButton.interactable = false;
    }
}
