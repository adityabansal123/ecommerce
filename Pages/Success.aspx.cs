﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Success : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<cart> carts = (List<cart>)Session[User.Identity.GetUserId()];

        CartModel model = new CartModel();
        model.MarkOrdersAsPaid(carts);

        Session[User.Identity.GetUserId()] = null;
    }
}