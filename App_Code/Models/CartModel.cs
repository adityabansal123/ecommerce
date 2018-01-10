using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CartModel
/// </summary>
public class CartModel
{
    public string InsertCart(cart cart)
    {
        try
        {
            GarageDBEntities db = new GarageDBEntities();
            db.carts.Add(cart);
            db.SaveChanges();

            return cart.DatePurchased + " was successfully inserted";
        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }
    public string UpdateCart(int id, cart cart)
    {
        try
        {
            GarageDBEntities db = new GarageDBEntities();
            cart p = db.carts.Find(id);

            p.DatePurchased = cart.DatePurchased;
            p.ClientID = cart.ClientID;
            p.Amount = cart.Amount;
            p.IsInCart = cart.IsInCart;
            p.ProductID = cart.ProductID; 

            db.SaveChanges();

            return cart.DatePurchased + " was successfully updated";

        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }
    public string DeleteCart(int id)
    {
        try
        {
            GarageDBEntities db = new GarageDBEntities();
            cart cart = db.carts.Find(id);

            db.carts.Attach(cart);
            db.carts.Remove(cart);
            db.SaveChanges();

            return cart.DatePurchased + " was successfully deleted";
        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }

    public List<cart> GetOrdersInCart(string clientId)
    {
        GarageDBEntities db = new GarageDBEntities();
        List<cart> orders = (from x in db.carts
                             where x.ClientID == clientId
                             && x.IsInCart
                             orderby x.DatePurchased descending
                             select x).ToList();
        return orders;
    }

    public int GetAmountOfOrders(string clientId)
    {
        try
        {
            GarageDBEntities db = new GarageDBEntities();
            int amount = (from x in db.carts
                          where x.ClientID == clientId
                          && x.IsInCart
                          select x.Amount).Sum();

            return amount;
        }
        catch
        {
            return 0;
        }
    }

    public void UpdateQuantity(int id, int quantity)
    {
        GarageDBEntities db = new GarageDBEntities();
        cart p = db.carts.Find(id);
        p.Amount = quantity;

        db.SaveChanges();
    }

    public void MarkOrdersAsPaid(List<cart> carts)
    {
        GarageDBEntities db = new GarageDBEntities();

        if (carts != null)
        {
            foreach (cart cart in carts)
            {
                cart oldCart = db.carts.Find(cart.ID);
                oldCart.DatePurchased = DateTime.Now;
                oldCart.IsInCart = false;
            }
            db.SaveChanges();
        }
    }
}