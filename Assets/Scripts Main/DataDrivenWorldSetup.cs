using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private WorldData currentWorldData;

    void Start()
    {
        // See if the world index already exists (since it was loaded in another scene)
        var currentWorldIndex = GameObject.Find("CurrentWorldIndex");
        if (currentWorldIndex == null)
        {
            // Since it is not there, create it, and mark it so it isn't destroyed when the scene changes
            currentWorldIndex = Instantiate(currentWorldIndexPrefab);
            DontDestroyOnLoad(currentWorldIndex);
        }

        // Get the current world Data by using the world index value, and pulling out the index
        var worldIndexObj = currentWorldIndex.GetComponent<CurrentWorldIndex>();
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
    }

}
