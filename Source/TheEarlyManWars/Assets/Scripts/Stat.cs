using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Stat
{
	public int baseValue;
	public int growthPercent;

	List<int> modifiers = new List<int>();

	public int GetValue() {
		var finalValue = baseValue;
		finalValue += modifiers.Sum();
		return finalValue;
	}

	public void GrowUp()
	{
		baseValue *= (1 + growthPercent);
	}

	public void AddModifier(int modifier){
		if(modifier != 0)
			modifiers.Add(modifier);
	}

	public void RemoveModifier(int modifier){
		if(modifier != 0)
			modifiers.Remove(modifier);
	}

	public void ClearModifiers()
	{
		modifiers.Clear();
	}
}
