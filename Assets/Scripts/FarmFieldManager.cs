using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FarmFieldManager : MonoBehaviour
{
    [SerializeField] private GameObject[] plants;
    static public FarmFieldManager instance;
    [SerializeField] private Material brownMaterial;

    [SerializeField] private List<GameObject> plantedPlants = new List<GameObject>(); // Ekilen bitkilerin listesi
    [SerializeField] private List<GameObject> harvestedPlants = new List<GameObject>(); // Hasat edilen bitkilerin listesi

    private int currentPlantIndex = 0;

    void Start()
    {
        instance = this;
        foreach (var item in plants)
        {
            item.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }

    public void SetPlantColor()
    {
        if (currentPlantIndex < plants.Length)
        {
            GameObject currentPlant = plants[currentPlantIndex];
            currentPlant.GetComponent<MeshRenderer>().material.color = Color.green;
            plantedPlants.Add(currentPlant);
            currentPlantIndex++;
        }
    }

    public void SetHarvestColor()
    {
        if (plantedPlants.Count > 0)
        {
            GameObject harvestedPlant = plantedPlants[0];
            harvestedPlant.GetComponent<MeshRenderer>().material.color = Color.yellow;
            harvestedPlants.Add(harvestedPlant);
            plantedPlants.RemoveAt(0);
        }
    }

    public void SetSellColor()
    {
        if (harvestedPlants.Count > 0)
        {
            GameObject soldPlant = harvestedPlants[0];
            soldPlant.GetComponent<MeshRenderer>().material = brownMaterial;
            harvestedPlants.RemoveAt(0);
           
        }
    }
}
