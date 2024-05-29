// 인터페이스를 모은 스크립트


// 속성공격 피해량은 계산단계에서 분류하고 damage에 넘겨주기
public interface IDamageable
{
    void TakeDamage(int damage);
}

// 속성데미지를 주는 객체에 상속될 속성공격계산메서드
public interface IDealFireElement
{
    // CalculateFireDamage 에 줄 데미지의 계산식을 작성하기. 
    void CalculateFireDamage();
}