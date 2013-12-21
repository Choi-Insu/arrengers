using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using QuickZip.IO.PIDL.UserControls.ViewModel;
using FileExplorerText.TagTree;
using FileExplorerText.FileList2;
using FileExplorerText.DB;
using System.Collections.ObjectModel;
using System.Data.SQLite;

namespace PIDLTest
{
    /// <summary>
    /// Interaction logic for PIDLMVVM.xaml
    /// </summary>
    public partial class PIDLMVVM : Window
    {
        ObservableCollection<TagedItem> _TagedItemCollection =
        new ObservableCollection<TagedItem>();        

        public PIDLMVVM()
        {
            
            InitializeComponent();            
        }

        public ObservableCollection<TagedItem> TagedItemCollection
        { get { return _TagedItemCollection; } }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);  
            //this.dirTree.RootDirectory = FileSystemInfoEx.FromString(@"::{00021400-0000-0000-C000-000000000046}}") as DirectoryInfoEx;
            //this.dirTree.SelectedDirectory = FileSystemInfoEx.FromString(@"C:\") as DirectoryInfoEx;                           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FileSystemInfoEx fsi = (flist.Items[flist.Items.Count - 1] as FileListViewItemViewModel).EmbeddedModel.EmbeddedEntry;
            //fsi = (flist.Items[0] as FileListViewItemViewModel).EmbeddedModel.EmbeddedEntry;
            flist.Focus(fsi);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int tabIndex = tabControl.SelectedIndex;
            switch (tabIndex)
            {
                case 0:
                    {
                        flist.Visibility = Visibility.Visible;
                        flist2.Visibility = Visibility.Hidden;
                        break;
                    }
                case 1:
                    {
                        flist.Visibility = Visibility.Hidden;
                        flist2.Visibility = Visibility.Visible;
                        break;
                    }

            }            
        }
        

        private void TagTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            DBManger dbManager = new DBManger();
            TagTreeViewItem selectedItem = e.NewValue as TagTreeViewItem;

            string query = "SELECT * FROM TAGTABLE WHERE TAGID LIKE '%" + selectedItem.TagCategory.TagId +"%'";
            dbManager.Query = query;
            dbManager.execute(ref _TagedItemCollection);              
        }        
        
    }
}
