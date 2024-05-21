using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Collections/ShapeCollection")]
public class ShapeCollection : ScriptableObject
{
    public List<Shape> formationShapes;
   // internal FormationShape[] formationShapes;
    public FormationShape GetFormation
    {
        get
        {
            return formationShapes[0].GetShape;
        }
    }
}

 [CreateAssetMenu(menuName = "ClassData/Shapes/Cube")]
public class Shape : ScriptableObject
{
    [SerializeField] FormationShape.Shape shape;
    [SerializeField] public Cube cube;

    FormationShape _outputShape;
    public FormationShape GetShape 
    {
        get
        {

            switch (shape)
            {
                case FormationShape.Shape.Cube:
                    _outputShape = cube;
                    break;

                case FormationShape.Shape.Non:
                    Debug.Log("No Shape Seclected in ScriptableOnject");
                    break;
                default:
                    Debug.Log("No Shape Seclected in ScriptableOnject");
                    break;
            }

            _outputShape.shape = shape;
            return _outputShape;
        }
    }



}
