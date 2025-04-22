using System;
using System.Security.Policy;
using UnityEngine;

[CreateAssetMenu(fileName="World", menuName="MetaverseSpaceSchool/CreateWorld")]
public class WorldDataEditor : ScriptableObject
{
    #region Classes
    /// <summary>
    /// This contains time as a serializable object so it can appear in the inspector, as TimeSpan is not
    /// </summary>
    [Serializable]
    public class SerializableTime
    {
        public int days;
        public int hours;
        public int minutes;
    }
    #endregion

    #region Fields

    public string worldName;
    public string sceneName;
    [Tooltip("Gravity in m/s2 at the equator")]
    public float gravityMPS2;

    [Tooltip("The minimum nighttime temperature in F")]
    public int minTempF;
    [Tooltip("The maximum daytime temperature in F")]
    public int maxTempF;

    [Tooltip("The time per full rotation")]
    public SerializableTime dayNightRotationLength;

    [Tooltip("The distance from the sun in AU")]
    public float distanceFromSunAU;

    [Tooltip("This field is optional.  Only use it if scene is set to the generic world scene")]
    public Material optGroundMaterial;

    #endregion
}
