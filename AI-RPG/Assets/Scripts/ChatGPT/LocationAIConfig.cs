using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIConfig/Location", fileName = "LocationAIConfig")]
public class LocationAIConfig : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField][TextArea(5,10)] private string locationDiscription;

    public string LocationDiscription => locationDiscription;
    public string Name => _name;
    public List<NPCAIConfig> AssignedNPCs;

    public void AddNPCToLocation(NPCAIConfig npcAIConfig)
    {
        if(AssignedNPCs.Contains(npcAIConfig)) return;
        AssignedNPCs.Add(npcAIConfig);
    }

    public string GetNPCsStringFormat()
    {
        var str = "";
        foreach (var config in AssignedNPCs)
        {
            str += $"{config.Name} The {config.Role}, ";
        }

        return str;
    }
}
