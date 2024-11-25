using System.Runtime.Serialization;
using Task.DB;

namespace Task.TestHelpers
{
    // Is used instead of Order_Detail class during serialization.
    public class SerializationSurrogate : ISerializationSurrogate
    {
        // Add Order_Detail fields to SerializationInfo instance.
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var orderDetail = (Order_Detail)obj;
            info.AddValue("OrderID", orderDetail.OrderID);
            info.AddValue("ProductID", orderDetail.ProductID);
            info.AddValue("UnitPrice", orderDetail.UnitPrice);
            info.AddValue("Quantity", orderDetail.Quantity);
            info.AddValue("Discount", orderDetail.Discount);
            info.AddValue("Order", orderDetail.Order);
            info.AddValue("Product", orderDetail.Product);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var orderDetail = (Order_Detail)obj;
            orderDetail.OrderID = info.GetInt32("OrderID");
            orderDetail.ProductID = info.GetInt32("ProductID");
            orderDetail.UnitPrice = info.GetDecimal("UnitPrice");
            orderDetail.Quantity = info.GetInt16("Quantity");
            orderDetail.Discount = info.GetSingle("Discount");
            orderDetail.Order = (Order)info.GetValue("Order", typeof(Order));
            orderDetail.Product = (Product)info.GetValue("Product", typeof(Product));
            return orderDetail;
        }
    }
}
