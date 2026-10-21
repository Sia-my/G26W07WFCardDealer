namespace PokerCore;

/// <summary>족보 하나의 이론 확률 : 경우의 수(Count) / 전체 경우의 수</summary>
public sealed record HandProbability(HandRank Rank, long Count, double Probability);

/// <summary>
/// 포커 전체 족보 확률표.
/// 52장 중 5장을 뽑는 모든 경우(52C5 = 2,598,960가지) 중 각 족보가 나오는 경우의 수를 조합 공식으로 계산.
///   C(n, k) = n 개 중 k 개를 고르는 경우의 수
/// 계산 결과는 PokerCore.Tests 에서 판별기로 2,598,960개를 전부 판별한 개수와 일치하는지 검증함
/// </summary>
public static class HandProbabilityTable
{
    /// <summary>전체 경우의 수 52C5 = 2,598,960</summary>
    public static long TotalHands { get; } = Combination(52, 5);

    /// <summary>족보별 확률 (높은 족보부터)</summary>
    public static IReadOnlyList<HandProbability> All { get; } = Build();

    /// <summary>특정 족보의 확률</summary>
    public static HandProbability Of(HandRank rank) => All.First(p => p.Rank == rank);

    private static IReadOnlyList<HandProbability> Build()
    {
        const long suitPatterns = 4 * 4 * 4 * 4 * 4;                // 5장의 무늬 배치 4^5 = 1,024

        var counts = new Dictionary<HandRank, long>
        {
            // 10-J-Q-K-A × 무늬 4
            [HandRank.RoyalFlush] = 4,
            // 스트레이트 10종(A-5 ~ 10-A) × 무늬 4 - 로열 4
            [HandRank.StraightFlush] = 10 * 4 - 4,
            // 포카드 숫자 13 × 나머지 1장 48
            [HandRank.FourOfAKind] = 13 * 48,
            // 트리플 숫자 13 × 무늬 C(4,3) × 페어 숫자 12 × 무늬 C(4,2)
            [HandRank.FullHouse] = 13 * Combination(4, 3) * 12 * Combination(4, 2),
            // 무늬 4 × 같은 무늬 13장 중 5장 - 스트레이트 플러시(로열 포함) 40
            [HandRank.Flush] = 4 * Combination(13, 5) - 40,
            // 스트레이트 10종 × 무늬 배치 4^5 - 스트레이트 플러시(로열 포함) 40
            [HandRank.Straight] = 10 * suitPatterns - 40,
            // 트리플 숫자 13 × C(4,3) × 나머지 숫자 2개 C(12,2) × 각 무늬 4 × 4
            [HandRank.ThreeOfAKind] = 13 * Combination(4, 3) * Combination(12, 2) * 4 * 4,
            // 페어 숫자 2개 C(13,2) × 각 무늬 C(4,2)² × 나머지 1장 44
            [HandRank.TwoPair] = Combination(13, 2) * Combination(4, 2) * Combination(4, 2) * 44,
            // 페어 숫자 13 × C(4,2) × 나머지 숫자 3개 C(12,3) × 각 무늬 4 × 4 × 4
            [HandRank.OnePair] = 13 * Combination(4, 2) * Combination(12, 3) * 4 * 4 * 4,
            // (숫자 5개 C(13,5) - 스트레이트 10) × (무늬 배치 4^5 - 플러시 4)
            [HandRank.HighCard] = (Combination(13, 5) - 10) * (suitPatterns - 4),
        };

        return counts
            .OrderByDescending(kv => kv.Key)
            .Select(kv => new HandProbability(kv.Key, kv.Value, (double)kv.Value / TotalHands))
            .ToList();
    }

    /// <summary>조합 C(n, k) = n! / (k! (n-k)!) 를 곱셈·나눗셈 반복으로 계산 (중간값이 항상 정수)</summary>
    public static long Combination(int n, int k)
    {
        long result = 1;
        for (int i = 1; i <= k; i++)
            result = result * (n - k + i) / i;
        return result;
    }
}
