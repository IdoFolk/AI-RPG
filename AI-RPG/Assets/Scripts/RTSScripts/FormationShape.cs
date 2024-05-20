using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class FormationShape 
{
 public enum Shape {Cube, Non }
    internal Shape shape;
    

    internal int posQuantity;
    internal Vector3 middle;
    internal Group _group;
    public FormationShape(int quantity, Group group)
    {
        posQuantity = quantity;
        _group = group;

    }
    public abstract Vector3[] GetPosArray(Vector3 destination, Vector3 forwardDirection, Vector3 rightDirection);
    public abstract Vector3[] ConformShape(Vector3 destination, Vector3 forwardDirection, Vector3 rightDirection);
}
[System.Serializable]
public class Cube : FormationShape
{

    public Cube(int quantity, Group group,Cube cube) : base(quantity, group)
    {
        maxInLine = cube.maxInLine;
        gapBetweenLines = cube.gapBetweenLines;
        gapBetweenRaws = cube.gapBetweenRaws;
    }

    public int maxInLine;
    public float gapBetweenRaws;
    public float gapBetweenLines;
    

    public override Vector3[] ConformShape(Vector3 destination,Vector3 forwardDirection,Vector3 rightDirection)
    {
        Vector3[] positions = new Vector3[posQuantity];

        int counter=0;
        float rawQuantity = posQuantity / maxInLine;
        int Raws = Mathf.CeilToInt(rawQuantity+1); 

        for (int x = 0; x <Raws; x++)
        {
           
            //they need to go to a certein direction from input currently they just go to middle -_-
            
            Vector3 LastPositivePoint = destination + (-forwardDirection * x * gapBetweenRaws);
            Vector3 LastNegativePoint = LastPositivePoint;
            bool Positive=false;
            for (int y = 0; y < maxInLine; y++)
            {
                if (counter == posQuantity) { break; }
                if (y == 0)
                {
                    positions[counter] = LastPositivePoint;
                }
                else
                {
                    Positive = !Positive;
                    if (!Positive)
                    {
                        Vector3 Dist = -rightDirection * gapBetweenLines;
                        LastNegativePoint += Dist;
                        positions[counter] = LastNegativePoint;
                    }
                    else
                    {
                        Vector3 Dist = rightDirection * gapBetweenLines;
                        LastPositivePoint += Dist;
                        positions[counter] = LastPositivePoint;
                    }
                }
              
                counter++;
            }
       

            
        }
        
        return positions;

    }

    public override Vector3[] GetPosArray(Vector3 destination, Vector3 forwardDirection, Vector3 rightDirection)
    {
        return ConformShape(destination, forwardDirection, rightDirection);
    }
}
