using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    FieldOfView _fow;
    [SerializeField] GameObject AlertedMark;

    void Start()
    {
        _fow = GetComponent<FieldOfView>();
        StartCoroutine(GetView());
    }

    IEnumerator GetView()
    {
        while (true)
        { List<Transform> targets= _fow.GetTransformList();
            if (targets.Count > 0)
            {
                AlertedMark.SetActive(true);
            foreach (Transform found in targets)
            {
                Debug.Log(found);
                
            }
            }
            else { AlertedMark.SetActive(false); }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
