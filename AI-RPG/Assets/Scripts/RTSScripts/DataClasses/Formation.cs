using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Formation 
{
    #region Constructors

    /// <summary>
    /// inserting all units to FormationDic ordered from 0 to unitCount
    /// </summary>
    /// <param name="group"></param>
    public Formation(Group group)
    {
        _group = group;

        List<BasicUnit> UnitList = group.GetUnitList;
        int counter = 0;
        foreach (BasicUnit found in UnitList)
        {
            Formationdic.Add(counter, found);
            counter++;
        }

    }

    #endregion


    #region PrivateFields

    Dictionary<int, BasicUnit> Formationdic = new Dictionary<int, BasicUnit>();
    Group _group;

    #endregion


    #region Searialize Fields

    [SerializeField] float UnitsInRow;

    #endregion


    #region Proccesses

    /// <summary>
    /// Sets Each Unit movement and orientation inside formation
    /// </summary>
    /// <param name="Shape"></param>
    /// <param name="destination"></param>
    /// <param name="orientation"></param>
    /// <param name="origin"></param>
    public void ConformToFormation(FormationShape Shape, Vector3 destination, Vector3 orientation, Vector3 rightDirection)
    {
        
        Vector3[] positions = Shape.ConformShape(destination, orientation,rightDirection);

        foreach (KeyValuePair<int, BasicUnit> found in Formationdic)
        {
            found.Value.GoTo(positions[found.Key], orientation);
        }
    }

    #endregion


}

