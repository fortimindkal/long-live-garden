using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PotManager : MonoBehaviour, IPointerDownHandler 
{
    [SerializeField]
    private bool isPlanted;
    public PlantHealth _plants;
    public PlayerInteraction _player;

    void Start()
    {
        _plants.SetPlanted(false);
    }

    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        FindObjectOfType<AudioManager>().Play("UI Clicks");
        if(_player._selectedObject != null)
        {
            _player._selectedObject.GetComponent<PlantHealth>()._selectPlants.enabled = false;
        }
        _player._selectedObject = gameObject;
        _player._selectedObject.GetComponent<PlantHealth>()._selectPlants.enabled = true;
    }
}
