namespace PokerCore;

/// <summary>
/// 카드 52장 묶음 (덱).
/// 52장을 한 번씩만 만들어 섞은 뒤 위에서부터 나눠 주므로, 같은 덱에서 뽑은 카드는 구조적으로 절대 중복되지 않음
/// (매번 무작위로 뽑고 중복을 검사하는 방식보다 단순하고, 반복 횟수도 항상 일정함)
/// </summary>
public sealed class Deck
{
    /// <summary>52장 전체 (무늬 4 × 숫자 13). 모든 덱이 공유하는 읽기 전용 목록</summary>
    public static IReadOnlyList<Card> FullDeck { get; } =
        (from suit in Enum.GetValues<Suit>()
         from rank in Enum.GetValues<Rank>()
         select new Card(rank, suit)).ToArray();

    private readonly List<Card> _cards = new(FullDeck.Count);
    private readonly Random _random;

    /// <param name="random">난수 생성기. 생략하면 Random.Shared 사용 (테스트에서는 시드를 고정한 Random 을 넣어 결과를 재현 가능)</param>
    public Deck(Random? random = null)
    {
        _random = random ?? Random.Shared;
        Reset();
    }

    /// <summary>남은 카드 수</summary>
    public int Count => _cards.Count;

    /// <summary>52장을 다시 채우고 섞기</summary>
    public void Reset()
    {
        _cards.Clear();
        _cards.AddRange(FullDeck);
        Shuffle();
    }

    /// <summary>Fisher–Yates 셔플 : 모든 순서가 같은 확률로 나오는 섞기, O(n)</summary>
    public void Shuffle()
    {
        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);                    // 0 ~ i 중 하나
            (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
        }
    }

    /// <summary>맨 위에서 count 장을 나눠 주고 덱에서 제거</summary>
    public IReadOnlyList<Card> Deal(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        if (count > _cards.Count)
            throw new InvalidOperationException($"남은 카드({_cards.Count}장)보다 많이 나눠 줄 수 없습니다. (요청: {count}장)");

        int start = _cards.Count - count;
        List<Card> hand = _cards.GetRange(start, count);
        _cards.RemoveRange(start, count);
        return hand;
    }
}
