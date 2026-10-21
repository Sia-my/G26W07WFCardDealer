namespace PokerCore;

/// <summary>
/// 포커 족보 (낮은 것 → 높은 것 순서).
/// enum 값의 크기 = 족보의 세기 → 족보끼리 비교할 때 그대로 크기 비교 가능
/// </summary>
public enum HandRank
{
    HighCard,       // 하이카드 (노페어)
    OnePair,        // 원페어
    TwoPair,        // 투페어
    ThreeOfAKind,   // 트리플
    Straight,       // 스트레이트
    Flush,          // 플러시
    FullHouse,      // 풀하우스
    FourOfAKind,    // 포카드
    StraightFlush,  // 스트레이트 플러시
    RoyalFlush,     // 로열 스트레이트 플러시
}

public static class HandRankExtensions
{
    public static string ToKoreanName(this HandRank rank) => rank switch
    {
        HandRank.RoyalFlush => "로열 스트레이트 플러시",
        HandRank.StraightFlush => "스트레이트 플러시",
        HandRank.FourOfAKind => "포카드",
        HandRank.FullHouse => "풀하우스",
        HandRank.Flush => "플러시",
        HandRank.Straight => "스트레이트",
        HandRank.ThreeOfAKind => "트리플",
        HandRank.TwoPair => "투페어",
        HandRank.OnePair => "원페어",
        HandRank.HighCard => "하이카드",
        _ => rank.ToString(),
    };
}
