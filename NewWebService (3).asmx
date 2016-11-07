﻿<%@ WebService Language="C#" Class="Service.BookCRUD" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;

namespace Service
{
    [WebService(Namespace = "http://follow-me.com/")]
        public class BookCRUD : System.Web.Services.WebService
    {
        XML object1 = new XML("W:\\Visual\\C#\\Selectop_work\\Work1_XML\\W1_xml_n\\W1_xml_n\\Data_Book.xml");
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
        [WebMethod(MessageName = "ABC", Description = "Default no parameter for test case")]
        public object Insert()
        {
            object1.Insert_Element();
            object1.Save();
            return object1.Read();
        }
        [WebMethod(MessageName = "Hi", Description = "Insert Element")]
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
        //Search Return ID 
        [WebMethod]
        public object Search_price(string low , string high)
        {
            return object1.SearchByPrice(low, high);
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
        public object SearchByPrice(string Lower, string Upper)
        {
            //string price = xdoc.Elements("bookstore").Elements("book").Elements("Price").Single().Value;
            //float low = float.Parse(Lower);
            //float up = float.Parse(Upper);
            //string price = xdoc.Elements("bookstore").Elements("book").Elements("Price").Single().Value;   
            return 1;//xdoc.Elements("bookstore").Elements("book").Where(x => float.Parse(x.Elements("Price").Single().Value) >= 10).SingleOrDefault().ToString();
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
    }

}
