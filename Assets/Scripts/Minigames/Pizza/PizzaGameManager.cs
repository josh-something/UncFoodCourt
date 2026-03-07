using UnityEngine;
using System.Collections.Generic;
using System.Globalization;
using TMPro;

public class PizzaGameManager : MonoBehaviour
{
    #region Variables

    [HideInInspector] public Grades finalGrade;
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
    [Space]
    [Header("Instruction Text")]
    [SerializeField] private TMP_Text instructionText;
    [SerializeField] [TextArea]private List<string> instructions;
    
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
        instructionText.text = instructions[(int)_pizzaState];
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
                instructionText.text = instructions[(int)_pizzaState];
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

    private void GradeScore()
    {
        if (_isGraded) return;
        
        _isGraded = true;
        var time = Time.time - _timestart;
        Grades grade;

        if (time <= perfectTime)
        {
            grade = Grades.Perfect;
        }
        else if (time <= goodTime)
        {
            grade = Grades.Good;
        }
        else
        {
            grade = Grades.Bad;
        }
        
        finalGrade = grade;
        Debug.Log("Time:" + time + ", Grade: " + grade);
        instructionText.text = "Finish \n Time: " + time.ToString("F2") + ", Grade: " + grade;
    }
}
