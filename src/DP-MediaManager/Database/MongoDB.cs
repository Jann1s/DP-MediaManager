﻿using DP_MediaManager.LibraryItem;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_MediaManager.Database
{
    class MongoDB
    {

        public Boolean Insert(IMovie movie, ISeries series)
        {
            return true;
        }

        public void Edit(int id)
        {

        }

        public void Find(String searchItem)
        {

        }

        public Boolean Delete(int id)
        {
            return true;
        }

        public List<Entry> ShowAll()
        {
            return null;
        }

        public Entry Show(int id)
        {
            return null;
        }
    }
}
