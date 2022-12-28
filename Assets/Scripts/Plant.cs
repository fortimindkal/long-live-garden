using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Plant", menuName ="Plant")]
public class Plant : ScriptableObject
{
    public string plantName;
    public float plantMaxHealth;
    public float plantPoorHealth;
    public float plantMaxWater;
    public float plantConsume;
    public int plantPhaseDay;
    public Sprite[] plantStages;
}