using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DataDrivenWorldSetup : MonoBehaviour
{
    public GameObject currentWorldIndexPrefab;

    public List<WorldData> worldDataList;

    public TMP_Text titleText;

    public TMP_Text gravityText;
    public TMP_Text temperatureText;
    public TMP_Text dayLengthText;
    public TMP_Text distanceFromSunText;

    public GravitySetting gravitySetting;

    public MeshRenderer groundPlane;

    public Dropdown planetChoiceDropdown;

    public TransitionController transitionController;

    private WorldData currentWorldData;

    public void OnTravelToWorld()
    {
        // Gets the object that tracks the next world between scenes, and set its value based on the current dropdown
        var worldIndexObj = GetWorldIndex();
        worldIndexObj.currentWorldIndex = planetChoiceDropdown.value;
        transitionController.GoToGenericWorld();
    }

    void Start()
    {
        var worldIndexObj = GetWorldIndex();
        currentWorldData = worldDataList[worldIndexObj.currentWorldIndex];

        // Update text according to the world we're using
        titleText.text = "Welcome to " + currentWorldData.worldName;

        gravityText.text = "Gravity: " + currentWorldData.gravityMPS2 + "m/s^2";
        temperatureText.text = "Temperature: " + currentWorldData.minTempF + "-" + currentWorldData.maxTempF + " F";
        dayLengthText.text = "Day length: " + currentWorldData.dayNightRotationLength.ToString();
        distanceFromSunText.text = "Distance from sun: " + currentWorldData.distanceFromSunAU + " AU";

        gravitySetting.GravityValue = currentWorldData.gravityMPS2;

        // Update the ground plane
        if (currentWorldData.optGroundMaterial != null)
        {
            groundPlane.material = currentWorldData.optGroundMaterial;
        }

        SetupWorldDropdown(worldIndexObj);
    }

    private CurrentWorldIndex GetWorldIndex()
    {
        // See if the world index already exists (since it was loaded in another scene)
        CurrentWorldIndex currentWorldIndex = GameObject.FindFirstObjectByType<CurrentWorldIndex>();
        if (currentWorldIndex == null)
        {
            // Since it is not there, create it, and mark it so it isn't destroyed when the scene changes
            var instantiated = Instantiate(currentWorldIndexPrefab);
            currentWorldIndex = instantiated.GetComponent<CurrentWorldIndex>();
            DontDestroyOnLoad(currentWorldIndex);
        }

        // Get the current world Data by using the world index value, and pulling out the index
        return currentWorldIndex;
    }

    private void SetupWorldDropdown(CurrentWorldIndex currentIndex)
    {
        planetChoiceDropdown.ClearOptions();

        var planetOptionNames = new List<string>();

        int worldIdx = 0;
        int toSelect = 0;
        foreach (var world in worldDataList)
        {
            planetOptionNames.Add(world.worldName);
            // Check against the currently selected world index.
            // If it matches, mark it so we can have this one be the one currently highlighted.
            if (worldIdx == currentIndex.currentWorldIndex)
            {
                toSelect = worldIdx;
            }
            ++worldIdx;
        }

        planetChoiceDropdown.AddOptions(planetOptionNames);
        planetChoiceDropdown.value = toSelect;
    }
}
