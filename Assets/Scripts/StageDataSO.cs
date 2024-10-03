using UnityEngine;

[CreateAssetMenu(fileName = "New Stage Data", menuName = "Stage Data")]
public class StageDataSO : ScriptableObject
{
    public string stageName;
    public int scoreRequirement;
    public float timeLimit;
    public string levelName; //name to show in popup
    public string popUpMessage;      // Custom message for the stage

}