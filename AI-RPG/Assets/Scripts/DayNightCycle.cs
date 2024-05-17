using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Light mainLightSource;
    void Update()
    {
        transform.Rotate(Quaternion.Euler(rotateSpeed * Time.deltaTime,0,0).eulerAngles);
        //mainLightSource.shadowStrength = 0;
    }
}
