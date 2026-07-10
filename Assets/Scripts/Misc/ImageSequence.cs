using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ImageSequence : MonoBehaviour
{
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private float _shotFadeDuration;
    [SerializeField] private float _startDelay;
    
    private IEnumerator Start()
    {
        var shots = transform.GetComponentsInChildren<Image>();

        foreach (var shot in shots)
        {
            shot.color = new Color(shot.color.r, shot.color.g, shot.color.b, 0);
            shot.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(_startDelay);
        
        foreach (var shot in shots)
        {
            shot.gameObject.SetActive(true);
            shot.DOFade(1, _shotFadeDuration);
            yield return new WaitForSeconds(_timeBetweenShots);
        }
    }
}
