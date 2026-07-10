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

    public void Play()
    {
        
        StartCoroutine(SequenceCoroutine());
    }

    private IEnumerator SequenceCoroutine()
    {
        var shots = transform.GetComponentsInChildren<Image>(true);

        foreach (var shot in shots)
        {
            shot.color = new Color(shot.color.r, shot.color.g, shot.color.b, 0);
            shot.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(_startDelay);


        for (var i = 0; i < shots.Length; i++)
        {
            var duration= i == 0 ? 0 : _shotFadeDuration;
            
            shots[i].gameObject.SetActive(true);
            shots[i].DOFade(1, duration);
            
            yield return new WaitForSeconds(_timeBetweenShots);
        }
    }
}
