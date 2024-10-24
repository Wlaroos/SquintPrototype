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
        _dot.color = new Color32(200, 90, 90, 255);
    }

    public void DotDisable()
    {
        _dot.color = new Color32(45, 45, 45, 255);
    }

    public void ResetUI()
    {
        _progressBar.fillAmount = 0f;
        _tooltipText.SetText("");
    }
}
