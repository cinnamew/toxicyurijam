using System;
using System.Collections;
using DG.Tweening;
using Fungus;
using UnityEngine;
using UnityEngine.UI;

public class ImageSequence : MonoBehaviour
{
    [SerializeField] private Image[] _shots;
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private float _shotFadeDuration;
    [SerializeField] private float _startDelay;
    [SerializeField] private float _endDelay;
    [SerializeField] private CanvasGroup _menuButton;
    [SerializeField] private CanvasGroup _credits;
    
    public void Play()
    {
        StartCoroutine(SequenceCoroutine());
    }

    private IEnumerator SequenceCoroutine()
    {
        _credits.DOFade(1, 1);

        foreach (var shot in _shots)
        {
            shot.color = new Color(shot.color.r, shot.color.g, shot.color.b, 0);
            shot.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(_startDelay);


        for (var i = 0; i < _shots.Length; i++)
        {
            var duration= i == 0 ? 0 : _shotFadeDuration;
            
            _shots[i].gameObject.SetActive(true);
            _shots[i].DOFade(1, duration);
            
            yield return new WaitForSeconds(_timeBetweenShots);
            
            if(i >= 1)
                _credits.DOFade(0, 1);
        }
        yield return new WaitForSeconds(_endDelay);
        _menuButton.interactable = true;
        _menuButton.DOFade(1, 1);
        _credits.DOFade(0, 1);
    }
}
