using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManguitoPanelManager : MonoBehaviour
{
    [SerializeField] private int _spaceBetweenManguitos, _xStart, _yStart, _maxManguitos;
    [SerializeField] private GameObject _manguitoPrefab;
    private List<GameObject> _manguitoList = new List<GameObject>();
    private static ManguitoPanelManager _instance;
    public static ManguitoPanelManager Instance
    {
        get { return _instance; }
    }

    public void CreateManguitoSlot(int shieldsToCreate)
    {
        for (int i = 0; i < shieldsToCreate; i++)
        {
            var obj = Instantiate(_manguitoPrefab, Vector3.zero, Quaternion.identity, transform);
            _manguitoList.Add(obj);
            obj.GetComponent<RectTransform>().localPosition = GetManguitoSlotPosition(_manguitoList.Count - 1);
        }
    }

    public void RemoveManguitoSlot(int numberToRemove)
    {
        numberToRemove = Mathf.Abs(numberToRemove);

        for(int i = 0; i < numberToRemove; i++)
        {
            int index = _manguitoList.Count - 1;
            Destroy(_manguitoList[index].gameObject);
            Debug.Log("eliminado manguito");
            _manguitoList.RemoveAt(index);
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
