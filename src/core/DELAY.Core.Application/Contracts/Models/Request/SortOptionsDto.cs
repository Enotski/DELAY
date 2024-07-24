using DELAY.Core.Application.Contracts.Enums;

namespace DELAY.Core.Application.Contracts.Models.Request
{
    public class SortOptionsDto
    {
        public string Column { get; set; }
        public OrderType Order { get; set; }
        public SortOptionsDto(string column, OrderType order) : this(column)
        {
            Order = order;
        }
        public SortOptionsDto(string column)
        {
            Column = column;
        }
        public SortOptionsDto()
        {
        }
    }
}
