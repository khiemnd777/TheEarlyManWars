using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTabUI : MonoBehaviour
{
	public RectTransform technologyPanel;
	public RectTransform towerPanel;
	public RectTransform characterPanel;

	public void TechnologyTab ()
	{
		technologyPanel.gameObject.SetActive (true);
		towerPanel.gameObject.SetActive (false);
		characterPanel.gameObject.SetActive (false);
	}

	public void TowerTab ()
	{
		technologyPanel.gameObject.SetActive (false);
		towerPanel.gameObject.SetActive (true);
		characterPanel.gameObject.SetActive (false);
	}

	public void CharacterTab ()
	{
		technologyPanel.gameObject.SetActive (false);
		towerPanel.gameObject.SetActive (false);
		characterPanel.gameObject.SetActive (true);
	}
}
