using PokerCore;

namespace G26W07WFCardDealer
{
    public partial class Form1 : Form
    {
        private readonly Deck _deck = new();
        private readonly IHandEvaluator _evaluator = new FiveCardHandEvaluator();
        private readonly PictureBox[] _cardBoxes;
        private HandResult? _currentResult;                      // 지금 화면에 보이는 패의 판별 결과 (분배 전에는 null)

        public Form1()
        {
            InitializeComponent();
            _cardBoxes = [Card1, Card2, Card3, Card4, Card5];
        }

        /// <summary>[카드 분배] 버튼 / 카드 클릭 : 새 덱에서 5장을 나눠 주고 어떤 족보인지 표시 (메인 기능)</summary>
        private void OnDeal(object? sender, EventArgs e)
        {
            _deck.Reset();                                                    // 52장 채우고 섞기
            IReadOnlyList<Card> hand = _deck.Deal(FiveCardHandEvaluator.HandSize);   // 위에서 5장 (중복 불가능)
            _currentResult = _evaluator.Evaluate(hand);                       // 족보 판별

            for (int i = 0; i < _cardBoxes.Length; i++)
                _cardBoxes[i].Image = CardImageMapper.GetImage(hand[i]);

            HandProbability probability = HandProbabilityTable.Of(_currentResult.Category);
            lblResult.Text = _currentResult.Description;                      // 예) K·9 투페어
            lblInfo.Text = $"카드 : {string.Join("  ", hand)}     |     족보 : {_currentResult.Name}" +
                           $"     |     이 족보가 나올 확률 : {probability.Probability:P4}";

            HighlightCurrentRank();                                           // 확률표가 열려 있으면 현재 족보 강조
        }

        /// <summary>[전체 족보 확률 보기] 버튼 : 10개 족보의 경우의 수와 확률을 표로 표시</summary>
        private void OnShowProbabilities(object? sender, EventArgs e)
        {
            lvProbabilities.BeginUpdate();
            lvProbabilities.Items.Clear();
            foreach (HandProbability p in HandProbabilityTable.All)
            {
                var item = new ListViewItem(new[]
                {
                    p.Rank.ToKoreanName(),
                    $"{p.Count:N0}",
                    $"{p.Probability:P4}",
                    "",
                })
                { Tag = p.Rank };
                lvProbabilities.Items.Add(item);
            }
            lvProbabilities.Items.Add(new ListViewItem(new[]
            {
                "합계", $"{HandProbabilityTable.TotalHands:N0}", $"{1.0:P4}", "",
            }));
            lvProbabilities.EndUpdate();

            HighlightCurrentRank();
        }

        /// <summary>확률표에서 현재 패의 족보 행을 강조 표시</summary>
        private void HighlightCurrentRank()
        {
            foreach (ListViewItem item in lvProbabilities.Items)
            {
                bool isCurrent = _currentResult is not null && item.Tag is HandRank rank && rank == _currentResult.Category;
                item.BackColor = isCurrent ? Color.LightYellow : SystemColors.Window;
                item.Font = new Font(lvProbabilities.Font, isCurrent ? FontStyle.Bold : FontStyle.Regular);
                item.SubItems[3].Text = isCurrent ? "◀ 현재 패" : "";
            }
        }
    }
}
