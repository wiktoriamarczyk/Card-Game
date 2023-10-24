using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSwitcher : MonoBehaviour
{
    [SerializeField] GameObject mainObject;
    [SerializeField] GameObject alternativeObject;

    bool mainObjectOn = true;

    public void SwitchObjects()
    {
        mainObjectOn = !mainObjectOn;
        mainObject.SetActive(mainObjectOn);
        alternativeObject.SetActive(!mainObjectOn);
    }
}
