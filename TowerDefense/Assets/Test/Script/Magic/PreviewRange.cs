using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PreviewRange : MonoBehaviour
{
    [SerializeField, Header("魔法カードのステータス")]
    Status_Magic m_magicStatus;


    /// <summary>
    /// デカールのサイズを設定
    /// </summary>
    /// <param name="size"></param>
    public void SetDecalSize(Vector3 size)
    {
        DecalProjector decalProjector = GetComponent<DecalProjector>();

        decalProjector.size = size;
    }
    /// <summary>
    /// デカールのサイズを設定
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public void SetDecalSize(float x,float y,float z)
    {
        DecalProjector decalProjector = GetComponent<DecalProjector>();

        decalProjector.size = new Vector3(x, y, z);
    }

    private void Awake()
    {
        m_magicStatus.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        //半径を取得
        float radius = m_magicStatus.GetKeyValuePairs().GetValueOrDefault("半径");


        DecalProjector decalProjector = GetComponent<DecalProjector>();

        //直径
        decalProjector.size = new Vector3(radius*2.0f, radius*2.0f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
