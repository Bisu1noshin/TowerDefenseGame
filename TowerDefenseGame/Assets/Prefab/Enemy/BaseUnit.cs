using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnit : MonoBehaviour
{
    protected int HP;     //����HP
    protected int maxHP;  //�ő�HP
    protected bool canKB; //�m�b�N�o�b�N���ł���I�u�W�F�N�g���ۂ�
    protected int kbTime; //�m�b�N�o�b�N��
    [SerializeField]protected Vector2 moveVec; //��b�Ԃňړ�������W�ʂ�����
    List<float> timers = new List<float> { 0, 0 };             //�s���s�\�^�C�}�[�Ǘ��p�@1�ԖځF�U�����v���ԁ@2�ԖځF�m�b�N�o�b�N����
    List<float> maxTimers = new List<float> { 0.5f, 0.2f };    //�s���s�\�^�C�}�[�Ǘ��p�@1�ԖځF�ő�U�����ԁ@2�ԖځF�ő�m�b�N�o�b�N����
    protected float attackInterval;       //�U���Ԋu
    protected float maxAttackInterval;    //�ő�U���Ԋu
    [SerializeField]GameObject bar;
    protected bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        Setup();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += (Vector3)moveVec * Time.deltaTime;
        if (!isAlive) { return; }
        if(attackInterval > 0) { attackInterval -=  Time.deltaTime; }
        if (!DecreaseTimer()) { return; }
        UpdateOverrided();
    }
    protected virtual void Setup()
    {

    }
    protected abstract void UpdateOverrided();
    protected virtual void Hit(Vector2 angle, int damage) //�e�̔��ˊp�x�ƃ_���[�W������
    {
        HP -= damage;

        float HPper = ((float)HP / maxHP) * kbTime; //�m�b�N�o�b�N����
        if(HPper == (int)HPper && canKB)
        {
            KnockBack(angle);
        }
        
    }
    protected virtual void KnockBack(Vector2 angle)
    {
        moveVec = angle.normalized * 2.0f;
    }
    protected virtual void Kill()
    {
        Destroy(gameObject);
    }
    bool DecreaseTimer()
    {
        bool all0 = true;
        for(int t = 0; t < timers.Count; ++t)
        {
            if (timers[t] > 0)
            {
                timers[t] -= Time.deltaTime;
                all0 = false;
            }
        }
        return all0;
    }
    protected void SetTimer(int elementNum) //timers�̎w�肵���ԍ���Ή������ő�l�ɐݒ肷��
    {
        if(elementNum >= timers.Count) { return; }
        timers[elementNum] = maxTimers[elementNum];
    }
    protected virtual void SetAttack() //�U���������Ƃɂ��鏈��
    {
        attackInterval = maxAttackInterval;
        SetTimer(0);

    }
    public void Attack(int dmg) //Animation�ŌĂяo���p�̊֐�
    {
        PlayerController p = GameObject.Find("Player").GetComponent<PlayerController>();
        //p.Hit(dmg);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
