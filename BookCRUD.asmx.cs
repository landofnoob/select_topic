using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;

namespace Service
{
    /// <summary>
    /// Summary description for BookCRUD
    /// </summary>
    [WebService(Namespace = "http://follow-me.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BookCRUD : System.Web.Services.WebService
    {
        XML object1 = new XML("C:\\Users\\PLUG\\Desktop\\Service\\Data_Book.xml");
        [WebMethod]
        public object Load(string path)
        {
            object1.Load(path);
            return object1.Read();
        }
        [WebMethod]
        public object Read()
        {
            return object1.Read();
        }
        [WebMethod]
        public object ReadByID(int ID)
        {
            return object1.ReadbyID(ID);
        }
        [WebMethod]
        public object Insert()
        {
            object1.Insert_Element();
            object1.Save();
            return object1.Read();
        }
        [WebMethod]
        public object InsertAll(string title, string price, string author, string year, string category, string ISBN_10, string ISBN_13)
        {
            object1.Insert_Element(title, price, author, year, category, ISBN_10, ISBN_13);
            object1.Save();
            return object1.Read();
        }
        [WebMethod]
        public object Update_Element(int ID, string ElementName, string ElementValue)
        {
            object1.Update_Element(ID, ElementName, ElementValue);
            object1.Save();
            return object1.Read();
        }
        [WebMethod]
        public object Update(int ID)
        {
            object1.Update(ID);
            object1.Save();
            return object1.Read();
        }
        [WebMethod(MessageName = "Delete", Description = "Remove Book form Bookstore")]
        public object Delete(int id)
        {
            object1.Delete(id);
            object1.Save();
            return object1.Read();
        }
        [WebMethod]
        public int Length()
        {
            return object1.Count();
        }
        [WebMethod]
        public object Save()
        {
            object1.Save();
            return object1.Read();
        }
        [WebMethod]
        public object Insert_AllandPage(string title, string price, string author, string year, string category, string ISBN_10, string ISBN_13, string Page)
        {
            object1.Insert_Pages_Element(title, price, author, year, category, ISBN_10, ISBN_13, Page);
            object1.Save();
            return object1.Read();
        }
        [WebMethod]
        public object Insert_AllandMoney(string title, string price, string author, string year, string category, string ISBN_10, string ISBN_13, string Money)
        {
            object1.Insert_Moneys_Element (title, price, author, year, category, ISBN_10, ISBN_13, Money);
            object1.Save();
            return object1.Read();
        }
        [WebMethod]
        public object Insert_AllPageandMoney(string title, string price, string author, string year, string category, string ISBN_10, string ISBN_13, string Page, string Money)
        {
            object1.Insert_Page_Money_Element(title, price, author, year, category, ISBN_10, ISBN_13, Page, Money);
            object1.Save();
            return object1.Read();
        }
        [WebMethod]
        public object Add_Page(int check, string Page)
        {
            object1.Insert_Page_Element(check, Page);
            object1.Save();
            return object1.Read();
        }
        [WebMethod]
        public object Add_Money(int check, string Money)
        {
            object1.Insert_Money_Element(check, Money);
            object1.Save();
            return object1.Read();
        }

        //Search Return ID 
        [WebMethod]
        public object Search_price(string prices,string logic)
        {
            return object1.SearchByPrice(prices,logic);
        }
    }
    class XML
    {
        public XDocument xdoc = new XDocument();
        public string XML_dir = "";
        public XML()
        {

        }
        public XML(string path)
        {
            xdoc = XDocument.Load(path);
            XML_dir = path;
        }
        //Load Data
        public void Load(string path)
        {
            xdoc = XDocument.Load(path);
            XML_dir = path;
        }
        //Read
        public object Read()
        {
            return xdoc.Elements("bookstore").Single().ToString();
        }
        //Read by ID
        public object ReadbyID(int check)
        {
            return xdoc.Element("bookstore").Elements("book").Where(x => (string)x.Attribute("ID") == check.ToString()).Single().ToString();
        }
        //Update TestCase 
        public void Update(int check)
        {
            xdoc.Elements("bookstore").Elements("book").Where(x => (string)x.Attribute("ID") == check.ToString()).Single().Element("Title").Value = "Update_Testing................................";
        }
        //Delete
        public void Delete(int check)
        {
            xdoc.Elements("bookstore").Elements("book").Where(x => (string)x.Attribute("ID") == check.ToString()).Remove();
        }
        public int Count()
        {
            int count = 0;
            var result = xdoc.Elements("bookstore").Descendants();
            foreach (XElement item in result)
            {
                if (item.Name == "book")
                {
                    count += 1;
                }
            }
            return count;
        }
        public void Test_Case()
        {
            Console.WriteLine(
              xdoc.Elements("bookstore")
              .Elements("book")
              .Where(x => (string)x.Attribute("ID") == "10")
              .Single()
              );
        }
        //Add Item No Parameter for testing
        public void Insert_Element()
        {
            XElement Insert_Data = new XElement("book");
            Insert_Data.Add(new XAttribute("ID", "99"));
            Insert_Data.Add(new XElement("Title", "Testing"));
            Insert_Data.Add(new XElement("Price", "99"));
            Insert_Data.Add(new XElement("Author", "Tester"));
            Insert_Data.Add(new XElement("Year", "1999"));
            Insert_Data.Add(new XElement("Category", "Computer"));
            Insert_Data.Add(new XElement("ISBN-10", "9999999999"));
            Insert_Data.Add(new XElement("ISBN-13", "9999999999999"));
            xdoc.Element("bookstore").Add(Insert_Data);
            Console.WriteLine(
              xdoc.Elements("bookstore")
              .Elements("book")
              .Where(x => (string)x.Attribute("ID") == "99")
              .Single()
              );
        }
        //Add Item
        public void Insert_Element(string title, string price, string author, string year, string category, string ISBN_10, string ISBN_13)
        {
            XElement Insert_Data = new XElement("book");
            Insert_Data.Add(new XAttribute("ID", this.Count() + 1));
            Insert_Data.Add(new XElement("Title", title));
            Insert_Data.Add(new XElement("Price", price));
            Insert_Data.Add(new XElement("Author", author));
            Insert_Data.Add(new XElement("Year", year));
            Insert_Data.Add(new XElement("Category", category));
            Insert_Data.Add(new XElement("ISBN-10", ISBN_10));
            Insert_Data.Add(new XElement("ISBN-13", ISBN_13));
            //Console.WriteLine(Insert_Data);
            xdoc.Element("bookstore").Add(Insert_Data);
            //Console.WriteLine(xdoc.Element("bookstore"));
        }
        //Update Items
        public void Update_Element(int check, string ElementNameInput, string ElementValue)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("title", "Title");
            dict.Add("id", "ID");
            dict.Add("isbn-10", "ISBN-10");
            dict.Add("isbn-13", "ISBN-13");
            dict.Add("year", "Year");
            dict.Add("author", "Author");
            dict.Add("price", "Price");
            dict.Add("category", "Category");
            dict.Add("isbn_10", "ISBN-10");
            dict.Add("isbn_13", "ISBN-13");
            string ElementName;
            if (dict.TryGetValue(ElementNameInput.ToLower(), out ElementName))
            {
            }
            xdoc.Elements("bookstore").Elements("book").Where(x => (string)x.Attribute("ID") == check.ToString()).Single().Element(ElementName).Value = ElementValue;
        }
        //Remove Items
        public void Delete_Element(int check)
        {
            xdoc.Elements("bookstore").Elements("book").Where(x => (string)x.Attribute("ID") == check.ToString()).Remove();
            Console.WriteLine(xdoc.Element("bookstore"));
        }
        //Search Return ID
        public int Search_Element()
        {
            int ID = 0;

            return ID;
        }
        public void Save()
        {
            xdoc.Save(XML_dir);
        }
        public void Save(string path)
        {
            xdoc.Save(path);
        }
        public object Search(string Type, string data)
        {
            return "object";
        }
        public object SearchByPrice(string Price,string logic)
        {
            string prices = xdoc.Elements("bookstore").Elements("book").Elements("Price").Single().Value.ToString();
            var result = xdoc.Elements("bookstore").Descendants();
            XDocument Data = new XDocument();
            foreach (XElement item in result)
            {
                if (Price == prices)
                {
                    Data.Add(item);
                }
            }

            return Data;
        }
        private bool Operator(string logic, int x, int y)
        {
            switch (logic)
            {
                case ">": return x > y;
                case "<": return x < y;
                case "==": return x == y;
                default: throw new Exception("invalid logic");
            }
        }

        //In Class Test

        //TestCase
        public void Test_Page_Money_Element()
        {
            XElement Insert_Data = new XElement("book");
            Insert_Data.Add(new XAttribute("ID", "99"));
            Insert_Data.Add(new XElement("Title", "Testing"));
            Insert_Data.Add(new XElement("Price", "99"));
            Insert_Data.Add(new XElement("Author", "Tester"));
            Insert_Data.Add(new XElement("Year", "1999"));
            Insert_Data.Add(new XElement("Category", "Computer"));
            Insert_Data.Add(new XElement("ISBN-10", "9999999999"));
            Insert_Data.Add(new XElement("ISBN-13", "9999999999999"));
            Insert_Data.Add(new XElement("Pages", "9999"));
            Insert_Data.Add(new XElement("Thai_Money", "999"));
            xdoc.Element("bookstore").Add(Insert_Data);
            Console.WriteLine(
              xdoc.Elements("bookstore")
              .Elements("book")
              .Where(x => (string)x.Attribute("ID") == "99")
              .Single()
              );

        }
        public void Insert_Pages_Element(string title, string price, string author, string year, string category, string ISBN_10, string ISBN_13, string Pages)
        {
            XElement Insert_Data = new XElement("book");
            Insert_Data.Add(new XAttribute("ID", this.Count() + 1));
            Insert_Data.Add(new XElement("Title", title));
            Insert_Data.Add(new XElement("Price", price));
            Insert_Data.Add(new XElement("Author", author));
            Insert_Data.Add(new XElement("Year", year));
            Insert_Data.Add(new XElement("Category", category));
            Insert_Data.Add(new XElement("ISBN-10", ISBN_10));
            Insert_Data.Add(new XElement("ISBN-13", ISBN_13));
            Insert_Data.Add(new XElement("Pages", Pages));
            Insert_Data.Add(new XElement("Thai_Money", ""));
            //Console.WriteLine(Insert_Data);
            xdoc.Element("bookstore").Add(Insert_Data);
            //Console.WriteLine(xdoc.Element("bookstore"));
        }

        public void Insert_Moneys_Element(string title, string price, string author, string year, string category, string ISBN_10, string ISBN_13, string Money)
        {
            XElement Insert_Data = new XElement("book");
            Insert_Data.Add(new XAttribute("ID", this.Count() + 1));
            Insert_Data.Add(new XElement("Title", title));
            Insert_Data.Add(new XElement("Price", price));
            Insert_Data.Add(new XElement("Author", author));
            Insert_Data.Add(new XElement("Year", year));
            Insert_Data.Add(new XElement("Category", category));
            Insert_Data.Add(new XElement("ISBN-10", ISBN_10));
            Insert_Data.Add(new XElement("ISBN-13", ISBN_13));
            Insert_Data.Add(new XElement("Pages", ""));
            Insert_Data.Add(new XElement("Thai_Money", Money));
            //Console.WriteLine(Insert_Data);
            xdoc.Element("bookstore").Add(Insert_Data);
            //Console.WriteLine(xdoc.Element("bookstore"));
        }
        public void Insert_Page_Money_Element(string title, string price, string author, string year, string category, string ISBN_10, string ISBN_13, string Page,string Money)
        {
            XElement Insert_Data = new XElement("book");
            Insert_Data.Add(new XAttribute("ID", this.Count() + 1));
            Insert_Data.Add(new XElement("Title", title));
            Insert_Data.Add(new XElement("Price", price));
            Insert_Data.Add(new XElement("Author", author));
            Insert_Data.Add(new XElement("Year", year));
            Insert_Data.Add(new XElement("Category", category));
            Insert_Data.Add(new XElement("ISBN-10", ISBN_10));
            Insert_Data.Add(new XElement("ISBN-13", ISBN_13));
            Insert_Data.Add(new XElement("Pages", Page));
            Insert_Data.Add(new XElement("Thai_Money", Money));
            //Console.WriteLine(Insert_Data);
            xdoc.Element("bookstore").Add(Insert_Data);
            //Console.WriteLine(xdoc.Element("bookstore"));
        }
        public void Insert_Page_Element(int check, string Pages)
        {
            XElement Insert_Data = new XElement("book");
            Insert_Data.Add(new XElement("Pages"), Pages);
            xdoc.Elements("bookstore").Elements("book").Where(x => (string)x.Attribute("ID") == check.ToString()).Single().Add(Insert_Data);
        }
        public void Insert_Money_Element(int check, string Money)
        {
            XElement Insert_Data = new XElement("book");
            Insert_Data.Add(new XElement("Thai_Money"), Money);
            xdoc.Elements("bookstore").Elements("book").Where(x => (string)x.Attribute("ID") == check.ToString()).Single().Add(Insert_Data);
        }
    }
}