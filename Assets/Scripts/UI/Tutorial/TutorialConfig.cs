using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTutorialConfig", menuName = "Configs/UI/Tutorial")]
public class TutorialConfig : ScriptableObject
{
    [SerializeField] private float _tutorialMinSize = 0;
    [SerializeField] private float _tutorialMaxSize = 7f;
    [SerializeField] private float _tutorialAnimationDuration = 1f;

    public float TutorialMinSize => _tutorialMinSize;
    public float TutorialMaxSize => _tutorialMaxSize;
    public float TutorialAnimationDuration => _tutorialAnimationDuration;

    private void OnValidate()
    {
        if (_tutorialAnimationDuration < 0)
            throw new ArgumentOutOfRangeException(nameof(_tutorialAnimationDuration), "Tutorial animation duration cannot be below zero");

        if (_tutorialMaxSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(_tutorialMaxSize), "Tutorial animation max size must be greater than zero");
    }
}
