using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestAnimationHandlerConfig", menuName = "Configs/QuestAnimationHandler")]
public class QuestAnimationHandlerConfig : ScriptableObject
{
    [SerializeField] private Vector3 _textMovementOffset;
    [SerializeField] private Color _baseColor;
    [SerializeField] private float _animationDuration;
    
    public Vector3 TextMovementOffset => _textMovementOffset;
    public Color BaseColor => _baseColor;
    public float AnimationDuration => _animationDuration;

    private void OnValidate()
    {
        if(_animationDuration < 0)
            throw new ArgumentOutOfRangeException(nameof(_animationDuration), "Animation duration cannot be below zero");
    }
}
