namespace PokerCore;

/// <summary>
/// 카드 한 장 = (숫자, 무늬).
/// readonly record struct 로 만든 이유
///  - 불변(immutable) : 한 번 만든 카드는 바뀌지 않음
///  - 값 비교 : 같은 숫자·무늬면 자동으로 같은 카드 (Equals / == / Distinct / Contains 가 바로 동작)
///  - 값 타입 : 작은 데이터라 복사 비용이 적음
/// </summary>
public readonly record struct Card(Rank Rank, Suit Suit)
{
    public string SuitSymbol => Suit switch
    {
        Suit.Spades => "♠",
        Suit.Hearts => "♥",
        Suit.Diamonds => "♦",
        Suit.Clubs => "♣",
        _ => "?",
    };

    /// <summary>예) ♠A, ♥10</summary>
    public override string ToString() => $"{SuitSymbol}{this.Rank.ToSymbol()}";
}
