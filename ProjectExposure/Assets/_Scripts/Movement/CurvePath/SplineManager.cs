using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineManager : MonoBehaviour
{
    [SerializeField]
    public Texture TextureDisplay;

    public static bool SubscribedToUpdate=false;
    public List<BezierCurve> splines;



    public void Start()
    {
      
    }

    public void Reset()
    {
        splines = new List<BezierCurve>();
        
    }

    public void UpdateSplineList()
    {
 
        splines = new List<BezierCurve>();
        BezierCurve[] splineArray= GameObject.FindObjectsOfType<BezierCurve>();
        foreach (BezierCurve bc in splineArray)
        {
            splines.Add(bc);
        }

    }
}
