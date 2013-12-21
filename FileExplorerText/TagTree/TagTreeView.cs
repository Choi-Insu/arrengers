using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace FileExplorerText.TagTree
{
    public class TagTreeView : TreeView
    {
        public TagTreeView()
        {
            RefreshTree();
        }

        public void RefreshTree()
        {
            //BeginInit();
            Items.Clear();

            TagManagerElementData tagManager = new TagManagerElementData();

            foreach (TagCategory tagObj in tagManager.allTags)
            {
                if(tagObj.SubTag.Length > 0)
                {
                    TagTreeViewItem item = new TagTreeViewItem(tagObj);
                    item.Text = tagObj.TagName;
                    Items.Add(item);
                    item.Populate(tagManager);
                }
                EndInit();
            }
        }
    }
}
