using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SquintUIPanel : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField] private TextMeshProUGUI _tooltipText;
    [SerializeField] private Image _dot;
    [SerializeField] private TextMeshProUGUI _squintTutText;
    [SerializeField] private TextMeshProUGUI _listTutText;
    [SerializeField] private TextMeshProUGUI _objectiveText;
    
    [SerializeField] private AudioClip _checkpointSFX;

    private bool _isRunning;
    private bool _doOnce;

    private void Awake()
    {
        StartListTextFade(5, 2);
    }

    public void SetTooltip(string tooltip)
    {
        _tooltipText.SetText(tooltip);
    }

    public void UpdateProgressBar(float fillAmount)
    {
        _progressBar.fillAmount = fillAmount;
    }

    public void DotEnable()
    {
        _dot.color = Color.white;
    }

    public void DotDisable()
    {
        _dot.color = new Color32(120, 120, 120, 255);
    }

    public void ResetUI()
    {
        _progressBar.fillAmount = 0f;
        _tooltipText.SetText("");
    }

    public void ShowTutText()
    {
        _squintTutText.gameObject.SetActive(true);
    }

    public void StartListTextFade(float delay, float time)
    {
        if (!_isRunning && !_doOnce)
        {
            StopAllCoroutines();
            StartCoroutine(FadeText(_listTutText, delay, time));
        }
    }

    public void StartSquintTextFade(float delay, float time)
    {
        StopAllCoroutines();
        StartCoroutine(FadeText(_squintTutText, delay, time));
    }

    public void SetObjectiveText(string text)
    {
        _objectiveText.text = "Objective: " + text;
        AudioHelper.PlayClip2D(_checkpointSFX, 1);
    }

    private IEnumerator FadeText(TextMeshProUGUI text, float delay, float fadeTime)
    {
        yield return new WaitForSeconds(delay);
        _isRunning = true;

        float timeCount = 0;
        float alpha = text.color.a;
        while (timeCount < fadeTime)
		{
			timeCount += Time.deltaTime;
            alpha =  (1 - (timeCount / fadeTime));
            text.color = new Color(1,1,1,alpha);

			yield return null;
		}

        text.color = new Color(1, 1, 1, 0);

        _doOnce = true;
        _isRunning = false;
    }
}
