using EStockFlow.Enums;

namespace EStockFlow.Models
{
    public class ReportResponse
    {
        public int Total { get; set; }
        
        public List<TransactionResponse> Transactions { get; set; }
    }
}