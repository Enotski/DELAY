using DELAY.Core.Application.Contracts.Enums;

namespace DELAY.Core.Application.Contracts.Models.SelectOptions
{
    public class SortOptions
    {
        public string Column { get; set; }
        public OrderType Order { get; set; }
        public SortOptions(string column, OrderType order) : this(column)
        {
            Order = order;
        }
        public SortOptions(string column)
        {
            Column = column;
        }
        public SortOptions()
        {
        }
    }
}
