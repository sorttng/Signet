using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
namespace FAS.UI
{
    /// <summary>
    /// PagerControl.xaml 的交互逻辑
    /// </summary>
    public partial class PagerControl : UserControl
    {
        #region Dependency Properties
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(int), typeof(PagerControl),
                new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPageChanged));

        public static readonly DependencyProperty TotalItemsProperty =
            DependencyProperty.Register("TotalItems", typeof(int), typeof(PagerControl),
                new PropertyMetadata(0, OnPageChanged));

        public static readonly DependencyProperty TotalPagesProperty =
            DependencyProperty.Register("TotalPages",typeof(int),typeof(PagerControl),
                new FrameworkPropertyMetadata(0,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,OnPageChanged));

        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int), typeof(PagerControl),
                new FrameworkPropertyMetadata(20, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPageChanged));

        public static readonly DependencyProperty HasDataProperty =
                DependencyProperty.Register("HasData", typeof(bool), typeof(PagerControl),
                    new PropertyMetadata(false));
        public static readonly DependencyProperty PageNumbersProperty =
                DependencyProperty.Register("PageNumbers", typeof(ObservableCollection<PageNumberItem>), typeof(PagerControl));

        public static readonly DependencyProperty CanPreviousProperty =
                DependencyProperty.Register("CanPrevious", typeof(bool), typeof(PagerControl),
                    new PropertyMetadata(false));

        public static readonly DependencyProperty CanNextProperty =
                DependencyProperty.Register("CanNext", typeof(bool), typeof(PagerControl),
                    new PropertyMetadata(false));

        public int CurrentPage
        {
            get => (int)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        public int TotalPages
        {
            get => (int)GetValue(TotalPagesProperty);
            set => SetValue(TotalPagesProperty, value);
        }

        public int TotalItems
        {
            get => (int)GetValue(TotalItemsProperty);
            set => SetValue(TotalItemsProperty, value);
        }

        // 添加可选项集合
        public int PageSize
        {
            get => (int)GetValue(PageSizeProperty);
            set => SetValue(PageSizeProperty, value);
        }

        public ObservableCollection<PageNumberItem> PageNumbers
        {
            get => (ObservableCollection<PageNumberItem>)GetValue(PageNumbersProperty);
            set => SetValue(PageNumbersProperty, value);
        }

        public bool HasData
        {
            get => (bool)GetValue(HasDataProperty);
            private set => SetValue(HasDataProperty, value);
        }

        public bool CanPrevious
        {
            get => (bool)GetValue(CanPreviousProperty);
            private set => SetValue(CanPreviousProperty, value);
        }

        public bool CanNext
        {
            get => (bool)GetValue(CanNextProperty);
            private set => SetValue(CanNextProperty, value);
        }
        #endregion

        public PagerControl()
        {
            InitializeComponent();
            PageNumbers = new ObservableCollection<PageNumberItem>();
            Loaded += (s, e) => UpdatePagination();
        }

        #region Event Handlers
        private void FirstPage_Click(object sender, RoutedEventArgs e) => CurrentPage = 1;
        private void LastPage_Click(object sender, RoutedEventArgs e) => CurrentPage = TotalPages;
        private void PreviousPage_Click(object sender, RoutedEventArgs e) => CurrentPage--;
        private void NextPage_Click(object sender, RoutedEventArgs e) => CurrentPage++;

        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int page)
            {
                CurrentPage = page;
            }
        }
        #endregion

        #region Pagination Logic
        //private int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling((double)TotalItems / PageSize);
        private static void OnTotalPagesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (PagerControl)d;
            // 当总页数变化时更新分页显示
            control.UpdatePagination();
        }

        private static void OnPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (PagerControl)d;
            control.ValidateCurrentPage();

            // 计算新的总页数
            int newTotalPages = control.PageSize == 0 ? 0 :
                (int)Math.Ceiling((double)control.TotalItems / control.PageSize);

            // 更新依赖属性
            control.TotalPages = newTotalPages;

            control.UpdatePagination();
            control.UpdateControlStates();
        }

        private void ValidateCurrentPage()
        {
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > TotalPages && TotalPages != 0) CurrentPage = TotalPages;
        }

        private void UpdatePagination()
        {
            PageNumbers.Clear();
            if (TotalItems <= 0 || TotalPages <= 0) return;

            const int maxVisible = 5;
            var pages = new List<int>();

            if (TotalPages <= maxVisible)
            {
                pages.AddRange(Enumerable.Range(1, TotalPages));
            }
            else
            {
                int start = Math.Max(2, CurrentPage - 1);
                int end = Math.Min(TotalPages - 1, CurrentPage + 1);

                pages.Add(1);
                if (start > 2) pages.Add(-1); // Ellipsis

                for (int i = start; i <= end; i++)
                    pages.Add(i);

                if (end < TotalPages - 1) pages.Add(-1);
                pages.Add(TotalPages);
            }

            // 转换为PageNumberItem并设置状态
            foreach (var page in pages)
            {
                PageNumbers.Add(new PageNumberItem
                {
                    Number = page,
                    Display = page == -1 ? "..." : page.ToString(),
                    IsCurrent = page == CurrentPage // 这里设置当前页状态
                });
            }
        }

        private void UpdateControlStates()
        {
            // 计算是否有数据
            HasData = TotalItems > 0;

            // 计算按钮状态
            CanPrevious = HasData && CurrentPage > 1;
            CanNext = HasData && CurrentPage < TotalPages;

            // 禁用页码输入框
            var txtPage = this.FindName("txtPageInput") as TextBox;
            if (txtPage != null) txtPage.IsEnabled = HasData;
        }
        #endregion

        private void CmbPageSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is int size)
            {
                PageSize = size;
                CurrentPage = 1; // 切换页数时重置到首页
            }
        }
    }

    public class PageNumberItem
    {
        public int Number { get; set; }
        public string Display { get; set; }
        public bool IsCurrent { get; set; } // 新增属性
    }
}
