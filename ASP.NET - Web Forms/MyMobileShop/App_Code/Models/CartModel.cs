using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CartModel
/// </summary>
public class CartModel
{
    public string InsertCart(Cart cart)
    {
        try
        {
            MyMobileShopDBEntities db = new MyMobileShopDBEntities();
            db.Carts.Add(cart);
            db.SaveChanges();

            return cart.DatePurchased + " was succesfully inserted";
        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }

    public string UpdateCart(int id, Cart cart)
    {
        try
        {
            MyMobileShopDBEntities db = new MyMobileShopDBEntities();

            //Fetch object from db
            Cart p = db.Carts.Find(id);

            p.DatePurchased = cart.DatePurchased;
            p.ClientID = cart.ClientID;
            p.Amount = cart.Amount;
            p.IsInCart = cart.IsInCart;
            p.ProductID = cart.ProductID;

            db.SaveChanges();
            return cart.DatePurchased + " was succesfully updated";

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
            MyMobileShopDBEntities db = new MyMobileShopDBEntities();
            Cart cart = db.Carts.Find(id);

            db.Carts.Attach(cart);
            db.Carts.Remove(cart);
            db.SaveChanges();

            return cart.DatePurchased + "was succesfully deleted";
        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }

    public List<Cart> GetOrdersInCart(string userId)
    {
        MyMobileShopDBEntities db = new MyMobileShopDBEntities();
        List<Cart> orders = (from x in db.Carts
                             where x.ClientID == userId
          && x.IsInCart
                             orderby x.DatePurchased
                             select x).ToList();
        return orders;
    }

    public int GetAmountOfOrders(string userId)
    {
        try
        {
            MyMobileShopDBEntities db = new MyMobileShopDBEntities();
            int amount = (from x in db.Carts
                          where x.ClientID == userId
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
        MyMobileShopDBEntities db = new MyMobileShopDBEntities();
        Cart cart = db.Carts.Find(id);
        cart.Amount = quantity;
        db.SaveChanges();
    }

    public void MarkOrdersAsPaid(List<Cart> carts)
    {
        MyMobileShopDBEntities db = new MyMobileShopDBEntities();

        if(carts != null)
        {
            foreach(Cart cart in carts)
            {
                Cart oldCart = db.Carts.Find(cart.ID);
                oldCart.DatePurchased = DateTime.Now;
                oldCart.IsInCart = false;
            }

            db.SaveChanges();
        }
    }
}