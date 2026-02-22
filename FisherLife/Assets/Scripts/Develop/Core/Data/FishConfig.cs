using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "Config/Fish")]
public class FishConfig : ScriptableObject
{
    public string Name;
    public int Level;
    [TextArea(0, 5)]
    public string Description;
}
