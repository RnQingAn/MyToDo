using MyToDo.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public  class MemoViewModel:BindableBase
    {

        public DelegateCommand AddCommand { get; private set; }
        /// <summary>
        /// 右侧编辑窗口是否展开
        /// </summary>
        private bool isRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }


        public MemoViewModel()
        {
            CreateToDoDtos();
            AddCommand = new DelegateCommand(Add);
        }
        /// <summary>
        /// 添加备忘录
        /// </summary>
        private void Add()
        {
            IsRightDrawerOpen = true;
        }

        private void CreateToDoDtos()
        {
            for (int i = 0; i < 20; i++)
            {
                MemoDtos.Add(new()
                {
                    Title = "标题" + i,
                    Content = "测试数据...",
                });
            }
        }

        private ObservableCollection<MemoDto> memoDtos = new();

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set
            {
                memoDtos = value;
                RaisePropertyChanged();
            }
        }

    }
}
