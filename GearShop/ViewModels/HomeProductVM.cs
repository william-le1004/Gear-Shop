using Domain.Entities;

namespace GearShopWeb.ViewModels;

public class HomeProductVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string ImgUrl { get; set; }
    //public Product product { get; set; }
    public IEnumerable<CartItem> cartList { get; set; }
}
