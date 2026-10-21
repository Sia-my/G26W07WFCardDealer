namespace PokerCore;

/// <summary>
/// 족보 판별기 인터페이스.
/// 화면(Form)은 이 인터페이스에만 의존 → 규칙이 다른 판별기(예: 7장 중 최고 5장, 조커 포함 등)로 바꿔 끼워도 화면 코드는 그대로
/// </summary>
public interface IHandEvaluator
{
    HandResult Evaluate(IReadOnlyCollection<Card> cards);
}
