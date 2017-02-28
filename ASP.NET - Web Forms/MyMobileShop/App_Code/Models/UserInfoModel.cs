using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserInfoModel
/// </summary>
public class UserInfoModel
{
    public UserInformation GetUserInformation(string guId)
    {
        MyMobileShopDBEntities db = new MyMobileShopDBEntities();
        UserInformation info = (from x in db.UserInformations
                    where x.GUID == guId
                    select x).FirstOrDefault();
        return info;
    }

    public void InsertUserInformstion(UserInformation info)
    {
        MyMobileShopDBEntities db = new MyMobileShopDBEntities();
        db.UserInformations.Add(info);
        db.SaveChanges();
    }
}