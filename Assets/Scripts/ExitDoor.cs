using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitDoor : InteractableBase
{
    [SerializeField] Image _imageToFade;

    public override void OnInteract()
    {
        base.OnInteract();

        StartCoroutine(FadeScreen(2));
    }

    private IEnumerator FadeScreen(float fadeTime)
    {
        float timeElapsed = 0;
        while (_imageToFade.color.a < 1)
        {
            _imageToFade.color = Color.Lerp(new Color(1, 1, 1, 0), Color.black, timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
