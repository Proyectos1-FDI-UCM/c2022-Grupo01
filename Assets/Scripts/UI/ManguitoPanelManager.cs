using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManguitoPanelManager : MonoBehaviour
{
    [SerializeField] private int _spaceBetweenManguitos, _xStart, _yStart, _maxManguitos;
    [SerializeField] private GameObject _manguitoPrefab;
    private static ManguitoPanelManager _instance;
    private GameObject obj2;
    public static ManguitoPanelManager Instance
    {
        get { return _instance; }
    }

    public void CreateManguitoSlot(int shields)
    {
        for (int i = 0; i < shields; i++)
        {
            var obj = Instantiate(_manguitoPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetManguitoSlotPosition(i);
        }
    }

    public Vector3 GetManguitoSlotPosition(int i)
    {
        return new Vector3(_xStart + (_spaceBetweenManguitos * (i % _maxManguitos)), _yStart, 0f);
    }

    private void Awake()
    {
        _instance = this;
    }
}
