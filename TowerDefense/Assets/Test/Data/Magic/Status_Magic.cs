using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StatusData_Magic")]

public class Status_Magic : ScriptableObject
{
    [SerializeField, Header("������")]
    string explanatoryNote;

    public string GetExplanatoryNote()
    {
        return explanatoryNote;
    }

}
