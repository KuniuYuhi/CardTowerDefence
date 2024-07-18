using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Deck/CardData")]
public class CardData : ScriptableObject
{
    [SerializeField, Header("�J�[�h�̃R�X�g")]
    int castCost;

    [SerializeField, Header("�J�[�h�̃I�u�W�F�N�g")]
    GameObject objectPrefab;

    [SerializeField, Header("���f���̃v���r���[")]
    GameObject previewModel;

    [SerializeField, Header("�J�[�h��")]
    string cardName;

    [SerializeField, Header("�J�[�h�̃X�e�[�^�X�B�K�������̂��Z�b�g")]
    Status_Character unitStatus;
    [SerializeField]
    Status_Building buildingStatus;
    [SerializeField]
    Status_Magic magicStatus;

    /// <summary>
    /// �J�[�h�̃^�C�v
    /// </summary>
    public enum EnCardType
    {
        enCardType_Unit,
        enCardType_Building,
        enCardType_Magic
    };

    [SerializeField, Header("�J�[�h�̃^�C�v")]
    EnCardType enCardType;


    /// <summary>
    /// �r���R�X�g���擾
    /// </summary>
    /// <returns></returns>
    public int GetCastCost()
    {
        return castCost;
    }

    /// <summary>
    /// ��������v���t�@�u���擾
    /// </summary>
    /// <returns></returns>
    public GameObject GetObjectPrefab()
    {
        return objectPrefab;
    }

    /// <summary>
    /// �v���r���[���f���̎擾
    /// </summary>
    /// <returns></returns>
    public GameObject GetPreviewModel()
    {
        return previewModel;
    }

    /// <summary>
    /// �J�[�h�̖��O���擾
    /// </summary>
    /// <returns></returns>
    public string GetCardName()
    {
        return cardName;
    }

    /// <summary>
    /// �J�[�h�̃^�C�v���擾
    /// </summary>
    /// <returns></returns>
    public EnCardType GetCardType()
    {
        return enCardType;
    }

    /// <summary>
    /// ���j�b�g�̃X�e�[�^�X���擾
    /// </summary>
    /// <returns></returns>
    public Status_Character GetUnitStatus()
    {
        return unitStatus;
    }
    /// <summary>
    /// �����̃X�e�[�^�X���擾
    /// </summary>
    /// <returns></returns>
    public Status_Building GetBuildingStatus()
    {
        return buildingStatus;
    }
    /// <summary>
    /// ���@�̃X�e�[�^�X���擾
    /// </summary>
    /// <returns></returns>
    public Status_Magic GetMagicStatus()
    {
        return magicStatus;
    }

}
