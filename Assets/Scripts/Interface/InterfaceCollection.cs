// �������̽��� ���� ��ũ��Ʈ


// �Ӽ����� ���ط��� ���ܰ迡�� �з��ϰ� damage�� �Ѱ��ֱ�
public interface IDamageable
{
    void TakeDamage(int damage);
}

// �Ӽ��������� �ִ� ��ü�� ��ӵ� �Ӽ����ݰ��޼���
public interface IDealFireElement
{
    // CalculateFireDamage �� �� �������� ������ �ۼ��ϱ�. 
    void CalculateFireDamage();
}