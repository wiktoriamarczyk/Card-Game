using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsSwitcher : MonoBehaviour
{
    [SerializeField] List<GameObject> mainObjects;
    [SerializeField] List<GameObject> alternativeObjects;

    bool mainObjectOn = true;

    public void SwitchObjects()
    {
        mainObjectOn = !mainObjectOn;
        foreach (var mainObject in mainObjects)
        {
            mainObject.SetActive(mainObjectOn);
        }
        foreach (var alternativeObject in alternativeObjects)
        {
            alternativeObject.SetActive(!mainObjectOn);
        }
    }
}
