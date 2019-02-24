using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
	Settings _settings;
	Camera _camera;

	void Awake ()
	{
		_settings = FindObjectOfType<Settings> ();
		_camera = GetComponent<Camera>();
	}

	void Start ()
	{
		_camera.orthographicSize = _settings.defaultScreenSize;
	}
}
