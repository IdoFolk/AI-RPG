using UnityEngine;

[CreateAssetMenu(menuName = "AIConfig/WorldSetting", fileName = "WorldSettingAIConfig")]
public class WorldSettingAIConfig : ScriptableObject
{
    [SerializeField][TextArea(5,10)] private string worldDiscription;

    public string WorldDiscription => worldDiscription;
}
