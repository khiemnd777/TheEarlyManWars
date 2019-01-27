using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Currency", menuName = "Create Currency")]
public class Currency : ScriptableObject
{
	public float gold;
	public float diamond;
	public float experencePoint;

	public void GainExperencePoint (float amount)
	{
		experencePoint += amount;
	}

	public void PurchaseByExperencePoint (float amount, System.Action then)
	{
		if (amount > experencePoint) return;
		experencePoint -= amount;
		if (then != null) then ();
	}

	public void GainDiamond (float amount)
	{
		diamond += amount;
	}

	public void PurchaseByDiamond (float amount, System.Action then)
	{
		if (amount > diamond) return;
		diamond -= amount;
		if (then != null) then ();
	}

	public void GainGold (float amount)
	{
		gold += amount;
	}

	public void PurchaseByGold (float amount, System.Action then)
	{
		if (amount > gold) return;
		gold -= amount;
		if (then != null) then ();
	}
}
