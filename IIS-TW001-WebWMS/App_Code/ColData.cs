using System;
using System.Collections.Generic;
using System.Text;

namespace ClassBuilder
{
    public class ColData
    {
        public ColData()
        {
            this.LoadCatalog = ClassBuilder.EnumLoadCatalog.None;
            this.SQL = "";
            this.ReferencedTable = "";
            this.ReferencedField = "";
            this.ReferencedDisplayField = "";

        }

        public ClassBuilder.EnumLoadCatalog LoadCatalog
        {
            get;
            set;
        }

        public string SQL
        {
            get;
            set;
        }

        public string ReferencedTable
        {
            get;
            set;
        }

        public string ReferencedField
        {
            get;
            set;
        }

        public string ReferencedDisplayField
        {
            get;
            set;
        }

        public List<KeyValuePair<string, string>> Lists = new List<KeyValuePair<string, string>>();

        public void CopyTo(ColData another)
        {
            if (another != null)
            {
                another.LoadCatalog = this.LoadCatalog;
                another.SQL = this.SQL;
                another.ReferencedTable = this.ReferencedTable;
                another.ReferencedField = this.ReferencedField;
                another.ReferencedDisplayField = this.ReferencedDisplayField;
                another.Lists.Clear();
                foreach (KeyValuePair<string, string> one in this.Lists)
                {
                    another.Lists.Add(one);
                }
            }
        }
    }
}