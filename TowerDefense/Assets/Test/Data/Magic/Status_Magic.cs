using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/StatusData_Magic")]

public class Status_Magic : ScriptableObject
{
    [SerializeField, Header("説明文")]
    string explanatoryNote;

    [SerializeField,Header("必要なステータス")]
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
    /// ステータスを管理するリストを取得
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, float> GetKeyValuePairs()
    {
        return dictionary;
    }

}
