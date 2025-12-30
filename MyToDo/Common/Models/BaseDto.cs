using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
   public class BaseDto:BindableBase
    {
        
        private int id=0;
        private string createDate="1970-1-1";
       
        public int Id
		{
			get { return id; }
            set { id = value; RaisePropertyChanged(); }
        }
		public string CreateDate
		{
			get { return createDate; }
			set { createDate = value; RaisePropertyChanged(); }
		}
        private string updateDate = "1970-1-1";
        public string UpdateDate
		{
			get { return updateDate; }
			set { updateDate = value; RaisePropertyChanged(); }
		}


	}
}
