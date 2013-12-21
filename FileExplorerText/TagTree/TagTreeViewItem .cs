using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileExplorerText.TagTree
{
    public class TagTreeViewItem : ImagedTreeViewItem
    {
        TagCategory tagObj;
        TagManagerElementData tagManager;

        public TagTreeViewItem(TagCategory tagObj)
        {
            this.tagObj = tagObj;
            Text = tagObj.TagName;
            
            //SelectedImage = new BitmapImage(new Uri("pack:application:,,/"))
        }

        public TagCategory TagCategory
        {
            get { return tagObj; }
        }

        public void Populate(TagManagerElementData _tagManager)
        {
            Items.Clear();
            List<TagCategory> tagObjs;
            this.tagManager = _tagManager;

            try
            {
                tagObjs = tagObj.getSubtag(tagManager.allTags);
            }
            catch (System.Exception ex)
            {
                return;
            }

            foreach (TagCategory tagObj in tagObjs)
            {
                //tagObj의 하위까지 복사된다.
                //Items.Add(new TagTreeViewItem(tagObj)); 

                TagCategory tmp = new TagCategory();
                tmp.TagId = tagObj.TagId;
                tmp.TagName = tagObj.TagName;
                tmp.SubTag = tagObj.SubTag;
                Items.Add(new TagTreeViewItem(tmp));
            }
        }

        protected override void OnExpanded(System.Windows.RoutedEventArgs e)
        {
            base.OnExpanded(e);
           
            foreach (object obj in Items)
            {
                TagTreeViewItem item = obj as TagTreeViewItem;
                item.Populate(this.tagManager);
            }
        }
    }    
}
