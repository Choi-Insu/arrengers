using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace FileExplorerText.TagTree
{
    public class TagCategory
    {
        private int tagId;
        private string tagName;
        private string subTag;
        public List<TagCategory> searchedTags = new List<TagCategory>();

        public int TagId
        {
            get { return this.tagId; }
            set { this.tagId = value; }

        }

        public string TagName
        {
            get { return this.tagName; }
            set { this.tagName = value; }

        }

        public string SubTag
        {
            get { return this.subTag; }
            set { this.subTag = value; }
        }

        public List<TagCategory> getSubtag(List<TagCategory> allTags)
        {

            string[] subArray = SubTag.Split('/');


            foreach (string data in subArray)
            {
                if (data != "")
                {                   
                    foreach (TagCategory elementData in allTags)
                    {

                        int tagId = Convert.ToInt32(data);
                        if (tagId == elementData.TagId)
                        {
                            searchedTags.Add(elementData);
                        }
                    }
                }
            }
            return searchedTags;
        }
    }

    public class TagManagerElementData
    {

        private string query = "";


        public List<TagCategory> allTags;
        public int upperTagCount;

        private string dbConnectionString = "Data Source=TAGDB.sqlite;Version=3;";
        private SQLiteConnection sqliteCon;
        private SQLiteDataReader dr = null;
        private SQLiteCommand createCommand = null;


        public TagManagerElementData()
        {
            upperTagCount = 0;
            query = "";

            allTags = new List<TagCategory>();

            sqliteCon = new SQLiteConnection(dbConnectionString);
            try
            {

                sqliteCon.Open();
                query = "select TAGID,TAGNAME,DEPENDENCY from TAGMANAGER";
                createCommand = new SQLiteCommand(query, sqliteCon);
                createCommand.ExecuteNonQuery();
                dr = createCommand.ExecuteReader();

                while (dr.Read())
                {
                    TagCategory elementdata = new TagCategory();

                    elementdata.TagId = dr.GetInt32(0);
                    elementdata.TagName = dr.GetString(1);
                    elementdata.SubTag = dr.GetString(2);
                    allTags.Add(elementdata);

                    if (elementdata.SubTag.Length > 0)
                    {
                        upperTagCount++;

                    }
                }

                sqliteCon.Close();
            }
            catch (Exception ex)
            {
            }

        }

    }
}
