using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using FileExplorerText.FileList2;

namespace FileExplorerText.DB
{
    public class DBManger
    {
        private string query;
        private string dbConnectionString = "Data Source=TAGDB.sqlite;Version=3;";
        private SQLiteConnection sqliteCon;
        private SQLiteDataReader dataReader = null;
        private SQLiteCommand createCommand = null;

        public DBManger()
        {
            sqliteCon = new SQLiteConnection(dbConnectionString);
        }

        public string Query
        {
            set
            {
                this.query = value;
            }
            get
            {
                return this.query;
            }
        }

        public SQLiteDataReader DateReader
        {
            get
            {
                return this.dataReader;
            }
        }

        public void execute(ref ObservableCollection<TagedItem> _TagedItemCollection)
        {
            sqliteCon.Open();            
            createCommand = new SQLiteCommand(query, sqliteCon);
            createCommand.ExecuteNonQuery();
            dataReader = createCommand.ExecuteReader();

            _TagedItemCollection.Clear();

            while (dataReader.Read())
            {
                _TagedItemCollection.Add(new TagedItem
                {
                    FileName = dataReader.GetString(2),
                    FileExension = dataReader.GetString(3),
                    ModifyDate = dataReader.GetString(4)
                });

            }

            sqliteCon.Close();
        }

        public void executeNoResult()
        {
            sqliteCon.Open();
            createCommand = new SQLiteCommand(query, sqliteCon);
            createCommand.ExecuteNonQuery();
            dataReader = createCommand.ExecuteReader();            

            sqliteCon.Close();
        }
    }
}
