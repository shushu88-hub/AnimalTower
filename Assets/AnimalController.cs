using UnityEngine;
using UnityEngine.UI;

public class AnimalController : MonoBehaviour
{
    // �������v���C���[�ɂ���ĉ�����ꂽ�����m�F���邽�߂̃t���O
    private bool isReleased = false;
    // Rigidbody2D�R���|�[�l���g���i�[���邽�߂̕ϐ�
    private Rigidbody2D rb;
    // �����̈ړ����x�𒲐����邽�߂̃p�����[�^
    public float moveSpeed = 10f;

    void Start()
    {
        // Rigidbody2D���擾
        rb = GetComponent<Rigidbody2D>();
        // ������Ԃł͓������Œ肵�A��������������܂ŕ������Z���~
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        // �������܂��������Ă��Ȃ��ꍇ�A�v���C���[�ɂ�鑀�������
        if (!isReleased)
        {
            // �}�E�X��X���W���擾���A�����̈ʒu������ɍ��킹��
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, transform.position.y, 0);

            // ���N���b�N�i�������̓^�b�v�j�œ����𗎉�������
            if (Input.GetMouseButtonDown(0))
            {
                ReleaseAnimal();
            }
        }
    }

    // �����𗎉������郁�\�b�h
    void ReleaseAnimal()
    {
        // ������������ꂽ���Ƃ������t���O��true�ɂ���
        isReleased = true;
        // �������Z��L�����iDynamic�ɕύX���邱�Ƃœ����������j
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    // �Փˎ��ɍ������X�R�A�ɉ��Z���A�J�����𒲐�����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isReleased)
        {
            // ������Y���W�i�����j���擾
            float height = transform.position.y;

            // �Q�[���}�l�[�W���[�ɍ�����n���A�J�����ʒu�𒲐����ăX�R�A�����Z
            FindObjectOfType<GameManager>().AddScoreAndAdjustCamera(height);

            // ���݂̓�����null�ɐݒ肵�ĐV���������𐶐��\�ɂ���
            FindObjectOfType<AnimalSpawner>().currentAnimal = null;

            // ���̓����I�u�W�F�N�g���폜
            Destroy(this);
        }
    }

}
