namespace PokerCore;

/// <summary>
/// 카드의 숫자 (13종).
/// 값 자체가 카드의 세기가 되도록 2 ~ 14 로 지정 (A = 14 가 가장 높음)
/// → 크기 비교, 연속(스트레이트) 판별을 정수 연산으로 바로 할 수 있음
/// </summary>
public enum Rank
{
    Two = 2,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,   // 11
    Queen,  // 12
    King,   // 13
    Ace,    // 14
}

public static class RankExtensions
{
    /// <summary>화면/로그 출력용 기호 (A, K, Q, J, 10 ~ 2)</summary>
    public static string ToSymbol(this Rank rank) => rank switch
    {
        Rank.Ace => "A",
        Rank.King => "K",
        Rank.Queen => "Q",
        Rank.Jack => "J",
        _ => ((int)rank).ToString(),
    };
}
