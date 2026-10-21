using PokerCore;

namespace G26W07WFCardDealer
{
    /// <summary>
    /// [View 전용] Card(비즈니스 모델) → 카드 이미지(리소스) 변환.
    /// 이미지 파일 이름 규칙은 화면에만 필요한 정보이므로 PokerCore 가 아니라 WinForms 프로젝트에 둠
    ///   - 파일 이름 : {숫자}_of_{무늬}  예) ace_of_spades, 10_of_hearts
    ///   - J, Q, K 는 그림이 있는 버전(이름 끝에 "2")을 사용  예) king_of_clubs2
    /// </summary>
    internal static class CardImageMapper
    {
        // 같은 이미지를 매번 새로 불러오지 않도록 캐시 (Bitmap 객체가 계속 쌓이는 것 방지)
        private static readonly Dictionary<string, Image> Cache = new();

        public static string ToResourceName(Card card)
        {
            string rank = card.Rank switch
            {
                Rank.Ace => "ace",
                Rank.King => "king",
                Rank.Queen => "queen",
                Rank.Jack => "jack",
                _ => ((int)card.Rank).ToString(),
            };
            string suit = card.Suit.ToString().ToLowerInvariant();   // Spades → spades
            bool isFaceCard = card.Rank is Rank.Jack or Rank.Queen or Rank.King;

            return $"{rank}_of_{suit}{(isFaceCard ? "2" : "")}";
        }

        public static Image GetImage(Card card)
        {
            string name = ToResourceName(card);
            if (!Cache.TryGetValue(name, out Image? image))
            {
                image = Properties.Resources.ResourceManager.GetObject(name) as Image
                        ?? throw new InvalidOperationException($"카드 이미지 리소스를 찾을 수 없습니다: {name}");
                Cache[name] = image;
            }
            return image;
        }
    }
}
