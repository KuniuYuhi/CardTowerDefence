using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    CinemachineBlendListCamera blendListCamera;

    [SerializeField]
    string m_inGameCameraName;

    bool m_changeSceneFlag = false;






    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        aaaa();

    }

    
    void aaaa()
    {
        if (m_changeSceneFlag|| blendListCamera.LiveChild==null) return;

        //���݂̃J�������ݒ肵�����O�Ɠ����Ȃ�
        if (blendListCamera.LiveChild.Name == m_inGameCameraName)
        {
            //�u�����h���łȂ��Ȃ�
            if (!blendListCamera.IsBlending)
            {
                //�J�������X�e�[�W�̃Z���^�[�Ɉڂ����̂�
                //�Q�[���X�^�[�g�p�l����\��������
                InGameUiManager.Instance.ActiveGameStartPanel();


                //�J���������S�ɐ؂�ւ�����̂Ńt���O�𗧂Ă�
                m_changeSceneFlag = true;
                //�Q�[���X�^�[�g��UI��\��������

            }
        }


        //�V�[���؂�ւ��t���O�𗧂Ă�
        //if (m_changeSceneFlag)
        //{
        //    m_changeSceneFlag = false;
        //    GameManager.Instance.SetChangeSceneFlag(true);
        //}
    }


}
