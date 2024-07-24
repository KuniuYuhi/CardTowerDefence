using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/StatusData_Magic")]

public class Status_Magic : ScriptableObject
{
    [SerializeField, Header("������")]
    string explanatoryNote;

    [SerializeField,Header("�K�v�ȃX�e�[�^�X")]
    public List<MagicKeyValuePair> keyValuePairs;

    private Dictionary<string, float> dictionary;


    /// <summary>
    /// 
    /// </summary>
    public void Initialize()
    {
        dictionary = new Dictionary<string, float>();
        foreach (MagicKeyValuePair kvp in keyValuePairs)
        {
            dictionary.Add(kvp.m_keyName, kvp.m_keyValue);
        }
    }



    public string GetExplanatoryNote()
    {
        return explanatoryNote;
    }

    /// <summary>
    /// �X�e�[�^�X���Ǘ����郊�X�g���擾
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, float> GetKeyValuePairs()
    {
        return dictionary;
    }

}
