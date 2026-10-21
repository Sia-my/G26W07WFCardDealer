namespace PokerCore;

/// <summary>
/// 5장 포커 족보 판별기.
///
/// [알고리즘] 5장에서 딱 세 가지 정보만 뽑으면 모든 족보가 결정됨
///   ① 같은 숫자끼리 묶은 그룹 (장수 많은 순 → 숫자 높은 순 정렬)   예) K K 9 9 3 → (K×2) (9×2) (3×1)
///   ② 플러시 여부 : 무늬 종류가 1개인가
///   ③ 스트레이트 여부 : 숫자 5개가 모두 다르고 연속인가 (A-2-3-4-5 백 스트레이트 포함)
/// 이후 높은 족보부터 차례로 검사하여 처음 맞는 것이 정답 (if-else 사슬)
///
/// [같은 족보끼리 비교] ①의 정렬 순서가 곧 비교 순서 (TieBreakers)
///   예) 풀하우스 Q Q Q 7 7 → [Q, 7] : 트리플 숫자 먼저, 그다음 페어 숫자
/// </summary>
public sealed class FiveCardHandEvaluator : IHandEvaluator
{
    public const int HandSize = 5;

    public HandResult Evaluate(IReadOnlyCollection<Card> cards)
    {
        ArgumentNullException.ThrowIfNull(cards);
        if (cards.Count != HandSize)
            throw new ArgumentException($"카드는 정확히 {HandSize}장이어야 합니다. (입력: {cards.Count}장)", nameof(cards));
        if (cards.Distinct().Count() != HandSize)
            throw new ArgumentException("같은 카드가 두 번 이상 들어 있습니다.", nameof(cards));

        // ① 숫자별 그룹 : 장수 내림차순 → 숫자 내림차순
        List<(Rank Rank, int Count)> groups = cards
            .GroupBy(card => card.Rank)
            .Select(g => (Rank: g.Key, Count: g.Count()))
            .OrderByDescending(g => g.Count)
            .ThenByDescending(g => g.Rank)
            .ToList();

        // ② 플러시 : 무늬가 한 종류
        bool isFlush = cards.Select(card => card.Suit).Distinct().Count() == 1;

        // ③ 스트레이트 : 연속된 5개의 숫자 (가장 높은 숫자도 같이 구함)
        bool isStraight = TryGetStraightHigh(groups, out Rank straightHigh);

        List<Rank> groupRanks = groups.Select(g => g.Rank).ToList();
        int first = groups[0].Count;                          // 가장 많은 그룹의 장수
        int second = groups.Count > 1 ? groups[1].Count : 0;  // 두 번째 그룹의 장수

        // 높은 족보부터 검사
        HandRank category;
        IReadOnlyList<Rank> tieBreakers = groupRanks;

        if (isStraight && isFlush)
        {
            category = straightHigh == Rank.Ace ? HandRank.RoyalFlush : HandRank.StraightFlush;
            tieBreakers = [straightHigh];
        }
        else if (first == 4)
            category = HandRank.FourOfAKind;          // 4 + 1
        else if (first == 3 && second == 2)
            category = HandRank.FullHouse;            // 3 + 2
        else if (isFlush)
            category = HandRank.Flush;
        else if (isStraight)
        {
            category = HandRank.Straight;
            tieBreakers = [straightHigh];
        }
        else if (first == 3)
            category = HandRank.ThreeOfAKind;         // 3 + 1 + 1
        else if (first == 2 && second == 2)
            category = HandRank.TwoPair;              // 2 + 2 + 1
        else if (first == 2)
            category = HandRank.OnePair;              // 2 + 1 + 1 + 1
        else
            category = HandRank.HighCard;             // 1 + 1 + 1 + 1 + 1

        List<Card> sorted = cards.OrderByDescending(c => c.Rank).ThenByDescending(c => c.Suit).ToList();
        return new HandResult(category, tieBreakers, sorted);
    }

    /// <summary>
    /// 스트레이트 판별. groups 는 장수 → 숫자 내림차순으로 정렬되어 있으므로,
    /// 그룹이 5개(= 숫자가 모두 다름)이면 groups[0] 이 가장 높은 숫자, groups[4] 가 가장 낮은 숫자
    /// </summary>
    private static bool TryGetStraightHigh(List<(Rank Rank, int Count)> groups, out Rank high)
    {
        high = default;
        if (groups.Count != HandSize) return false;           // 같은 숫자가 있으면 스트레이트 불가

        Rank top = groups[0].Rank;
        Rank bottom = groups[HandSize - 1].Rank;

        if ((int)top - (int)bottom == 4)                      // 예) 9 8 7 6 5 : 9 - 5 = 4
        {
            high = top;
            return true;
        }
        if (top == Rank.Ace && groups[1].Rank == Rank.Five)   // A 5 4 3 2 : A 를 1 로 보는 백 스트레이트
        {
            high = Rank.Five;                                 // 가장 낮은 스트레이트 (5 하이)
            return true;
        }
        return false;
    }
}
