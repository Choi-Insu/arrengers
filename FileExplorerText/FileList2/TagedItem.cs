using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace FileExplorerText.FileList2
{
    
    public class TagedItem
    {        

        public string TagId { get; set; }
        public string TagName { get; set; }
        public string TagPath { get; set; }
        public string FileName { get; set; }
        public string FileExension { get; set; }
        public string ModifyDate { get; set; }
        public string FileSize { get; set; }
        public string ImgUri { get; set; }
        
    }

    
}
