using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GroceryList : MonoBehaviour
{
	[SerializeField] private GameObject _boxMain;
	[SerializeField] private GameObject _boxDown;
	[SerializeField] private GameObject _boxUp;
	[Space]
	[SerializeField] private float _durationInSeconds = 0.5f;
	
	private Vector3 _startPos;
	private Vector3 _targetPos;
	private Vector3 _startRot;
	private Vector3 _targetRot;

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	private void Awake()
	{
		_startPos = _boxDown.transform.localPosition;
		_startRot = _boxDown.transform.localRotation.eulerAngles;
		_targetPos = _boxUp.transform.localPosition;
		_targetRot = _boxUp.transform.localRotation.eulerAngles;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			StopAllCoroutines();
			StartCoroutine(Focus(_durationInSeconds, false));
		}

		if (Input.GetKeyUp(KeyCode.Q))
		{
			StopAllCoroutines();
			StartCoroutine(Focus(_durationInSeconds, true));
		}
	}

	private IEnumerator Focus(float blurTime, bool unfocus)
	{
		float timeCount = 0;

		Vector3 currentPos = _boxMain.transform.localPosition;
		Vector3 currentRot = _boxMain.transform.localRotation.eulerAngles;

		while (timeCount < blurTime)
		{
			timeCount += Time.deltaTime;

			if (unfocus)
			{
				_boxMain.transform.localPosition = Vector3.Lerp(currentPos, _startPos, timeCount / blurTime);
				_boxMain.transform.localRotation = Quaternion.Euler(Vector3.Lerp(currentRot, _startRot, timeCount / blurTime));
			}
			else
			{
				_boxMain.transform.localPosition = Vector3.Lerp(currentPos, _targetPos, timeCount / blurTime);
				_boxMain.transform.localRotation = Quaternion.Euler(Vector3.Lerp(currentRot, _targetRot, timeCount / blurTime));
			}

			yield return null;
		}
	}
}