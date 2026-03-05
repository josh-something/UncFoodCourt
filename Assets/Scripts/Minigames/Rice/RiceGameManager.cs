using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class RiceGameManager : MonoBehaviour
{
    /*Pseudocode
     Game starts with an unmixed rice bowl.
     
     Tap(click) and hold to start mixing
     delta mouse (distance pointer moved since last frame) += Amount mixed
     if (amount mixed >= threshold)
     {
        swap sprites to 25%/50%/75% mixed
     }
     if (amount mixed >= final threshold)
     {
        swap to 100% mixed sprite
        end game
     }
     TODO Add Grading
     */
    /*
     Not Mixed = 0%
     Light = 25% mixed
     Half = 50% mixed
     Almost = 75% mixed
     Mixed = 100%
     */
    enum MixState
    {
        NotMixed,
        LightlyMixed,
        HalfMixed,
        AlmostMixed,
        Mixed
    }

    #region  Variables

    [InspectorName("Particle System")]
    public ParticleSystem ps;
    public UnityEvent<float> onMixProgressReached;
    
    [SerializeField] private float mixingMult = 1;
    [SerializeField] private float targetMixProgress = 1600;
    
    private InputAction _click;
    private InputAction _drag;

    private float _particleCD;
    private float _mixProgress;
    private bool _isDragging;
    private MixState _state = MixState.NotMixed;
    
    
    #endregion

    private void Awake()
    {
        _click = InputSystem.actions.FindAction("Attack");
        _drag = InputSystem.actions.FindAction("Look");
    }

    private void OnEnable()
    {
        _click.started += ClickOnstarted;
        _click.canceled += ClickOncanceled;
    }

    private void OnDisable()
    {
        _click.started -= ClickOnstarted;
        _click.canceled -= ClickOncanceled;
    }
    
    void Update()
    {
        if (_state == MixState.Mixed) return;
        if (!_isDragging) return;
        IncreaseMix(out var mixValue);
        GenerateParticles(mixValue);
        UpdateMixProgress();
    }
    
    private void IncreaseMix(out float mixValue)
    {
        var delta = _drag.ReadValue<Vector2>().magnitude;
        mixValue = delta * mixingMult * Time.deltaTime;
        _mixProgress += mixValue;
    }

    private void GenerateParticles(float mixValue)
    {
        _particleCD += mixValue;
        if (_particleCD >= 10)
        {
            ps.Play();
            _particleCD -= 10;
        }
    }

    void UpdateMixProgress()
    {
        var ratio = _mixProgress / targetMixProgress;
        //Debug.Log(ratio);
        // Checking to pass...
        switch (_state)
        {
            case MixState.NotMixed:
                // ...25% threshold
                if (ratio >.25) 
                {
                    onMixProgressReached?.Invoke(25);
                    _state = MixState.LightlyMixed;
                }
                break;
            
            case MixState.LightlyMixed:
                // ...50%
                if (ratio > .50)
                {
                    onMixProgressReached?.Invoke(50);
                    _state = MixState.HalfMixed;
                }
                break;
            
            case MixState.HalfMixed:
                //... 75%
                if (ratio > .75)
                {
                    onMixProgressReached?.Invoke(75);
                    _state = MixState.AlmostMixed;
                }
                break;
            
            case MixState.AlmostMixed:
                //... 100%
                if (ratio > 1)
                {
                    onMixProgressReached?.Invoke(100);
                    _state = MixState.Mixed;
                    GradeScore();
                }
                break;
            case MixState.Mixed:
                Debug.LogWarning("This shouldn't Happen");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void GradeScore()
    {
        
        
    }

    private void ClickOnstarted(InputAction.CallbackContext obj)
    {
        Debug.Log("ClickOnstarted");
        _isDragging = true;
    }
    
    private void ClickOncanceled(InputAction.CallbackContext obj)
    {
        Debug.Log("ClickOncanceled");
        _isDragging = false;
    }
}
