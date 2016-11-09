using MVC_Lagersystem.Data_Access_Layer;
using MVC_Lagersystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_Lagersystem.Repositories
{
    public class StoreRepository
    {
        private StoreContext db;

        public StoreRepository()
        {
            this.db = new StoreContext();
        }

        public IEnumerable<StockItem> GetAllItems(string SortBy = null, bool Reverse = false)
        {
            return SortItems(db.Items, SortBy, Reverse);
        }

        public StockItem GetItemById(int id)
        {
            StockItem item = db.Items.First(o => o.ItemID == id);

            if (item != null)
                return item;

            return null;
        }

        public void AddItem(StockItem item)
        {
            db.Items.Add(item);
            db.SaveChanges();
        }

        public void EditItem(StockItem item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveItem(int id)
        {
            StockItem item = db.Items.Single(o => o.ItemID == id);
            db.Items.Remove(item);
            db.SaveChanges();
        }

        public IEnumerable<StockItem> GetItemBySearch(string str, string SortBy = null, bool Reverse = false)
        {
            IEnumerable<StockItem> tmp = db.Items.Where(o => o.Name.StartsWith(str));
            return SortItems(tmp, SortBy, Reverse);
        }

        private IEnumerable<StockItem> SortItems(IEnumerable<StockItem> items, string SortBy, bool Reverse)
        {
            if (SortBy != null)
            {
                IEnumerable<StockItem> i = items.AsEnumerable().OrderBy(o => typeof(StockItem).GetProperty(SortBy).GetValue(o, null));

                if (Reverse)
                    i = i.Reverse();

                return i;
            }

            return items;
        }
    }
}