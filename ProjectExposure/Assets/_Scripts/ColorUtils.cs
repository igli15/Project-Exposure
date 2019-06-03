using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorUtils 
{
	public static Vector3 GetHSVOfAColor(Color color)
	{
		float hue = 0;
		float saturation = 0;
		float value = 0;
		
		Color.RGBToHSV(color, out hue, out saturation, out value);

		return new Vector3(hue,saturation,value);
	}
	
	public static float GetHueOfColor(Color color)
	{
		float hue = 0;
		float saturation = 0;
		float value = 0;
		
		Color.RGBToHSV(color, out hue, out saturation, out value);

		return hue;
	}
	
	public static bool CheckIfColorAreSimilar(Color c1, Color c2,float range)
	{
		
		float c1Hue = GetHueOfColor(c1) * 360;

		float c2Hue = GetHueOfColor(c2) * 360;

		if (c1Hue > 340) c1Hue = 0;
		if (c2Hue > 340) c1Hue = 0;  //Reset red.
		
		float hueDiff = Mathf.Abs(c1Hue - c2Hue);

		if (hueDiff <= range)
		{
			return true;
		}

		return false;
	}

	public static float GetOverlap(float hue, Color c1)
	{
		//TODO implement
		return 0;
	}

	public static float GetOverlap(Color c1, Color c2)
	{
		return Mathf.Clamp(c1.r * c2.r + c1.g * c2.g + c1.b * c2.b, 0, 1);
	}
}
