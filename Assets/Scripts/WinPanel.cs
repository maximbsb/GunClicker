using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private GunCellSpawner gunCellSpawner;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private float waitTime = 1f;
    
    private void Awake()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        gunCellSpawner.OnWin += Show;
    }

    private void Show()
    {
        StartCoroutine(ShowCoroutine());
    }
    
    private IEnumerator ShowCoroutine()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        
        yield return new WaitForSeconds(waitTime);
        
        float t = 0;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, t / fadeTime);
            yield return null;
        }
        
    }
}
