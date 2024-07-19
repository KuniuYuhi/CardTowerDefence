using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntrusionUnitChecker : MonoBehaviour
{
    [SerializeField, Header("�`�F�b�N���������C���[")]
    LayerMask m_includeLayer;

    [SerializeField, Header("�`�F�b�N�������Ȃ����C���[")]
    LayerMask m_excuteLayer;


    // Start is called before the first frame update
    void Start()
    {
        BoxCollider collision = GetComponent<BoxCollider>();

        collision.includeLayers = m_includeLayer;
        collision.excludeLayers = m_excuteLayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        //�`�F�b�N�G���A�ɓ�������e�I�u�W�F�N�g�̃Q�[���I�u�W�F�N�g��ݒ�

        CharacterControll charaCon = other.GetComponent<CharacterControll>();

        if (charaCon == null) return;

        charaCon.SetTargetObject(transform.parent.gameObject);

    }


    private void OnTriggerExit(Collider other)
    {
        //�`�F�b�N�G���A�𔲂�����A�ǂ�������I�u�W�F�N�g�����Z�b�g

        CharacterControll charaCon = other.GetComponent<CharacterControll>();

        if (charaCon == null) return;

        charaCon.SetTargetObject(null);
    }

}
