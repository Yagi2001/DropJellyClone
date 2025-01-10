using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static Action MoveMade;
    [SerializeField]
    private LevelData _levelData;
    [SerializeField]
    private TMP_Text movesText;
    private int _moveCount;
    private int _goalCount;
    private void OnEnable()
    {
        _moveCount = _levelData.maxMoves;
        _goalCount = _levelData.goal;
        MoveMade += UpdateMoveCount;
        movesText.text = _moveCount.ToString();
    }

    private void OnDisable()
    {
        MoveMade -= UpdateMoveCount;
    }

    private void UpdateMoveCount()
    {
        _moveCount--;
        movesText.text = _moveCount.ToString();
    }
}
