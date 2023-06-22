using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OpenPanelButton : MonoBehaviour
{
    [SerializeField] private GameObject _panelObject;

    private Button _button;
    private Button Button => _button == null ? _button = GetComponent<Button>() : _button;

    private void OnEnable() => Button.onClick.AddListener(OnClick);
    private void OnDisable() => Button.onClick.RemoveListener(OnClick);

    private void OnClick()
    {
        AudioManager.Instance.PlayNormalSound(0);
        _panelObject.SetActive(true);
        _panelObject.transform.localScale = Vector3.zero;
        _panelObject.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.Linear);
    }
}
