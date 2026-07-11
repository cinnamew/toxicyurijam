using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundFader : MonoBehaviour
{
   private SpriteRenderer _defaultSpriteRenderer;
   [SerializeField] private float _duration = 0.5f;
   private SpriteRenderer _childSpriteRenderer;

   private void Start()
   {
      _defaultSpriteRenderer = GetComponent<SpriteRenderer>();
      _childSpriteRenderer = new GameObject().AddComponent<SpriteRenderer>();
      
      _childSpriteRenderer.transform.SetParent(transform);
      _childSpriteRenderer.transform.localPosition = Vector3.zero;
      _childSpriteRenderer.sortingOrder = _defaultSpriteRenderer.sortingOrder;
      _childSpriteRenderer.sharedMaterial= _defaultSpriteRenderer.sharedMaterial;
   }

   public void FadeBackground(Sprite sprite)
   {
      _childSpriteRenderer.sprite = sprite;
      _childSpriteRenderer.color = new Color(_childSpriteRenderer.color.r, _childSpriteRenderer.color.g, _childSpriteRenderer.color.b, 0f);
      _defaultSpriteRenderer.sortingOrder = -1;

      _childSpriteRenderer.DOFade(1, _duration).OnComplete(() =>
      {
         _defaultSpriteRenderer.sprite = _childSpriteRenderer.sprite;
         _childSpriteRenderer.color = new Color(_childSpriteRenderer.color.r, _childSpriteRenderer.color.g, _childSpriteRenderer.color.b, 0f);
         _defaultSpriteRenderer.sortingOrder = 0;
      });
   }
   
}
