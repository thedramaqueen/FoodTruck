using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPunch : MonoBehaviour
{
    private string _punchTweenID;

    private Button _button;
    private Button Button => _button == null ? _button = GetComponent<Button>() : _button;

    private void OnEnable()
    {
        Button.onClick.AddListener(PunchTween);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(PunchTween);
    }

    private void Awake()
    {
        _punchTweenID = GetInstanceID() + "punchTween";
    }

    private void PunchTween()
    {
        DOTween.Complete(_punchTweenID);
        transform.DOPunchScale(Vector3.one * 0.075f, 0.25f, 1).SetId(_punchTweenID).SetEase(Ease.Linear);
    }
}
