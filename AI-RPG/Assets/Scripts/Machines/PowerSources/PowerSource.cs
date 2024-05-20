using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PowerSource : Machine
{
    
    enum PowerSourceType { Container, Producer}

    [OnValueChanged("SetupPowerSource", IncludeChildren =true)]
    [SerializeField]
    List<PowerSourceType> sourceTypeList;

	[ReadOnly]
	[SerializeField]
    bool isContainer = false;
	public bool getIsContainer => isContainer;

	[ReadOnly]
	[SerializeField]
	bool isProducer = false;
	public bool getIsProducer => isContainer;

	[ShowIf("isContainer")]
    [SerializeField]
    float storedPower;

	[ShowIf ("isContainer")]
	[SerializeField]
	float powerStoreLimit;

	[ShowIf ("isProducer")]
	[SerializeField]
	float powerGenerationRate;

	LineRenderer portLineRenderer;

	// Update is called once per frame
	void Update()
    {
		if (isProducer && isContainer && storedPower < powerStoreLimit) {
			
			storedPower += powerGenerationRate * Time.deltaTime;
			storedPower = Mathf.Clamp (storedPower, 0, powerStoreLimit);
		}
	}

	public void ProvidePower () {
		storedPower = getOwnerMachineBase.ProvidePower (storedPower);
	}
#if UNITY_EDITOR

    void SetupPowerSource () {
        isContainer = sourceTypeList.Contains(PowerSourceType.Container);
		isProducer = sourceTypeList.Contains (PowerSourceType.Producer);
	}
#endif
}
