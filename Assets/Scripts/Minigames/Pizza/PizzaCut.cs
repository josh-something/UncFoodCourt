using System;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCut : MonoBehaviour
{
    /*TODO panel functionality*/
    
    #region Variables
    public bool isCutComplete {get; private set;}

    [SerializeField] List<Collider2D>  _colliders;
    
    private List<SpriteRenderer> _spriteRenderers;
    private Camera _camera;
    private SpriteRenderer _sr;
    private int _cutOrder = 0;
    #endregion

    private void Start()
    {
        _camera = Camera.main;
        _sr = GetComponent<SpriteRenderer>();
        _spriteRenderers = new List<SpriteRenderer>();
        for (int i = 0; i < _colliders.Count; i++)
        {
           _spriteRenderers.Add(_colliders[i].GetComponent<SpriteRenderer>());
        }
    }

    private void Update()
    {
        if (isCutComplete) return;
        CheckCut();
    }
    
    private void CheckCut()
    {
        Ray r = _camera.ScreenPointToRay(Input.mousePosition);
        var hit = Physics2D.Raycast(r.origin, r.direction);
        
        if (hit)
        {
            //Checks to see if the pointer is cutting in the right order and direction
            var isInOrder = hit.collider ==  _colliders[_cutOrder];
            var inCutDirection = (Vector3.Dot(-transform.up, Input.mousePositionDelta) > .8);
            
            if (isInOrder && inCutDirection)
            {
                _spriteRenderers[_cutOrder].enabled = true;
                _cutOrder++;
                if (_cutOrder >= _colliders.Count)
                {
                    isCutComplete = true;
                    _sr.enabled = false;
                }
            }
        }
    }
}
