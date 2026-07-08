namespace Commons
{
    /// <summary>
    /// 売買リストの1項目。買う竿 / 売る魚 / 売る竿 の共通抽象。
    /// </summary>
    public interface ITradeItem
    {
        string DisplayName { get; }
        int Price { get; }
        bool TryTrade();
    }

}
