using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CanvasManager : MonoBehaviour {
    private static CanvasManager instance;
    public static CanvasManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<CanvasManager>();
            }
            return instance;
        }
    }


    public Text scoreText;
    public float animDuration;

   


    public void OpenWindow(GameObject window)
    {
        Sequence sequence = DOTween.Sequence();
        window.SetActive(true);
       var canvasGroup = window.GetComponent<CanvasGroup>();
        if(canvasGroup == null)
        {
            canvasGroup = window.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0;
        canvasGroup.transform.localScale = Vector3.zero;
        sequence.Append(canvasGroup.DOFade(1, animDuration))
            .Join(canvasGroup.transform.DOScale(1, animDuration).SetEase(Ease.InBounce));
    }

    public void CloseWindow(GameObject window)
    {
        window.SetActive(true);
        Sequence sequence = DOTween.Sequence();
        var canvasGroup = window.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = window.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 1;
        canvasGroup.transform.localScale = Vector3.zero;
        sequence.Append(canvasGroup.DOFade(0, animDuration))
            .Join(canvasGroup.transform.DOScale(0, animDuration).SetEase(Ease.InBounce))
            .AppendCallback(() => window.SetActive(false));
    }



    public void SetScoreText(int value)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(scoreText.DOText(string.Format("Score: {0}",value), animDuration, true, ScrambleMode.Numerals));  
    }


	
}
