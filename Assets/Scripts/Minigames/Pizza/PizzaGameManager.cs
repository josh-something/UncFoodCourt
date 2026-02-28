using UnityEngine;
using System.Collections.Generic;
using System.Globalization;
using TMPro;

public class PizzaGameManager : MonoBehaviour
{
    #region Variables
    
    private enum PizzaState
    {
        Cutting,
        Wrapping,
        Grading
    }

    [SerializeField] private TMP_Text text;
    [SerializeField] private float perfectTime;
    [SerializeField] private float goodTime;
    [SerializeField] private PizzaBoxing Pizza;
    [SerializeField] private Transform PizzaBox;
    [SerializeField] private List<PizzaCut> _pizzaCuts;
    
    private PizzaState _pizzaState = PizzaState.Cutting;
    private float _timestart;
    private int _cuts;
    private bool _wrapInitialized;
    private bool _isGraded;
    
    #endregion

    void Start()
    {
        _timestart = Time.time;
        _pizzaCuts[0].gameObject.SetActive(true);
    }

    void Update()
    {
        if(_isGraded) return;
        text.text = (Time.time - _timestart).ToString("F2");
        switch (_pizzaState)
        {
            case PizzaState.Cutting:
                CutPizzas();
                break;
            case PizzaState.Wrapping:
                BoxPizza();
                break;
            case PizzaState.Grading:
                GradeScore();
                break;
        }
    }
    
    private void CutPizzas()
    {
        // check if finished current cut
        if (_pizzaCuts[_cuts].isCutComplete)
        {
            _cuts++;
            // if last cut, move on
            if (_cuts == _pizzaCuts.Count)
            {
                _pizzaState = PizzaState.Wrapping;
            }
            // else, go to next cut
            else
            {
                _pizzaCuts[_cuts].gameObject.SetActive(true);
            }
        }
    }
    private void BoxPizza()
        {
            if (!_wrapInitialized)
            {
                PizzaBox.gameObject.SetActive(true);
                Pizza.enabled = true;
                Pizza.transform.position += Vector3.down * 2;
                Pizza.startingLocation = Pizza.transform.position;
                Pizza.pizzaBox = PizzaBox;
                _wrapInitialized =  true;
                return;
            }

            if (Pizza.isBoxed)
            {
                _pizzaState =  PizzaState.Grading;
            }
        }

    private Grades GradeScore()
    {
        if (!_isGraded)
        {
            _isGraded = true;
            var time = Time.time - _timestart;

            if (time <= perfectTime)
            {
                Debug.Log("Time:" + time + ", Grade: " + nameof(Grades.Perfect));
                return Grades.Perfect;
            }
            else if (time <= goodTime)
            {
                Debug.Log("Time:" + time + ", Grade: " + nameof(Grades.Good));
                return Grades.Good;
            }
            else
            {
                Debug.Log("Time:" + time + ", Grade: " + nameof(Grades.Bad));
                return Grades.Bad;
            }
        }
        return Grades.Bad;
    }
}
