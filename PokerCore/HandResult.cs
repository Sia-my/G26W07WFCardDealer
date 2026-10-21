namespace PokerCore;

/// <summary>
/// 족보 판별 결과.
///  - Category    : 족보 종류
///  - TieBreakers : 같은 족보끼리 승부를 가릴 때 비교할 숫자 (중요한 것부터)
///                  예) 풀하우스 Q-Q-Q-7-7 → [Q, 7],  투페어 K-K-9-9-3 → [K, 9, 3],  백 스트레이트 → [5]
///  - Cards       : 판별에 쓰인 5장 (높은 숫자 순으로 정렬)
/// IComparable 을 구현해서 두 패의 승패 비교에도 재사용 가능
/// </summary>
public sealed class HandResult : IComparable<HandResult>
{
    public HandResult(HandRank category, IReadOnlyList<Rank> tieBreakers, IReadOnlyList<Card> cards)
    {
        Category = category;
        TieBreakers = tieBreakers;
        Cards = cards;
    }

    public HandRank Category { get; }
    public IReadOnlyList<Rank> TieBreakers { get; }
    public IReadOnlyList<Card> Cards { get; }

    /// <summary>족보 이름. 예) 풀하우스</summary>
    public string Name => Category.ToKoreanName();

    /// <summary>자세한 설명. 예) Q 풀하우스 (Q 트리플 + 7 페어)</summary>
    public string Description
    {
        get
        {
            string t0 = TieBreakers[0].ToSymbol();
            string t1 = TieBreakers.Count > 1 ? TieBreakers[1].ToSymbol() : "";
            return Category switch
            {
                HandRank.RoyalFlush => "로열 스트레이트 플러시 (10-J-Q-K-A)",
                HandRank.StraightFlush when TieBreakers[0] == Rank.Five => "백 스트레이트 플러시 (A-2-3-4-5)",
                HandRank.StraightFlush => $"{t0} 하이 스트레이트 플러시",
                HandRank.FourOfAKind => $"{t0} 포카드",
                HandRank.FullHouse => $"{t0} 풀하우스 ({t0} 트리플 + {t1} 페어)",
                HandRank.Flush => $"{t0} 하이 플러시",
                HandRank.Straight when TieBreakers[0] == Rank.Ace => "마운틴 스트레이트 (10-J-Q-K-A)",
                HandRank.Straight when TieBreakers[0] == Rank.Five => "백 스트레이트 (A-2-3-4-5)",
                HandRank.Straight => $"{t0} 하이 스트레이트",
                HandRank.ThreeOfAKind => $"{t0} 트리플",
                HandRank.TwoPair => $"{t0}·{t1} 투페어",
                HandRank.OnePair => $"{t0} 원페어",
                _ => $"{t0} 하이카드 (노페어)",
            };
        }
    }

    /// <summary>
    /// 두 패 비교 : 족보 종류가 다르면 족보로, 같으면 TieBreakers 를 앞에서부터 차례로 비교.
    /// 양수 = this 가 이김, 음수 = other 가 이김, 0 = 무승부 (무늬로는 승부를 가리지 않음)
    /// </summary>
    public int CompareTo(HandResult? other)
    {
        if (other is null) return 1;

        int byCategory = Category.CompareTo(other.Category);
        if (byCategory != 0) return byCategory;

        for (int i = 0; i < Math.Min(TieBreakers.Count, other.TieBreakers.Count); i++)
        {
            int byRank = TieBreakers[i].CompareTo(other.TieBreakers[i]);
            if (byRank != 0) return byRank;
        }
        return 0;
    }

    public override string ToString() => $"{Description} [{string.Join(" ", Cards)}]";
}
