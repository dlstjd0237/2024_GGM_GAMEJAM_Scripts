using BIS.Entities;
using UnityEngine;
using System.Collections;
using BIS.Pool;
using BIS.Objects;
using DG.Tweening;
using BIS.Managers;

namespace BIS.Boss
{
    public class BossSkeleton : Entity
    {
        [SerializeField] private Vector2 _screenMidTopPos;
        [SerializeField] private Transform _attackTrm;
        [SerializeField] private Transform _bottomTrm;

        [SerializeField] private Transform _leftThorn;
        private float _leftThornDefualtX = -20;
        [SerializeField] private Transform _rightThorn;
        private float _rightThornDefualtY = 20;

        private AudioSource _audio;
        private float _bottomDefualtY = -2f;

        public override bool Init()
        {
            if (base.Init() == false)
                return false;

            _audio = GetComponent<AudioSource>();

            return true;
        }

        private void Start()
        {
        }
        private void OnEnable()
        {

            StartCoroutine(AttackStartCoroutine());
        }
        private IEnumerator AttackStartCoroutine()
        {
            transform.DOMove(_screenMidTopPos, 0.5f);
            yield return new WaitForSeconds(1); //����׿� ���߱�

            _audio.Play();
            for (int i = 0; i < 13; i++)
            {
                yield return new WaitForSeconds(0.435f);
                Pattern1();
            }//6.4��
            yield return new WaitForSeconds(0.435f);

            Pattern2();
            yield return new WaitForSeconds(0.85f);
            Pattern2();
            yield return new WaitForSeconds(0.45f);

            for (int i = 0; i < 13; i++)
            {
                yield return new WaitForSeconds(0.45f);
                Pattern1();
            }
            _bottomTrm.DOLocalMoveY(-2.5f, 1.5f).OnComplete(() => Manager.Camera.ShakeCamera(Vector3.one * 5, 5, 5, 0.3f));
            transform.DOMove(new Vector3(0, 0, 0), 2);
            transform.DORotate(new Vector3(0, 0, 360), 2, RotateMode.FastBeyond360).OnComplete(() => _bottomTrm.DOLocalMoveY(_bottomDefualtY, 0.2f));
            yield return new WaitForSeconds(2);
            for (int i = 0; i < 3; i++)
            {
                Pattern2();
                yield return new WaitForSeconds(0.4f);
            }
            for (int i = 0; i < 4; i++)
            {
                pattern4();
                pattern3();
                yield return new WaitForSeconds(0.55f);
                pattern3();
                yield return new WaitForSeconds(0.55f);
            }
            Pattern5();
            for (int i = 0; i < 4; i++)
            {
                pattern4();
                pattern3();
                yield return new WaitForSeconds(0.55f); //�̰�
                pattern4();
                pattern3();
                yield return new WaitForSeconds(0.22f);
            }
            _bottomTrm.DOLocalMoveY(-2.5f, 0.5f).OnComplete(() => Manager.Camera.ShakeCamera(Vector3.one * 5, 5, 5, 0.3f));
            transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360).OnComplete(() => _bottomTrm.DOLocalMoveY(_bottomDefualtY, 0.2f));
            yield return new WaitForSeconds(0.2f);
            Pattern6();
            yield return new WaitForSeconds(0.8f);
            for (int i = 0; i < 3; i++)
            {
                Pattern2();
                yield return new WaitForSeconds(0.5f);
            }
            Pattern5();
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(0.3f);
                Pattern6();
                yield return new WaitForSeconds(0.4f);
                Pattern6();

            }

