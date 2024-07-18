using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectType : BuildingBase
{
    [SerializeField, Header("���˂���e")]
    GameObject m_bullet;

    protected RangeObjectDetector m_rangeObjectDetector;



    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        m_rangeObjectDetector = GetComponent<RangeObjectDetector>();

    }
   
    /// <summary>
    /// �e������(�I�u�W�F�N�g�̒e)
    /// </summary>
    /// <param name="targetObject">�^�[�Q�b�g�̃I�u�W�F�N�g</param>
    /// <param name="shotPointTRS">�e�𔭎˂���|�C���g</param>
    /// <param name="angle">���ˊp�x</param>
    protected void Shot(GameObject targetObject,Transform shotPointTRS,float angle)
    {
        //��C�̒e�𔚔���������W
        Vector3 targetPos = targetObject.transform.position;

        //�e�𐶐�
        GameObject bullet = Instantiate(
            m_bullet, shotPointTRS.position, shotPointTRS.rotation);
        //�e���R���|�[�l���g���擾����
        ProjectileMovement bulletProjectible = bullet.GetComponent<ProjectileMovement>();

        //�e���������^�[�Q�b�g�̍��W�Ɖe����^����͈͂�ݒ�
        bulletProjectible.Init(targetPos,GetRuntimeStatus().GetRadius(), angle);

        //�U�������̂ŁA�C���^�[�o�����I���܂ł͍U���ł��Ȃ��悤�ɂ���
        AfterShot();
    }


}
