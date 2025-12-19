using MyToDo.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
   public class ToDoViewModel:BindableBase
    {
        public DelegateCommand AddCommand { get;private set; }
        /// <summary>
        /// 右侧编辑窗口是否展开
        /// </summary>
        private bool isRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }


        public ToDoViewModel()
        {
            CreateToDoDtos();
            AddCommand = new DelegateCommand(Add);
        }
        /// <summary>
        /// 添加待办
        /// </summary>
        private void Add()
        {
            IsRightDrawerOpen = true;
        }

        private void CreateToDoDtos()
        {
            for (int i = 0; i < 20; i++)
            {
                ToDoDtos.Add(new() { 
                    Title = "标题"+i,
                    Content = "测试数据...",
                });
            }
        }

        private ObservableCollection<ToDoDto> toDoDtos=new();

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { 
                toDoDtos = value;
                RaisePropertyChanged();
            }
        }
        

    }
}