            _bottomTrm.DOLocalMoveY(-2.5f, 0.2f).OnComplete(() => Manager.Camera.ShakeCamera(Vector3.one * 5, 5, 5, 0.3f));
            transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360).OnComplete(() => _bottomTrm.DOLocalMoveY(_bottomDefualtY, 0.2f));
            for (int i = 0; i < 3; i++)
            {
                Pattern6();
                yield return new WaitForSeconds(0.15f);
                pattern4();
                yield return new WaitForSeconds(0.2f);
                pattern3();
                yield return new WaitForSeconds(0.45f);
            }

            PoolManager.SpawnFromPool("Particle_Spiral", _attackTrm.position).GetComponent<ParticleSystem>().Play();
            Manager.Camera.ShakeCamera(new Vector3(1, 1, 1), 2, 2, 2.3f);

            yield return new WaitForSeconds(2.8f);
            Pattern7();
            transform.DOMoveX(-28, 3);
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(0.5f);
                pattern4();
                yield return new WaitForSeconds(0.5f);
                pattern3();
            }
            transform.DOMoveX(28, 3);
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(0.5f);
                pattern4();
                yield return new WaitForSeconds(0.5f);
                pattern3();
            }
            transform.DOMoveX(0, 3);

            Pattern7();

            transform.DOMove(_screenMidTopPos, 0.5f);//�ٽ� ���� �ö���

            yield return new WaitForSeconds(0.85f);

            for (int i = 0; i < 13; i++)
            {
                yield return new WaitForSeconds(0.435f);
                Pattern1();
            }//6.4��
            yield return new WaitForSeconds(0.435f);

            Pattern2();
            yield return new WaitForSeconds(0.85f);
            Pattern2();
            yield return new WaitForSeconds(0.45f);

            for (int i = 0; i < 13; i++)
            {
                yield return new WaitForSeconds(0.45f);
                Pattern1();
            }
            _bottomTrm.DOLocalMoveY(-2.5f, 1.5f).OnComplete(() => Manager.Camera.ShakeCamera(Vector3.one * 5, 5, 5, 0.3f));
            transform.DOMove(new Vector3(0, 0, 0), 2);
            transform.DORotate(new Vector3(0, 0, 360), 2, RotateMode.FastBeyond360).OnComplete(() => _bottomTrm.DOLocalMoveY(_bottomDefualtY, 0.2f));
            yield return new WaitForSeconds(2);
            for (int i = 0; i < 3; i++)
            {
                Pattern2();
                yield return new WaitForSeconds(0.4f);
            }
            for (int i = 0; i < 4; i++)
            {
                pattern4();
                pattern3();
                yield return new WaitForSeconds(0.55f);
                pattern3();
                yield return new WaitForSeconds(0.55f);
            }
            Pattern5();
            for (int i = 0; i < 4; i++)
            {
                pattern4();
                pattern3();
                yield return new WaitForSeconds(0.55f); //�̰�
                pattern4();
                pattern3();
                yield return new WaitForSeconds(0.22f);
            }
            PoolManager.SpawnFromPool("Particle_Spiral", _attackTrm.position).GetComponent<ParticleSystem>().Play();
            Manager.Camera.ShakeCamera(new Vector3(1, 1, 1), 2, 2, 2.3f);

            yield return new WaitForSeconds(2.8f);
            Pattern7();
            transform.DOMoveX(-28, 3);
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(0.5f);
                pattern4();
                yield return new WaitForSeconds(0.5f);
                pattern3();
            }
            transform.DOMoveX(28, 3);
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(0.5f);
                pattern4();
                yield return new WaitForSeconds(0.5f);
                pattern3();
            }
            transform.DOMoveX(0, 3);

            Pattern7();
            yield return new WaitForSeconds(2);
            for (int i = 0; i < 3; i++)
            {
                Pattern2();
                yield return new WaitForSeconds(0.4f);
            }
            for (int i = 0; i < 4; i++)
            {
                pattern4();
                pattern3();
                yield return new WaitForSeconds(0.55f);
                pattern3();
                yield return new WaitForSeconds(0.55f);
            }
            Pattern5();
            for (int i = 0; i < 4; i++)
            {
                pattern4();
                pattern3();
                yield return new WaitForSeconds(0.55f); //�̰�
                pattern4();
                pattern3();
                yield return new WaitForSeconds(0.22f);
            }
            Pattern5();
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(0.3f);
                Pattern6();
                yield return new WaitForSeconds(0.4f);
                Pattern6();

            }

            _bottomTrm.DOLocalMoveY(-2.5f, 0.2f).OnComplete(() => Manager.Camera.ShakeCamera(Vector3.one * 5, 5, 5, 0.3f));
            transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360).OnComplete(() => _bottomTrm.DOLocalMoveY(_bottomDefualtY, 0.2f));
            for (int i = 0; i < 3; i++)
            {
                Pattern6();
                yield return new WaitForSeconds(0.15f);
                pattern4();
                yield return new WaitForSeconds(0.2f);
                pattern3();
                yield return new WaitForSeconds(0.45f);
            }
            _bottomTrm.DOLocalMoveY(-2.5f, 0.2f).OnComplete(() => Manager.Camera.ShakeCamera(Vector3.one * 5, 5, 5, 0.3f));
            transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360).OnComplete(() => _bottomTrm.DOLocalMoveY(_bottomDefualtY, 0.2f));
            for (int i = 0; i < 3; i++)
            {
                Pattern6();
                yield return new WaitForSeconds(0.15f);
                pattern4();
                yield return new WaitForSeconds(0.2f);
                pattern3();
                yield return new WaitForSeconds(0.45f);
            }

            PoolManager.SpawnFromPool("Particle_Spiral", _attackTrm.position).GetComponent<ParticleSystem>().Play();
            Manager.Camera.ShakeCamera(new Vector3(1, 1, 1), 2, 2, 2.3f);

            yield return new WaitForSeconds(2.8f);
            Pattern7();
            transform.DOMoveX(-28, 3);
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(0.5f);
                pattern4();
                yield return new WaitForSeconds(0.5f);
                pattern3();
            }
            transform.DOMoveX(28, 3);
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(0.5f);
                pattern4();
                yield return new WaitForSeconds(0.5f);
                pattern3();
            }
            transform.DOMoveX(0, 3);

            Pattern7();

            transform.DOMove(_screenMidTopPos, 0.5f);//�ٽ� ���� �ö���

            yield return new WaitForSeconds(0.85f);

            for (int i = 0; i < 13; i++)
            {
                yield return new WaitForSeconds(0.435f);
                Pattern1();
            }//6.4��
            yield return new WaitForSeconds(0.435f);

            Pattern2();
            yield return new WaitForSeconds(0.85f);
            Pattern2();
            yield return new WaitForSeconds(0.45f);

            for (int i = 0; i < 13; i++)
            {
                yield return new WaitForSeconds(0.45f);
                Pattern1();
            }
            _bottomTrm.DOLocalMoveY(-2.5f, 1.5f).OnComplete(() => Manager.Camera.ShakeCamera(Vector3.one * 5, 5, 5, 0.3f));
            transform.DOMove(new Vector3(0, 0, 0), 2);
            transform.DORotate(new Vector3(0, 0, 360), 2, RotateMode.FastBeyond360).OnComplete(() => _bottomTrm.DOLocalMoveY(_bottomDefualtY, 0.2f));
            yield return new WaitForSeconds(2);
            for (int i = 0; i < 3; i++)
            {
                Pattern2();
                yield return new WaitForSeconds(0.4f);
            }
            for (int i = 0; i < 4; i++)
            {
                pattern4();
                pattern3();
                yield return new WaitForSeconds(0.55f);
                pattern3();
                yield return new WaitForSeconds(0.55f);
            }
            Pattern5();
            for (int i = 0; i < 4; i++)
            {
                pattern4();
                pattern3();
                yield return new WaitForSeconds(0.55f); //�̰�
                pattern4();
                pattern3();
                yield return new WaitForSeconds(0.22f);
            }
            _bottomTrm.DOLocalMoveY(-2.5f, 0.5f).OnComplete(() => Manager.Camera.ShakeCamera(Vector3.one * 5, 5, 5, 0.3f));
            transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360).OnComplete(() => _bottomTrm.DOLocalMoveY(_bottomDefualtY, 0.2f));
            yield return new WaitForSeconds(0.2f);
            Pattern6();
            yield return new WaitForSeconds(0.8f);
            for (int i = 0; i < 3; i++)
            {
                Pattern2();
                yield return new WaitForSeconds(0.5f);
            }
            Pattern5();
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(0.3f);
                Pattern6();
                yield return new WaitForSeconds(0.4f);
                Pattern6();

            }

            _bottomTrm.DOLocalMoveY(-2.5f, 0.2f).OnComplete(() => Manager.Camera.ShakeCamera(Vector3.one * 5, 5, 5, 0.3f));
            transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360).OnComplete(() => _bottomTrm.DOLocalMoveY(_bottomDefualtY, 0.2f));
            for (int i = 0; i < 3; i++)
            {
                Pattern6();
                yield return new WaitForSeconds(0.15f);
                pattern4();
                yield return new WaitForSeconds(0.2f);
                pattern3();
                yield return new WaitForSeconds(0.45f);
            }

            PoolManager.SpawnFromPool("Particle_Spiral", _attackTrm.position).GetComponent<ParticleSystem>().Play();
            Manager.Camera.ShakeCamera(new Vector3(1, 1, 1), 2, 2, 2.3f);

            yield return new WaitForSeconds(2.8f);
            Pattern7();
            transform.DOMoveX(-28, 3);
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(0.5f);
                pattern4();
                yield return new WaitForSeconds(0.5f);
                pattern3();
            }
            transform.DOMoveX(28, 3);
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(0.5f);
                pattern4();
                yield return new WaitForSeconds(0.5f);
                pattern3();
            }
            transform.DOMoveX(0, 3);
            transform.DOShakePosition(3, new Vector3(0.1f, 0.1f, 0), 20, 150, false, true);
            Debug.Log("���⼭ ��");
        }

        /// <summary>
        /// ��ä�� ����
        /// </summary>
        private void Pattern1()
        {
            Manager.Camera.ShakeCamera(Vector3.one * 3, 3, 3, 0.3f);

            int bulletCount = 8; // �߻��� źȯ ��
            float totalAngle = 150f; // ��ä���� ��ü ������ �� �а� ����
            float angleStep = totalAngle / (bulletCount - 1); // �� źȯ ���� ����
            float startAngle = -totalAngle / 2; // ��ä���� ���� ����

            for (int i = 0; i < bulletCount; i++)
            {
                // ���� źȯ�� ���� ���
                float currentAngle = startAngle + (angleStep * i);

                // ȸ�� ���
                Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);

                // �Ʒ��� ���� ���� ���
                Vector2 direction = rotation * Vector2.down;

                // źȯ ����

                BulletSpawn(direction, _attackTrm);
            }
        }

        /// <summary>
        /// �ĵ� ����
        /// </summary>
        private void Pattern2()
        {
            Manager.Camera.ShakeCamera(Vector3.one * 4, 4, 4, 0.3f);

            PoolManager.SpawnFromPool("RingAttack", _attackTrm.position).GetComponent<RingAttack>()
                 .Sizeup(0.8f);
        }

        /// <summary>
        /// ���Ʒ��翷 ���u
        /// </summary>
        private void pattern3()
        {
            Manager.Camera.ShakeCamera(Vector3.one * 3, 3, 3, 0.3f);

            BulletSpawn(Vector2.up, _attackTrm);
            BulletSpawn(Vector2.down, _attackTrm);
            BulletSpawn(Vector2.right, _attackTrm);
            BulletSpawn(Vector2.left, _attackTrm);
        }

        /// <summary>
        /// ���Ʒ��翷 ���u
        /// </summary>
        private void pattern4()
        {
            Manager.Camera.ShakeCamera(Vector3.one * 3, 3, 3, 0.3f);

            BulletSpawn(new Vector2(0.71f, 0.71f), _attackTrm);
            BulletSpawn(new Vector2(-0.71f, 0.71f), _attackTrm);
            BulletSpawn(new Vector2(0.71f, -0.71f), _attackTrm);
            BulletSpawn(new Vector2(-0.71f, -0.71f), _attackTrm);

        }

        private void Pattern5()
        {
            _leftThorn.DOLocalMoveX(-10, 0.4f).SetEase(Ease.InOutBack);
            Manager.Camera.ShakeCamera(Vector3.one * 5, 5, 5, 0.5f);
            DOVirtual.DelayedCall(1, () =>
            {
                _rightThorn.DOLocalMoveX(10, 0.4f).SetEase(Ease.InOutBack);
                Manager.Camera.ShakeCamera(Vector3.one * 5, 5, 5, 0.5f);

            });
            DOVirtual.DelayedCall(3, () =>
            {
                _rightThorn.DOLocalMoveX(_rightThornDefualtY, 0.4f).SetEase(Ease.InOutBack);
                _leftThorn.DOLocalMoveX(_leftThornDefualtX, 0.4f).SetEase(Ease.InOutBack);

            });
        }

        private void Pattern6()
        {
            int bulletCount = 18; // �߻��� źȯ ��
            float angleStep = 360f / bulletCount; // �� źȯ ������ ����
            Vector2 randomVector = new Vector2(Random.Range(-0.2f, 0.3f), Random.Range(-0.2f, 0.3f));
            for (int i = 0; i < bulletCount; i++)
            {
                // ���� źȯ�� ���� ���
                float currentAngle = i * angleStep;

                // ������ �������� ��ȯ
                float radian = currentAngle * Mathf.Deg2Rad;

                // ���� ���� ���
                Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
                direction += randomVector;
                // źȯ ����
                BulletSpawn(direction, _attackTrm);
            }
        }

        private void Pattern7()
        {
            int bulletCount = 8; // �߻��� źȯ ��
            float angleStep = 360f / bulletCount; // �� źȯ ������ ����

            for (int i = 0; i < bulletCount; i++)
            {
                // ���� źȯ�� ���� ���
                float currentAngle = i * angleStep;

                // ������ �������� ��ȯ
                float radian = currentAngle * Mathf.Deg2Rad;

                // ���� ���� ���
                Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

                // źȯ ����
                GameObject go = PoolManager.SpawnFromPool("EnemyAlphaLaser", _attackTrm.transform.position, Quaternion.Euler(0, 0, currentAngle));
                go.GetComponent<EnemyAlphaLaser>().PlayAlphaLaser(Stat.attackDamage.GetValue());
            }
        }
        private void BulletSpawn(Vector3 dir, Transform shooter)
        {
            PoolManager.SpawnFromPool("EnemyBullet", shooter.position).GetComponent<Bullet>()
                  .SetMovement(dir, Stat.bulletSpeed.GetValue(), Stat.attackDamage.GetValue());
        }



    }
}
