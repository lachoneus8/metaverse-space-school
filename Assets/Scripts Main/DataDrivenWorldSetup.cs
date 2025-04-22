using TMPro;
using UnityEngine;

public class DataDrivenWorldSetup : MonoBehaviour
{
    public WorldData worldData;

    public TMP_Text titleText;

    public TMP_Text gravityText;
    public TMP_Text temperatureText;
    public TMP_Text dayLengthText;
    public TMP_Text distanceFromSunText;

    public MeshRenderer groundPlane;

    void Start()
    {
        titleText.text = "Welcome to " + worldData.worldName;

        gravityText.text = "Gravity: " + worldData.gravityMPS2 + "m/s^2";
        temperatureText.text = "Temperature: " + worldData.minTempF + "-" + worldData.maxTempF + " F";
        dayLengthText.text = "Day length: " + worldData.dayNightRotationLength.ToString();
        distanceFromSunText.text = "Distance from sun: " + worldData.distanceFromSunAU + " AU";

        if (worldData.optGroundMaterial != null)
        {
            groundPlane.material = worldData.optGroundMaterial;
        }
    }

}
