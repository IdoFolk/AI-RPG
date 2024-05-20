using System;
using UnityEngine;

[CreateAssetMenu(menuName = "AIConfig/NPC", fileName = "NPCAIConfig")]
public class NPCAIConfig : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string role;
    [SerializeField] private string startingDialog;
    [TextArea(5,10)]
    [SerializeField] private string npcRoleDiscription;
    [SerializeField] private LocationAIConfig location;
    [SerializeField] private WorldSettingAIConfig worldSetting;
    [SerializeField,Range(0,1)] private float creativity;

    public string StartingDialog => startingDialog;
    public string NpcRoleDiscription => npcRoleDiscription;
    public string Role => role;
    public float Creativity => creativity;
    public string Name => _name;
    public LocationAIConfig Location => location;
    public WorldSettingAIConfig WorldSetting => worldSetting;

    private void OnValidate()
    {
        if(location != null) location.AddNPCToLocation(this);
    }
}
