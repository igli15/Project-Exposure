using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    [SerializeField]
    public SplineManager splineManager; 

    public enum BezierControlPointMode
    {
        Free,
        Aligned,
        Mirrored
    }

    [SerializeField]
    private BezierControlPointMode[] m_modes;

    [SerializeField]
    private Vector3[] m_points;
    [SerializeField]
    private float[] m_lengths;

    public int ControlPointCount { get { return m_points.Length;} }
    public int CurveCount { get { return (m_points.Length - 1) / 3; } }
    public bool drawInEditor=false;
    public float TotalLength = 0;
    [SerializeField]
    public float Duration = 30;

    public void Reset()
    {
        splineManager = GameObject.FindObjectOfType<SplineManager>();

        //Default values
        m_points = new Vector3[] {
            new Vector3(1f, 0f, 0f),
            new Vector3(2f, 0f, 0f),
            new Vector3(3f, 0f, 0f),
            new Vector3(4f, 0f, 0f)
        };
        m_modes = new BezierControlPointMode[] {
            BezierControlPointMode.Aligned,
            BezierControlPointMode.Aligned
        };
    }

    public void Start()
    {
        CalculateTotalLength();
    }

    public Vector3 GetControlPoint(int index)
    {
        return m_points[index];
    }

    public Vector3 GetDirection(float t)
    {
        return GetVelocity(t).normalized;
    }

    public Vector3 GetPoint(float t)
    {
        int i;
        // if t==1 then we need to get last curve which starts from lastPoint - 4
        if (t >= 1f)
        {
            t = 1f;
            i = m_points.Length - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * CurveCount;  // 0<t<curveCount
            i = (int)t;
            t -= i;
            i *= 3;
        }
        return transform.TransformPoint(Bezier.GetPoint(
            m_points[i], m_points[i + 1], m_points[i + 2], m_points[i + 3], t));
    }

    public int GetCurrentIndex(float t)
    {
        int i = 0;
        if (t >= 1f)
        {
            i = m_points.Length - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * CurveCount;  // 0<t<curveCount
            i = (int)t;
        }
        return i;
    }

    public float GetDeltaProgress(float t, float speed)
    {
        float init_t=t;
        int i = 0;
        if (t >= 1f)
        {
            t = 1f;
            i = m_points.Length - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * CurveCount;  // 0<t<curveCount
            i = (int)t;
            t -= i;
            i *= 3;
        }
        float length = CalculateLength(i);
        if (speed > length) speed = length;

        float deltaProgress = speed / length;
        int nextIndex = GetCurrentIndex(init_t + deltaProgress);


        //Debug.Log("REMAINING_LENGTH: " + (1 - t) * length+" /"+speed);
        
        if (i/3!=nextIndex)
        {
            Debug.Log("Current Index: " + i / 3 + ", Next Index: " + nextIndex);
            float remainingLength = (1-t)*length;
            float nextLength = CalculateLength(nextIndex);
            float extraDelta = (speed - remainingLength) / nextLength;
            //CLEAR AND FIX IT
            Debug.Log("Delta progress: " + deltaProgress + ", t: " + t);
            //Debug.Log("progress: " + deltaProgress * (1 - t) + " + " + extraDelta);
            return speed/nextLength;
        }
        return deltaProgress;
    }

    public float GetLength(float t)
    {
        int i;
        if (t >= 1f)
        {
            t = 1f;
            i = m_points.Length - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * CurveCount;  // 0<t<curveCount
            i = (int)t;
            t -= i;
            i *= 3;
        }

        return CalculateLength(i);
    }

    public Vector3 GetVelocity(float t)
    {
        int i;
        if (t >= 1f)
        {
            t = 1f;
            i = m_points.Length - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * CurveCount;
            i = (int)t;
            t -= i;
            i *= 3;
        }
        return transform.TransformPoint(Bezier.GetFirstDerivative(
            m_points[i], m_points[i + 1], m_points[i + 2], m_points[i + 3], t)) - transform.position;
    }

    public BezierControlPointMode GetControlPointMode(int index)
    {
        return m_modes[(index + 1) / 3];
    }

    public void SetControlPoint(int index, Vector3 point)
    {
        if (index % 3 == 0)
        {
            Vector3 delta = point - m_points[index];
            if (index > 0)
            {
                m_points[index - 1] += delta;
            }
            if (index + 1 < m_points.Length)
            {
                m_points[index + 1] += delta;
            }
        }
        m_points[index] = point;
        EnforceMode(index);
    }

    public void SetRawPoint(int index, Vector3 point)
    {
        m_points[index] = point;
        EnforceMode(index);
    }

    public void SetControlPointMode(int index, BezierControlPointMode mode)
    {
        EnforceMode(index);
        m_modes[(index + 1) / 3] = mode;
    }

    public void CalculateTotalLength()
    {
        m_lengths = new float[CurveCount];
        TotalLength = 0;
        for (int i = 0; i < CurveCount; i++)
        {
            float length= CalculateLength(i);
            m_lengths[i] = TotalLength;
            TotalLength += length;
        }

    }

    public float CalculateLength(int index)
    {
        float depthLevel = 50;
        float totalLength = 0;
        Vector3 lastPoint= Bezier.GetPoint(m_points[index], m_points[index + 1], m_points[index + 2], m_points[index + 3], 0);
        for (int i = 1; i < depthLevel; i++)
        {
            Vector3 currentPoint=Bezier.GetPoint(m_points[index], m_points[index + 1], m_points[index + 2], m_points[index + 3], (float)i/depthLevel);
            totalLength += (currentPoint - lastPoint).magnitude;
            lastPoint = currentPoint;
        }
        //Debug.Log("TotalLength of curve " + index + " is " + totalLength);

        return totalLength;
    }




    public void AddCurve()
    {
        Vector3 point = m_points[m_points.Length - 1];
        Array.Resize(ref m_points, m_points.Length + 3);

        point.x += 6f;
        m_points[m_points.Length - 3] = point;
        point.x += 3f;
        m_points[m_points.Length - 2] = point;
        point.x += 6f;
        m_points[m_points.Length - 1] = point;

        Array.Resize(ref m_modes, m_modes.Length + 1);
        m_modes[m_modes.Length - 1] = m_modes[m_modes.Length - 2];
        EnforceMode(m_points.Length - 4);
    }

    private void EnforceMode(int index)
    {
        int modeIndex = (index + 1) / 3;
        BezierControlPointMode mode = m_modes[modeIndex];
        if (mode == BezierControlPointMode.Free || modeIndex == 0 || modeIndex == m_modes.Length - 1)
        {
            return;
        }
        //Getting neighbour constraints 
        int middleIndex = modeIndex * 3;
        int fixedIndex, enforcedIndex;
        if (index <= middleIndex)
        {
            fixedIndex = middleIndex - 1;
            enforcedIndex = middleIndex + 1;
        }
        else
        {
            fixedIndex = middleIndex + 1;
            enforcedIndex = middleIndex - 1;
        }

        Vector3 middle = m_points[middleIndex];
        Vector3 enforcedTangent = middle - m_points[fixedIndex];
        if (mode == BezierControlPointMode.Aligned)
        {
            enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, m_points[enforcedIndex]);
        }
        m_points[enforcedIndex] = middle + enforcedTangent;
    }
}
