using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group
{

    #region Constructos
    public Group() { }
    #endregion


    #region Private Fields
    List<BasicUnit> GroupUnits = new List<BasicUnit>();
    Formation _groupFormation;
    #endregion

  

    /// <summary>
    /// checking which shape to execute on formation
    /// </summary>
    /// <param name="shape"></param>
    /// <returns></returns>
    public FormationShape GenerateShape(FormationShape shape)
    {
        switch (shape.shape)
        {
            case FormationShape.Shape.Cube:
                shape = new Cube(groupSize(), this, (Cube)shape);
                break;
            case FormationShape.Shape.Non:
                Debug.Log("yo");
                break;

            default:
                Debug.Log("yo");
                break;
        }
        return shape;
    }

    /// <summary>
    /// creaate new Group of units
    /// </summary>
    /// <param name="UnitList"></param>
    public Group(List<BasicUnit> UnitList)
    {
        if (UnitList.Count > 0)
        {
            GroupUnits.AddRange(UnitList);
            foreach (BasicUnit found in GroupUnits)
            {
                found.Clicked();
            }
            
        }
        else { Debug.LogError("Unit less group!!!"); }

    }




    #region Inputs
    //Adding one Unit
    public void AddUnit(BasicUnit basicUnit)
    {
        GroupUnits.Add(basicUnit);
        basicUnit.Clicked();
    }

    //Adding Full List and telling all the units
    public void AddUnitList(List<BasicUnit> UnitList)
    {
        if (UnitList.Count > 0)
        {
            GroupUnits.AddRange(UnitList);
        }
        else { Debug.LogError("No Units In List"); }

        foreach (BasicUnit found in GroupUnits)
        {
            found.Clicked();
        }

    }

    //clearing the group and telling all the units
    public void ClearGroup()
    {
        foreach (BasicUnit found in GroupUnits)
        {
            found.ReleaseCilck();
        }
        GroupUnits.Clear();

    }
    //Sending group to certain position
    public void SendGroupTowards(Vector3 pointOnMap, FormationShape Shape, Vector3 groupOrient)
    {
        _groupFormation = new Formation(this);
        _groupFormation.ConformToFormation(Shape, pointOnMap, groupOrient,Vector3.Cross(groupOrient,Vector3.up));
    }


    #endregion


    #region Outputs
    public int groupSize() => GroupUnits.Count;
    public List<BasicUnit> GetUnitList => GroupUnits;
    public Vector3 GetGroupMiddle
    {
        get
        {

            Vector3 Middle = Vector3.zero;

            //Making the Average Position of All GroupUnits 
            foreach (BasicUnit Found in GroupUnits)
            {
                Middle += Found.transform.position;
            }
            Middle /= GroupUnits.Count;

            return Middle;
        }
    }
    #endregion


    #region Caulculations

    #endregion




}