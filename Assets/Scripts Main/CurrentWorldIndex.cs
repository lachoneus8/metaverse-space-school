using UnityEngine;

/// <summary>
/// This class is intended to be used to keep track of the current value of the next world being loaded
/// based on the choice made in a previous scene.
/// 
/// DataDrivenWorldSetup owns creating an instance of this and marking it so it sticks around between scene transitions.
/// </summary>
public class CurrentWorldIndex : MonoBehaviour
{
    public int currentWorldIndex;
}
