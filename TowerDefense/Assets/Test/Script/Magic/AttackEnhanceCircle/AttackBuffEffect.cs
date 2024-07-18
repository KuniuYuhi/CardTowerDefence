using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuffEffect : MonoBehaviour
{
    float m_boostDuration = 0.0f;

    int m_buffAmount = 0;

    UnitBase m_unit;
   
    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="buffAmount">�t���U����</param>
    /// <param name="boostDuration">���ʎ�������</param>
    public void Init(UnitBase unit, int buffAmount,float boostDuration)
    {
        m_unit = unit;
        m_buffAmount = buffAmount;
        m_boostDuration = boostDuration;
    }

    // Start is called before the first frame update
    void Start()
    {
        //�o�t�������郆�j�b�g�̕t���U���͂ɐݒ肵���U���͂���
        m_unit.SetAddAttackPower(m_buffAmount);

        //���ʎ������Ԃ��v���č폜����R���[�`�������s
        StartCoroutine(CleanupOnDestroy());

    }

    IEnumerator CleanupOnDestroy()
    {
        //���ʎ������Ԃ��I���܂ł͑҂�
        yield return new WaitForSeconds(m_boostDuration);

        //���Ԃ��I�������A�t���U���͂����Z�b�g���č폜
        m_unit.SetAddAttackPower(0);


        Destroy(this);
    }

}
