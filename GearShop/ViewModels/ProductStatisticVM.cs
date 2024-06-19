namespace GearShopWeb.ViewModels;

public class ProductStatisticVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string ImgUrl { get; set; }
    public double Sold { get; set; }
    public double TotalPrice { get; set; }

    public string category { get; set; }
}