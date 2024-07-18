using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �e���̈ړ������i�������j
/// </summary>
public class ProjectileMovement : MonoBehaviour
{
    Rigidbody m_rigidbody;

    float m_range;   //�e����^����͈�

    Vector3 m_targetPostion = Vector3.zero;

    Vector3 m_direction = Vector3.zero;

    float m_throwingAngle;

    float m_speed = 5.0f;

    bool isInit = false;




    /// <summary>
    /// �^�[�Q�b�g�̍��W��ݒ�
    /// </summary>
    /// <param name="position"></param>
    public void SetTargetPosition(Vector3 position)
    {
        m_targetPostion = position;
    }

    /// <summary>
    /// �͈͂�ݒ�
    /// </summary>
    /// <param name="range">�͈�</param>
    public void SetRange(float range)
    {
        m_range = range;
    }

    
    /// <summary>
    /// �������̏���������
    /// </summary>
    /// <param name="position">�^�[�Q�b�g�̍��W</param>
    /// <param name="range">�e����^����͈�</param>
    /// <param name="angle">�p�x</param>
    public void Init(Vector3 position, float range,float angle)
    {
        m_targetPostion = position;
        m_range = range;
        m_throwingAngle = angle;
        SettingMoveDirection();

        isInit = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

        if (!isInit) SettingMoveDirection();


        // �ˏo���x���Z�o
        Vector3 velocity = CalculateVelocity(
            transform.position, m_targetPostion, m_throwingAngle
            );

        m_rigidbody.AddForce(velocity * m_rigidbody.mass, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SettingMoveDirection()
    {
        //���g����^�[�Q�b�g�Ɍ������x�N�g�����v�Z
        m_direction = m_targetPostion - transform.position;
        m_direction.Normalize();

        m_direction *= m_speed;
    }


    /// <summary>
    /// ���x�̌v�Z
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    Vector3 CalculateVelocity(Vector3 start, Vector3 end,float angle)
    {
        // �ˏo�p�����W�A���ɕϊ�
        float rad = angle * Mathf.PI / 180;

        // ���������̋���x
        float x = Vector2.Distance(
            new Vector2(start.x, end.z), new Vector2(end.x, start.z)
            );

        // ���������̋���y
        float y = start.y - end.y;

        // �Ε����˂̌����������x�ɂ��ĉ���
        float speed = Mathf.Sqrt(
            -Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y))
            );



        if (float.IsNaN(speed))
        {
            // �����𖞂����������Z�o�ł��Ȃ����Vector3.zero��Ԃ�
            return Vector3.zero;
        }
        else
        {
            return (new Vector3(
                end.x - start.x, 
                x * Mathf.Tan(rad), 
                end.z - start.z).normalized * speed
                );
        }
    }


}
