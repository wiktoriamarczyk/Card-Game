using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] GameObject container;
    [SerializeField] Button returnButton;
    [SerializeField] Image buildingImageDisplay;
    [SerializeField] TextMeshProUGUI buildingNameDisplay;
    [SerializeField] TextMeshProUGUI buildingDescriptionDisplay;
    [SerializeField] TextMeshProUGUI cityNameDisplay;

    private void Awake()
    {
        returnButton.onClick.AddListener(() => container.SetActive(false));
    }

    public void Display(Sprite buildingImage, string buildingName, string buildingDescription)
    {
        container.SetActive(true);
        cityNameDisplay.text = Game.jsonHandler.readLevel.name;
        buildingImageDisplay.sprite = buildingImage;
        buildingNameDisplay.text = buildingName;
        buildingDescriptionDisplay.text = buildingDescription;
    }
}
