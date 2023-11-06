using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Resposnible for switching between two sets of objects.
/// </summary>
public class ObjectsSwitcher : MonoBehaviour
{
    [SerializeField] List<GameObject> mainObjects;
    [SerializeField] List<GameObject> alternativeObjects;

    bool mainObjectOn = true;

    /// <summary>
    /// Switches between two sets of objects.
    /// </summary>
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
