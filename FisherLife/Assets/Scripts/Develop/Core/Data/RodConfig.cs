using UnityEngine;

[CreateAssetMenu(fileName = "Rod", menuName = "Config/RodConfig")]
public class RodConfig : ScriptableObject
{
    public string Name;
    public int Level;
    [TextArea(0,5)]
    public string Description;
}
