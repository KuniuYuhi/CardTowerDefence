using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Deck/DeckData")]
public class DeckData : ScriptableObject
{
    [Header("カードリスト")]
    public List<GameObject> cardPrefabsList;

    //クリックした時に見れる詳細




    /// <summary>
    /// デッキをシャッフルする
    /// </summary>
    public void DeckShuffle()
    {
        for(int i = 0; i < cardPrefabsList.Count; i++)
        {
            var temp = cardPrefabsList[i];
            //現在のカードの番号からリストの総数からランダムな番号を取得
            int randomIndex = Random.Range(i, cardPrefabsList.Count);
            //対象のカードとランダムに選ばれたカードと入れ替える
            cardPrefabsList[i] = cardPrefabsList[randomIndex];
            //ランダムに選ばれたカードに確保しておいたカードを代入
            cardPrefabsList[randomIndex] = temp;
        }

    }

    public GameObject GetCardInDeck(int num)
    {
        return cardPrefabsList[num];
    }


    /// <summary>
    /// デッキにカードを追加する
    /// </summary>
    /// <param name="cardPrefab"></param>
    public void AddCardInDeck(GameObject cardPrefab)
    {
        cardPrefabsList.Add(cardPrefab);

        
    }

    /// <summary>
    /// デッキからカードを削除する
    /// </summary>
    /// <param name="cardPrefab"></param>
    public void RemoveCardInDeck(GameObject cardPrefab)
    {

        bool da = cardPrefabsList.Remove(cardPrefab);


        if(da)
        {
            Debug.Log("削除できた。");
        }
        else
        {
            Debug.Log("削除できなかった。");
        }


    }

}
