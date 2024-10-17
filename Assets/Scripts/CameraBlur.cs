using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraBlur : MonoBehaviour
{
	[SerializeField] private float _durationInSeconds = 0.5f;
	[Space]
	[SerializeField] private float _startFocalLength = 50f;
	[SerializeField] private float _targetFocalLength = 25f;
	[Space]
	[SerializeField] private float _startFocusDistance = 0.5f;
	[SerializeField] private float _targetFocusDistance = 0.2f;
	[Space]
	[SerializeField] private float _startAperture = 8f;
	[SerializeField] private float _targetAperture = 14f;
	[Space]
	[SerializeField] private float _startIntensity = 0.2f;
	[SerializeField] private float _targetIntensity = 1f;

	private PostProcessLayer v2_PostProcess;

	List<PostProcessVolume> volList = new List<PostProcessVolume>();

	private PostProcessVolume _vol;
	private PostProcessProfile _ppp;
	private DepthOfField _dph;
	private Vignette _vg;

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	private void Start()
	{
		v2_PostProcess = GetComponent<PostProcessLayer>();

		PostProcessManager.instance.GetActiveVolumes(v2_PostProcess, volList, true, true);

		//
		foreach (PostProcessVolume vol in volList)
		{
			_vol = vol;
			PostProcessProfile ppp = vol.profile;
			if (ppp)
			{
				_ppp = ppp;
				DepthOfField dph;
				if (ppp.TryGetSettings<DepthOfField>(out dph))
				{
					_dph = dph;
				}

				Vignette vg;
				if (ppp.TryGetSettings<Vignette>(out vg))
				{
					_vg = vg;
				}
			}
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			StopAllCoroutines();
			StartCoroutine(Focus(_durationInSeconds, false));
		}

		if (Input.GetKeyUp(KeyCode.Mouse1))
		{
			StopAllCoroutines();
			StartCoroutine(Focus(_durationInSeconds, true));
		}

		if (Input.GetKeyDown(KeyCode.Z))
		{
			_dph.focalLength.value = 50;
		}

		if (Input.GetKeyDown(KeyCode.X))
		{
			_dph.focalLength.value = 0;
		}
	}

	private IEnumerator Focus(float blurTime, bool unfocus)
	{
		float timeCount = 0;

		float currentFocalLength = _dph.focalLength.value;
		float currentAperture = _dph.aperture.value;
		float currentFocusDistance = _dph.focusDistance.value;

		float currentIntensity = _vg.intensity.value;

		while (timeCount < blurTime)
		{
			timeCount += Time.deltaTime;

			if (unfocus)
			{
				_dph.focalLength.value = (Mathf.Lerp(currentFocalLength, _startFocalLength, timeCount / blurTime));
				_dph.aperture.value = (Mathf.Lerp(currentAperture, _startAperture, timeCount / blurTime));
				_dph.focusDistance.value = (Mathf.Lerp(currentFocusDistance, _startFocusDistance, timeCount / blurTime));

				_vg.intensity.value = (Mathf.Lerp(currentIntensity, _startIntensity, timeCount / blurTime));
			}
			else
			{
				_dph.focalLength.value = (Mathf.Lerp(currentFocalLength, _targetFocalLength, timeCount / blurTime));
				_dph.aperture.value = (Mathf.Lerp(currentAperture, _targetAperture, timeCount / blurTime));
				_dph.focusDistance.value = (Mathf.Lerp(currentFocusDistance, _targetFocusDistance, timeCount / blurTime));

				_vg.intensity.value = (Mathf.Lerp(currentIntensity, _targetIntensity, timeCount / blurTime));
			}

			yield return null;
		}
	}
}  