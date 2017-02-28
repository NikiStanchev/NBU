using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Fillpage();
    }

    private void Fillpage()
    {
        // Get a list of all products in DB
        ProductModel productModel = new ProductModel();
        List<Product> products = productModel.GetAllProducts();

        // Make sure product exist in the DB
        if(products != null)
        {
            // Create a new panel with an ImageButton and 2 label for each product
            foreach(Product product in products)
            {
                Panel productPanel = new Panel();
                ImageButton imageButton = new ImageButton();
                Label lblName = new Label();
                Label lblPrice = new Label();

                // Set childControl's properties
                imageButton.ImageUrl = "~/Images/Products/" + product.Image;
                imageButton.CssClass = "productImage";
                imageButton.PostBackUrl = "~/Pages/Management/Product.aspx?id=" + product.ID;

                lblName.Text = product.Name;
                lblName.CssClass = "productName";

                lblPrice.Text = product.Price + " Лева";
                lblPrice.CssClass = "productPrice";

                // Add child controls to panel
                productPanel.Controls.Add(imageButton);
                productPanel.Controls.Add(new Literal { Text = "<br />" });
                productPanel.Controls.Add(lblName);
                productPanel.Controls.Add(new Literal { Text = "<br />" });
                productPanel.Controls.Add(lblPrice);

                // Add dinamic panels to static parent panel
                pnlProducts.Controls.Add(productPanel);
            }
        }
        else
        {
            // No products found
            pnlProducts.Controls.Add(new Literal { Text = "No products found!" });
        }
    }
}