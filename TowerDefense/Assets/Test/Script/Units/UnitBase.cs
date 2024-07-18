using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : SpawnableBase, IUnit,IDamageable
{
    //[SerializeField, Header("自分の所属する拠点")]
    //GameObject m_myBaseObject;

    [SerializeField,Header("キャラクターのステータス")]
    Status_Character m_originalCharaStatus;

    Status_Character m_runtimeStatus;      //実行中に使うステータス

    UnitStateContext m_pawnStateContext;        //プレイヤーの状態を管理

    AnimationEventScript m_eventScript;

    protected Animator m_animator;

    AttackTimer m_attackTimer;

    CharacterControll m_charaCon;

    protected bool m_normalAttackAction = false;

    protected bool m_skillAttackAction = false;

    protected bool m_isAttackActive = false;


    int m_addAttackPower = 0;           //バフなどの効果で上乗せする攻撃力

    /// <summary>
    /// 付加攻撃力を設定する
    /// </summary>
    /// <param name="value"></param>
    public void SetAddAttackPower(int value)
    {
        m_addAttackPower = value;
    }
    /// <summary>
    /// 付加攻撃力を取得
    /// </summary>
    /// <returns></returns>
    public int GetAddAttackPower()
    {
        return m_addAttackPower;
    }

    ///// <summary>
    ///// 自身の拠点オブジェクトを設定
    ///// </summary>
    ///// <returns></returns>
    //public void SetBaseObject(GameObject baseObject)
    //{
    //    m_myBaseObject = baseObject;
    //}
    ///// <summary>
    ///// 自身の拠点オブジェクトを取得
    ///// </summary>
    ///// <returns></returns>
    //public GameObject GetBaseObject()
    //{
    //    return m_myBaseObject;
    //}

    /// <summary>
    /// 攻撃タイマーを取得
    /// </summary>
    /// <returns></returns>
    public AttackTimer GetAttackTimer()
    {
        return m_attackTimer;
    }
    /// <summary>
    /// キャラコンの取得
    /// </summary>
    /// <returns></returns>
    public CharacterControll GetCharacterController()
    {
        return m_charaCon;
    }
    /// <summary>
    /// アニメーターを取得
    /// </summary>
    /// <returns></returns>
    public Animator GetAnimator()
    {
        return m_animator;
    }
    /// <summary>
    /// 実行中のステータスを取得
    /// </summary>
    /// <returns></returns>
    public Status_Character GetRuntimeStatus()
    {
        return m_runtimeStatus;
    }
    /// <summary>
    /// 攻撃中かのフラグを設定
    /// </summary>
    /// <param name="flag"></param>
    public void SetIsAttackActive(bool flag)
    {
        m_isAttackActive = flag;
    }
    /// <summary>
    /// 攻撃中かのフラグを取得
    /// </summary>
    /// <returns></returns>
    public bool GetIsAttackActive()
    {
        return m_isAttackActive;
    }
    /// <summary>
    /// アニメーションイベントスクリプトを取得
    /// </summary>
    /// <returns></returns>
    public AnimationEventScript GetAnimationEventScript()
    {
        return m_eventScript;
    }

    /// <summary>
    /// 現在のステートに応じた攻撃力を取得
    /// </summary>
    /// <returns></returns>
    public int GetCurrentAttackPower()
    {
        //攻撃力は通常攻撃の攻撃力
        int atk = m_runtimeStatus.GetNormalAttack();
        //ステートがスキル攻撃ならば、攻撃力をスキル攻撃力に変更
        if (m_pawnStateContext.GetCurrentUnitState().GetUnitState() == 
            En_UnitState.enUnitState_SkillAttack)
        {
            atk = m_runtimeStatus.GetSkillAttack();
        }

        //付加攻撃力を足す
        atk += m_addAttackPower;

        return atk;
    }

    /// <summary>
    /// アニメーターのパラメータを設定
    /// </summary>
    protected virtual void SetAnimatorParameters() { }

    protected virtual void Awake()
    {
        //オリジナルのデータからゲーム中に使うデータにコピー
        m_runtimeStatus = Instantiate(m_originalCharaStatus);
        //ステータスを初期化
        m_runtimeStatus.InitStatus();

        //ステート管理クラスを生成
        m_pawnStateContext = new UnitStateContext();
        //ステートを初期化
        m_pawnStateContext.Init(this, En_UnitState.enUnitState_Walk);   
    }



    // Start is called before the first frame update
    protected virtual void Start()
    {
        //各種コンポーネントを取得
        m_animator = GetComponent<Animator>();
        //攻撃インターバルを計るタイマーを生成
        m_attackTimer = new AttackTimer(
            m_runtimeStatus.GetNormalAtkIntarval(),
            m_runtimeStatus.GetSkillAtkIntarval());
        //キャラコンコンポーネントを取得
        m_charaCon = GetComponent<CharacterControll>();
        //アニメーションイベントコンポーネントを取得
        m_eventScript = GetComponent<AnimationEventScript>();
        //キャラクターを移動方向に回転
        transform.rotation = GetBaseObject().transform.rotation;
    }

    /// <summary>
    /// 死亡アニメーションが終わった後の処理
    /// </summary>
    public virtual void ProcessDie() { }

    //移動処理など
    protected virtual void FixedUpdate()
    {
        //現在のステートの更新処理
        m_pawnStateContext.FixedUpdate();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //攻撃中はタイマーを動かさない
        if(!m_isAttackActive)
        {
            //攻撃用タイマーを更新
            m_attackTimer.Update();
        }
        //現在のステートの更新処理
        m_pawnStateContext.Update();
        //アニメーターのパラメータを設定
        SetAnimatorParameters();

        Debug.Log(m_pawnStateContext.GetCurrentUnitState());
    }

    /// <summary>
    /// 共通の状態遷移処理
    /// </summary>
    /// <returns>現在の状態から遷移するならtrue、しないならfalse</returns>
    public bool CommonStateTransition()
    {
        //動きを止める

        //ターゲットが近くにいるなら攻撃
        if(m_charaCon.IsAttackableTarget())
        {
            //通常攻撃、スキル攻撃のどちらかを使用したなら
            if (IsActionAttack())
            {
                //攻撃中
                m_isAttackActive = true;

                m_eventScript.ActiveStart();
                return true;
            }
        }

        //待機状態と歩き状態
        //ターゲットの近くにいるなら待機状態
        if (m_charaCon.GetIsMove())
        {
            ChangeStateWalk();
        }
        else
        {
            ChangeStateIdle();
        }

        
        //状態は変わらない
        return false;
    }

    protected virtual bool IsActionAttack()
    {
        //攻撃中は処理しない
        if (m_isAttackActive) return false;

        //スキル攻撃ができる状態なら
        if(DecideSkillAttack()) return true;

        //通常攻撃ができる状態なら
        if (DecideNormalAttack()) return true;
        

        //攻撃しない
        return false;
    }

    protected bool DecideNormalAttack()
    {
        //通常攻撃ができる状態なら
        if (m_attackTimer.IsNormalAttackable())
        {
            m_normalAttackAction = true;
            ChangeStateNormalAttack();
            return true;
        }

        return false;
    }

    protected bool DecideSkillAttack()
    {
        //スキル攻撃ができる状態なら
        if (m_attackTimer.IsSkillAttackable())
        {
            m_skillAttackAction = true;
            ChangeStateSkillAttack();
            return true;
        }

        return false;
    }


    public void MoveTo()
    {
        //移動処理
        m_charaCon.MoveTo(m_runtimeStatus.GetSpeed());
    }

    public void Rotation()
    {
        //回転処理
        m_charaCon.Rotation();
    }

    /// <summary>
    /// HPを回復する
    /// </summary>
    /// <param name="recoveryHp"></param>
    public void RecoverHp(int recoveryHp)
    {
        m_runtimeStatus.RecoverHp(recoveryHp);
    }

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="hitDamage">受けるダメージ</param>
    public void Damage(int hitDamage)
    {
        m_runtimeStatus.ApplyDamage(hitDamage);

        Debug.Log(transform.name + "は" + hitDamage + "を受けた");

        if (m_runtimeStatus.GetHp()<=0)
        {
            Die();
        }
    }

    /// <summary>
    /// 死亡時処理
    /// </summary>
    public void Die()
    {
        m_animator.SetTrigger("Die");
        //死亡状態に切り替え
        ChangeStateDie();

        Debug.Log(transform.name + "は倒された");
    }

    ///////////////////////////////////////////////////////////////////////////////////////
    /// 各ステートクラス切り替え処理
    ///////////////////////////////////////////////////////////////////////////////////////

    //待機状態に切り替え
    public void ChangeStateIdle() =>
        m_pawnStateContext.ChangeState(En_UnitState.enUnitState_Idle);

    //歩き状態に切り替え
    public void ChangeStateWalk() =>
        m_pawnStateContext.ChangeState(En_UnitState.enUnitState_Walk);

    //通常攻撃状態に切り替え
    public void ChangeStateNormalAttack() =>
        m_pawnStateContext.ChangeState(En_UnitState.enUnitState_normalAttack);

    //スキル攻撃状態に切り替え
    public void ChangeStateSkillAttack() =>
        m_pawnStateContext.ChangeState(En_UnitState.enUnitState_SkillAttack);

    //死亡状態に切り替え
    public void ChangeStateDie() =>
        m_pawnStateContext.ChangeState(En_UnitState.enUnitState_Die);

   
}
