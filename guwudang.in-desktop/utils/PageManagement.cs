using guwudang.Account;
using guwudang.Invoice;
using guwudang.Partner;
using guwudang.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velacro.DataStructures;
using Velacro.UIElements.Basic;

namespace guwudang.utils
{
    public class PageManagement
    {
        private static bool initialized;
        private static MyDictionary<string, MyPage> pages;

        private static void initPages()
        {
            pages = new MyDictionary<string, MyPage>();
            pages.Add(utils.EPages.listDashboardPage, new Dashboard.Dashboard());
            pages.Add(utils.EPages.listAccountPage, new AccountPage());
            pages.Add(utils.EPages.listInvoicePage, new ListInvoicePage());
            pages.Add(utils.EPages.listProductPage, new ProductPage());
            pages.Add(utils.EPages.listPartnerPage, new PartnerPage());
            initialized = true;
        }

        public static MyPage getPage(string _page)
        {
            if (!initialized)
            {
                initPages();
            }
            return pages[_page];
        }

    }
}
