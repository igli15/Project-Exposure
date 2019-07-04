using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private RectTransform shootingRect;

	public Touch[] GetAllTouches()
    {
        return Input.touches;
    }

    public int GetIndexTheFirstTouchOnShootingArea()
    {
        float minX = shootingRect.anchorMin.x;
        float maxX = shootingRect.anchorMax.x;

        float minY = shootingRect.anchorMin.y;
        float maxY = shootingRect.anchorMax.y;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector2 touchViewPos = Camera.main.ScreenToViewportPoint(Input.GetTouch(i).position);
            if (touchViewPos.x > minX && touchViewPos.x < maxX && touchViewPos.y > minY && touchViewPos.y < maxY)
            {
                return i;
            }
        }

        return -1;
    }
}
