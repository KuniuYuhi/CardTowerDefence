using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRaycast : MonoBehaviour
{
    public Camera mainCamera;  // �g�p����J����
    public LayerMask layerMask; // ����̃��C���[


    public CardData m_cardData;


    public GameObject createObj;

    void Update()
    {
        ClickCast();
    }


    //�h���b�O���h���b�v�ŉr��



    /// <summary>
    /// �N���b�N�ŉr��
    /// </summary>
    void ClickCast()
    {
        if (Input.GetMouseButtonDown(0)) // �}�E�X�̍��N���b�N���`�F�b�N
        {
            // �N���b�N�����X�N���[�����W���擾
            Vector3 screenPosition = Input.mousePosition;

            // �X�N���[�����W�����C�ɕϊ�
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);

            // ���C�L���X�g�̌��ʂ��i�[���邽�߂�RaycastHit
            RaycastHit hit;

            // ����̃��C���[�ɑ΂��ă��C�L���X�g���s��
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                // ���C���[�ɓ��������ꍇ�̏���
                Debug.Log("Hit " + hit.collider.name + " at " + hit.point);

                Instantiate(createObj, hit.point, Quaternion.identity);
            }
            else
            {
                // ���C���[�ɓ�����Ȃ������ꍇ�̏���
                Debug.Log("No hit");
            }
        }
    }

}
