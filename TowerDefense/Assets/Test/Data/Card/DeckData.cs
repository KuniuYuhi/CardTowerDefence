using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Deck/DeckData")]
public class DeckData : ScriptableObject
{
    [Header("�J�[�h���X�g")]
    public List<GameObject> cardPrefabsList;

    //�N���b�N�������Ɍ����ڍ�




    /// <summary>
    /// �f�b�L���V���b�t������
    /// </summary>
    public void DeckShuffle()
    {
        for(int i = 0; i < cardPrefabsList.Count; i++)
        {
            var temp = cardPrefabsList[i];
            //���݂̃J�[�h�̔ԍ����烊�X�g�̑������烉���_���Ȕԍ����擾
            int randomIndex = Random.Range(i, cardPrefabsList.Count);
            //�Ώۂ̃J�[�h�ƃ����_���ɑI�΂ꂽ�J�[�h�Ɠ���ւ���
            cardPrefabsList[i] = cardPrefabsList[randomIndex];
            //�����_���ɑI�΂ꂽ�J�[�h�Ɋm�ۂ��Ă������J�[�h����
            cardPrefabsList[randomIndex] = temp;
        }

    }

    public GameObject GetCardInDeck(int num)
    {
        return cardPrefabsList[num];
    }


    /// <summary>
    /// �f�b�L�ɃJ�[�h��ǉ�����
    /// </summary>
    /// <param name="cardPrefab"></param>
    public void AddCardInDeck(GameObject cardPrefab)
    {
        cardPrefabsList.Add(cardPrefab);

        
    }

    /// <summary>
    /// �f�b�L����J�[�h���폜����
    /// </summary>
    /// <param name="cardPrefab"></param>
    public void RemoveCardInDeck(GameObject cardPrefab)
    {

        bool da = cardPrefabsList.Remove(cardPrefab);


        if(da)
        {
            Debug.Log("�폜�ł����B");
        }
        else
        {
            Debug.Log("�폜�ł��Ȃ������B");
        }


    }

}
